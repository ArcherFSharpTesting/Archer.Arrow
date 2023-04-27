﻿module Archer.Arrows.Internal

open System
open System.ComponentModel
open System.IO
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer
open Archer.Arrows
open Archer.CoreTypes.InternalTypes

type TestInternals = {
    ContainerPath: string
    ContainerName: string
    TestName: string
    Tags: TestTag seq
    FilePath: string
    FileName: string
    LineNumber: int
}

type ExecutionResultsAccumulator<'featureSetupResult, 'setupResult> =
    | Empty
    | FeatureSetupRun of result: Result<'featureSetupResult, SetupTeardownFailure>
    | SetupRun of result: Result<'featureSetupResult, SetupTeardownFailure> * Result<'setupResult, SetupTeardownFailure>
    | TestRun of setupResult: Result<'featureSetupResult, SetupTeardownFailure> * Result<'setupResult, SetupTeardownFailure> * testResult: TestResult
    | TeardownRun of setupResult: Result<'featureSetupResult, SetupTeardownFailure> * Result<'setupResult, SetupTeardownFailure> * testResult: TestResult option * teardownResult: Result<unit, SetupTeardownFailure>
    | FeatureTeardownRun of setupResult: Result<'featureSetupResult, SetupTeardownFailure> * Result<'setupResult, SetupTeardownFailure> option * testResult: TestResult option * teardownResult: Result<unit, SetupTeardownFailure>
    | FailureAccumulated of setupResult: Result<'featureSetupResult, SetupTeardownFailure> option * Result<'setupResult, SetupTeardownFailure> option * GeneralTestingFailure 

type SetupTeardownExecutorLifecycleEventArgs =
    | ExecuteSetupStart of CancelEventArgs
    | ExecuteSetupEnd of result: SetupResult * cancelEventArgs: CancelEventArgs
    | ExecuteRunner of CancelEventArgs
    | ExecuteRunnerEnd of TestExecutionResult
    | ExecuteStartTeardown 
    
type SetupTeardownExecutionDelegate = delegate of obj * SetupTeardownExecutorLifecycleEventArgs -> unit

type ISetupTeardownExecutor<'inputType> =
    [<CLIEvent>]
    abstract member LifecycleEvent: IEvent<SetupTeardownExecutionDelegate, SetupTeardownExecutorLifecycleEventArgs> with get
    abstract member Execute: value: 'inputType -> TestEnvironment -> TestExecutionResult
    
type SetupTeardownExecutor<'inputType, 'outputType>(setup: 'inputType -> Result<'outputType, SetupTeardownFailure>, teardown: Result<'outputType, SetupTeardownFailure> -> TestResult option -> Result<unit, SetupTeardownFailure>, runner: 'outputType -> TestEnvironment -> TestExecutionResult) =
    let lifecycleEvent = Event<SetupTeardownExecutionDelegate, SetupTeardownExecutorLifecycleEventArgs> ()
    
    abstract member Trigger: sender:obj * args:SetupTeardownExecutorLifecycleEventArgs -> unit
    default _.Trigger (sender: obj, args: SetupTeardownExecutorLifecycleEventArgs) =
        lifecycleEvent.Trigger (sender, args)
    
    interface ISetupTeardownExecutor<'inputType> with
        [<CLIEvent>]
        member this.LifecycleEvent = lifecycleEvent.Publish
        
        member this.Execute (value: 'inputType) (env: TestEnvironment) =
            try
                let cancelEventArgs = CancelEventArgs ()
                this.Trigger (this, ExecuteSetupStart cancelEventArgs)
                
                if cancelEventArgs.Cancel then
                    SetupTeardownCanceledFailure |> SetupExecutionFailure
                else
                    let setupResult = setup value
                    
                    let setupReport =
                        match setupResult with
                        | Ok _ -> SetupSuccess
                        | Error errorValue -> errorValue |> SetupFailure
                        
                    this.Trigger (this, ExecuteSetupEnd (setupReport, cancelEventArgs))
                    
                    if cancelEventArgs.Cancel then
                        SetupTeardownCanceledFailure |> SetupExecutionFailure
                    else
                        try
                            let runnerResult = 
                                match setupResult with
                                | Ok r ->
                                    this.Trigger (this, ExecuteRunner cancelEventArgs)
                                    if cancelEventArgs.Cancel then
                                        GeneralCancelFailure |> GeneralExecutionFailure
                                    else
                                        try
                                            let runResult = runner r env
                                            try
                                                this.Trigger (this, ExecuteRunnerEnd runResult)
                                                runResult
                                            with
                                            | ex -> ex |> GeneralExceptionFailure |> GeneralExecutionFailure
                                        with
                                        | ex -> ex |> TestExceptionFailure |> TestFailure |> TestExecutionResult
                                | Error setupError -> setupError |> SetupExecutionFailure
                            
                            try
                                this.Trigger (this, ExecuteStartTeardown)
                                try
                                    
                                    let teardownResult =     
                                        match runnerResult with
                                        | TestExecutionResult testResult -> teardown setupResult (Some testResult)
                                        | _ -> teardown setupResult None
                                    
                                    match teardownResult with
                                    | Ok _ -> runnerResult
                                    | Error setupTeardownFailure -> setupTeardownFailure |> TeardownExecutionFailure
                                with
                                | ex -> ex |> SetupTeardownExceptionFailure |> TeardownExecutionFailure
                            with
                            | ex -> ex |> GeneralExceptionFailure |> GeneralExecutionFailure
                        with
                        | ex -> ex |> GeneralExceptionFailure |> GeneralExecutionFailure
            with
            | ex -> ex |> SetupTeardownExceptionFailure |> SetupExecutionFailure
        
type WrappedTeardownExecutor<'outerInputType, 'outerOutputType> (outerSetup: 'outerInputType -> Result<'outerOutputType, SetupTeardownFailure>, outerTeardown: Result<'outerOutputType, SetupTeardownFailure> -> TestResult option -> Result<unit, SetupTeardownFailure>, inner: ISetupTeardownExecutor<'outerOutputType>) as this =
    inherit SetupTeardownExecutor<'outerInputType, 'outerOutputType> (outerSetup, outerTeardown, inner.Execute)
    
    do
        let executor = inner
        executor.LifecycleEvent.AddHandler (fun sender args ->
            this.Trigger (sender, args)
        )
    
    override this.Trigger (sender: obj, args: SetupTeardownExecutorLifecycleEventArgs) =
        match args with
        | ExecuteSetupStart _ ->
            if sender = this then
                base.Trigger (this, args)
        | ExecuteSetupEnd _ ->
            if sender <> this then
                base.Trigger (this, args)
        | ExecuteStartTeardown ->
            if sender = this then
                base.Trigger (this, args)
        | ExecuteRunner _ ->
            if sender <> this then
                base.Trigger (this, args)
        | ExecuteRunnerEnd _ ->
            if sender = this then
                base.Trigger (this, args)

    member this.AsSetupTeardownExecutor with get () = this :> ISetupTeardownExecutor<'outerInputType>

type TestCaseExecutor(parent: ITest, internals: ISetupTeardownExecutor<unit>) =
    let testLifecycleEvent = Event<TestExecutionDelegate, TestEventLifecycle> ()
    
    let handleEvents (cancelEventArgs: CancelEventArgs) (_sender: obj) args =
        match args with
        | ExecuteSetupStart eventArgs ->
            cancelEventArgs.Cancel <- eventArgs.Cancel
            testLifecycleEvent.Trigger (parent, TestStartSetup cancelEventArgs)
            eventArgs.Cancel <- cancelEventArgs.Cancel
        | ExecuteSetupEnd (result, eventArgs) ->
            cancelEventArgs.Cancel <- eventArgs.Cancel
            testLifecycleEvent.Trigger (parent, TestEndSetup (result, cancelEventArgs))
            eventArgs.Cancel <- cancelEventArgs.Cancel
        | ExecuteStartTeardown ->
            testLifecycleEvent.Trigger (parent, TestStartTeardown)
        | ExecuteRunner eventArgs ->
            cancelEventArgs.Cancel <- eventArgs.Cancel
            testLifecycleEvent.Trigger (parent, TestStart cancelEventArgs)
            eventArgs.Cancel <- cancelEventArgs.Cancel
        | ExecuteRunnerEnd testExecutionResult ->
            match testExecutionResult with
            | TestExecutionResult testResult -> testLifecycleEvent.Trigger (parent, TestEnd testResult)
            | _ -> ()
    
    let getApiEnvironment () =
        let assembly = System.Reflection.Assembly.GetExecutingAssembly ()
        let version = assembly.GetName().Version
        
        {
            ApiName = "Archer.Arrows"
            ApiVersion = version
        }
        
    member _.Execute environment =
        let env = 
            {
                ApiEnvironment = getApiEnvironment ()
                FrameworkEnvironment = environment
                TestInfo = parent 
            }
            
        let cancelEventArgs = CancelEventArgs ()
        let handler = handleEvents cancelEventArgs
        try
            testLifecycleEvent.Trigger (parent, TestStartExecution cancelEventArgs)
            if cancelEventArgs.Cancel then
                GeneralCancelFailure |> GeneralExecutionFailure
            else
                try
                    internals.LifecycleEvent.AddHandler handler
                        
                    try
                        let result = internals.Execute () env
                        
                        testLifecycleEvent.Trigger (parent, TestEndExecution result)
                        
                        result
                    with
                    | ex -> ex |> GeneralExceptionFailure |> GeneralExecutionFailure
                finally
                    internals.LifecycleEvent.RemoveHandler handler
        with
        | ex -> ex |> GeneralExceptionFailure |> GeneralExecutionFailure
        
    override _.ToString () =
        $"%s{parent.ToString ()}.IExecutor"
    
    interface ITestExecutor with
        member this.Parent = parent
        
        member this.Execute environment = this.Execute environment
        
        [<CLIEvent>]
        member this.TestLifecycleEvent = testLifecycleEvent.Publish

type TestCase (containerPath: string, containerName: string, testName: string, workings: ISetupTeardownExecutor<unit>, tags: TestTag seq, filePath: string, fileName: string,  lineNumber: int) =
    let location = {
        FilePath = filePath
        FileName = fileName
        LineNumber = lineNumber 
    }
    
    member _.ContainerPath with get () = containerPath
    member _.ContainerName with get () = containerName
    member _.TestName with get () = testName
    member _.Location with get () = location
    member _.Tags with get () = tags
        
    override _.ToString () =
        [
            containerPath
            containerName
            testName
        ]
        |> List.filter (String.IsNullOrWhiteSpace >> not)
        |> fun items -> String.Join (".", items)
    
    interface ITest with
        member this.ContainerName = this.ContainerName
        member this.ContainerPath = this.ContainerPath
        member this.GetExecutor() =
            
            TestCaseExecutor (this :> ITest, workings) :> ITestExecutor
        member this.Location = this.Location
        member this.Tags = this.Tags
        member this.TestName = this.TestName
        
type IBuilder<'a, 'b> =
    abstract member Add: internals: TestInternals * executor: ISetupTeardownExecutor<'a> -> 'b
    
type IScriptFeature<'featureType> =
    abstract member AsTest: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyWithEnvironmentIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    abstract member AsTest: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyWithEnvironmentIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    abstract member AsTest: tags: TagsIndicator * testBody: TestBodyWithEnvironmentIndicator<unit> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    abstract member AsTest: tags: TagsIndicator * testBody: TestBodyWithEnvironmentIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
        
    // --------- AsTest - Test Tags (without environment) ---------
    abstract member AsTest: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
        
    abstract member AsTest: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    abstract member AsTest: tags: TagsIndicator * testBody: TestBodyIndicator<unit> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    abstract member AsTest: tags: TagsIndicator * testBody: TestBodyIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
        
    // --------- AsTest - Setup (with environment) ---------
    abstract member AsTest: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyWithEnvironmentIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
        
    abstract member AsTest: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyWithEnvironmentIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    // --------- AsTest - Setup (without environment) ---------
    abstract member AsTest: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    abstract member AsTest: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    // --------- AsTest - TEST BODY (with environment) ---------
    abstract member AsTest: testBody: TestBodyWithEnvironmentIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    abstract member AsTest: testBody: TestBodyWithEnvironmentIndicator<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    abstract member AsTest: testBody: ('featureType -> TestEnvironment -> TestResult) * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    
    // --------- AsTest - TEST BODY (without environment) ---------
    abstract member AsTest: testBody: TestBodyIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    abstract member AsTest: testBody: TestBodyIndicator<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    abstract member AsTest: testBody: TestFunction<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> (string -> unit)
    

let baseTransformer<'featureType, 'a> (featureSetup: SetupIndicator<unit, 'featureType>) (featureTeardown: TeardownIndicator<'featureType>) (internals: TestInternals, inner: ISetupTeardownExecutor<'featureType>) =
    let (Setup setup) = featureSetup 
    let (Teardown teardown) = featureTeardown
        
    let executor = WrappedTeardownExecutor<unit,'featureType> (setup, teardown, inner)
    TestCase (internals.ContainerPath, internals.ContainerName, internals.TestName, executor, internals.Tags, internals.FilePath, internals.FileName, internals.LineNumber) :> ITest

type Feature<'featureType, 'xformType> (featurePath, featureName, transformer: TestInternals * ISetupTeardownExecutor<'featureType> -> 'xformType) =
    let mutable tests: 'xformType list = []
    
    let wrapTestBody (testBody: 'a -> TestResult) =
        TestWithEnvironmentBody (fun setupResult _env -> testBody setupResult)
        
    let wrapTeardown teardown =
        let (Teardown teardown) = teardown
        Teardown (fun _ -> teardown (Ok ()))
        
    interface IBuilder<'featureType, 'xformType> with
        member _.Add (internals: TestInternals, executor: ISetupTeardownExecutor<'featureType>) =
            let test = transformer (internals, executor)
            tests <- test::tests
            test
    
    // --------- TEST TAGS (with environment) ---------
    member _.Test (tags: TagsIndicator, setup: SetupIndicator<_, 'setupType>, testBody: TestBodyWithEnvironmentIndicator<'setupType>, teardown: TeardownIndicator<'setupType>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        let fileInfo = FileInfo fileFullName
        let filePath = fileInfo.Directory.FullName
        let fileName = fileInfo.Name
        
        let test =
            let TestTags tags, Setup setup, TestWithEnvironmentBody testBody, Teardown teardown = (tags, setup, testBody, teardown)
            let inner = SetupTeardownExecutor (setup, teardown, fun value env -> env |> testBody value |> TestExecutionResult) :> ISetupTeardownExecutor<'featureType>
            transformer ({ ContainerPath = featurePath; ContainerName = featureName; TestName = testName; Tags = tags; FilePath = filePath; FileName = fileName; LineNumber = lineNumber }, inner)
        
        tests <- test::tests
        test
    
    member this.Test (tags: TagsIndicator, testBody: TestBodyWithEnvironmentIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
    
    member this.Test (tags: TagsIndicator, setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Test (tags: TagsIndicator, testBody: 'featureType -> TestEnvironment -> TestResult, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, TestWithEnvironmentBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    // --------- TEST NAME / Tags (with environment) ---------
    member this.Test (testName: string, tags: TagsIndicator, setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, teardown: TeardownIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, testBody, teardown, testName, fileFullName, lineNumber)
    
    member this.Test (testName: string, tags: TagsIndicator, testBody: TestBodyWithEnvironmentIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, tags: TagsIndicator, setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, tags: TagsIndicator, testBody: 'featureType -> TestEnvironment -> TestResult, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, TestWithEnvironmentBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
    
    // --------- TEST TAGS (without environment) ---------
    member this.Test (testName: string, tags: TagsIndicator, TestBody testBody, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, wrapTestBody testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
        
    member this.Test (tags: TagsIndicator, TestBody testBody, teardown: TeardownIndicator<unit>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, wrapTestBody testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
    
    member this.Test (tags: TagsIndicator, setup: SetupIndicator<'featureType, 'a>, TestBody testBody, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Test (tags: TagsIndicator, testBody: 'featureType -> TestResult, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    // --------- TEST NAME / TAGS (without environment) ---------
    member this.Test (testName: string, tags: TagsIndicator, setup: SetupIndicator<'featureType,'a>, TestBody testBody, teardown: TeardownIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, wrapTestBody testBody, teardown, testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, tags: TagsIndicator, setup: SetupIndicator<'featureType, 'a>, TestBody testBody, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, setup, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, tags: TagsIndicator, testBody: 'featureType -> TestResult, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, tags: TagsIndicator, testBody: 'featureType -> TestResult, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (tags, Setup Ok, wrapTestBody testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
            
    // --------- SET UP (with environment) ---------
    member this.Test (setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, teardown: TeardownIndicator<'a>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, testBody, teardown, testName, fileFullName, lineNumber)
    
    member this.Test (setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
    
    // --------- TEST NAME / SET UP (with environment) ---------
    member this.Test (testName: string, setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, teardown: TeardownIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, testBody, teardown, testName, fileFullName, lineNumber)
    
    member this.Test (testName: string, setup: SetupIndicator<'featureType, 'a>, testBody: TestBodyWithEnvironmentIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
    
    // --------- SET UP (without environment) ---------
    member this.Test (setup: SetupIndicator<'featureType, 'a>, TestBody testBody, teardown: TeardownIndicator<'a>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, wrapTestBody testBody, teardown, testName, fileFullName, lineNumber)
        
    member this.Test (setup: SetupIndicator<'featureType, 'a>, TestBody testBody, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    // --------- TEST NAME / SET UP (without environment) ---------
    member this.Test (testName: string, setup: SetupIndicator<'featureType, 'a>, TestBody testBody, teardown: TeardownIndicator<'a>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, wrapTestBody testBody, teardown, testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, setup: SetupIndicator<'featureType, 'a>, TestBody testBody, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], setup, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    // --------- TEST BODY (with environment)---------
    member this.Test (testBody: TestBodyWithEnvironmentIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
    
    member this.Test (testBody: 'featureType -> TestEnvironment -> TestResult, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, TestWithEnvironmentBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
            
    // --------- TEST NAME / TEST BODY (with environment)---------
    member this.Test (testName: string, testBody: TestBodyWithEnvironmentIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
        
    member this.Test (testName: string, testBody: 'featureType -> TestEnvironment -> TestResult, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, TestWithEnvironmentBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
            
    // --------- TEST BODY (without environment)---------
    member this.Test (TestBody testBody, teardown: TeardownIndicator<unit>, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, wrapTestBody testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
        
    member this.Test (testBody: 'featureType -> TestResult, [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
            
    // --------- TEST NAME / TEST BODY (without environment)---------
    member this.Test (testName: string, TestBody testBody, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, wrapTestBody testBody, wrapTeardown teardown, testName, fileFullName, lineNumber)
            
    member this.Test (testName: string, testBody: 'featureType -> TestResult, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
        this.Test (TestTags [], Setup Ok, wrapTestBody testBody, Teardown (fun _ _ -> Ok ()), testName, fileFullName, lineNumber)
        
    member this.Script with get () = this :> IScriptFeature<'featureType>

    interface IScriptFeature<'featureType> with
        member this.AsTest (tags: TagsIndicator, setup: SetupIndicator<'featureType, 'setupType>, testBody: TestBodyWithEnvironmentIndicator<'setupType>, teardown: TeardownIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int): (string -> unit) =
            let buildTest (testName: string) =
                this.Test (tags, setup, testBody, teardown, testName, fileFullName, lineNumber)
                |> ignore
                
            buildTest

        member this.AsTest (tags: TagsIndicator, setup: SetupIndicator<_, 'setupType>, testBody: TestBodyWithEnvironmentIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, setup, testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
        
        member this.AsTest (tags: TagsIndicator, testBody: TestBodyWithEnvironmentIndicator<unit>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, Setup(fun _ -> Ok ()), testBody, teardown, fileFullName, lineNumber)
        
        member this.AsTest (tags: TagsIndicator, testBody: TestBodyWithEnvironmentIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, Setup(fun _ -> Ok ()), testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
            
        // --------- AsTest - Test Tags (without environment) ---------
        member this.AsTest (tags: TagsIndicator, setup: SetupIndicator<_, 'setupType>, testBody: TestBodyIndicator<'setupType>, teardown: TeardownIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            let testBody =
               match testBody with
               | TestBody f -> f
               
            this.Script.AsTest (tags, setup, wrapTestBody testBody, teardown, fileFullName, lineNumber)
            
        member this.AsTest (tags: TagsIndicator, setup: SetupIndicator<_, 'setupType>, testBody: TestBodyIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, setup, testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
        
        member this.AsTest (tags: TagsIndicator, testBody: TestBodyIndicator<unit>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, Setup (fun _ -> Ok ()), testBody, teardown, fileFullName, lineNumber)
        
        member this.AsTest (tags: TagsIndicator, testBody: TestBodyIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (tags, Setup (fun _ -> Ok ()), testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
            
        // --------- AsTest - Setup (with environment) ---------
        member this.AsTest (setup: SetupIndicator<_, 'setupType>, testBody: TestBodyWithEnvironmentIndicator<'setupType>, teardown: TeardownIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (TestTags [], setup, testBody, teardown, fileFullName, lineNumber)
            
        member this.AsTest (setup: SetupIndicator<_, 'setupType>, testBody: TestBodyWithEnvironmentIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (setup, testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
        
        // --------- AsTest - Setup (without environment) ---------
        member this.AsTest (setup: SetupIndicator<_, 'setupType>, testBody: TestBodyIndicator<'setupType>, teardown: TeardownIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            let testBody =
                match testBody with
                | TestBody f -> f
                
            this.Script.AsTest (setup, wrapTestBody testBody, teardown, fileFullName, lineNumber)
        
        member this.AsTest (setup: SetupIndicator<_, 'setupType>, testBody: TestBodyIndicator<'setupType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (setup, testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
        
        // --------- AsTest - TEST BODY (with environment) ---------
        member this.AsTest (testBody: TestBodyWithEnvironmentIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest ((Setup Ok), testBody, wrapTeardown teardown, fileFullName, lineNumber)
            
        member this.AsTest (testBody: TestBodyWithEnvironmentIndicator<'featureType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
            
        member this.AsTest (testBody: TestBodyWithEnvironment<'featureType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (TestWithEnvironmentBody testBody, fileFullName, lineNumber)
        
        // --------- AsTest - TEST BODY (without environment) ---------
        member this.AsTest (testBody: TestBodyIndicator<'featureType>, teardown: TeardownIndicator<unit>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest ((Setup Ok), testBody, wrapTeardown teardown, fileFullName, lineNumber)
            
        member this.AsTest (testBody: TestBodyIndicator<'featureType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (testBody, Teardown (fun _ _ -> Ok ()), fileFullName, lineNumber)
            
        member this.AsTest (testBody: TestFunction<'featureType>, [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string, [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int) =
            this.Script.AsTest (TestBody testBody, fileFullName, lineNumber)
        
    member this.GetTests () = tests
    
    override _.ToString () =
        [
            featurePath
            featureName
        ]
        |> List.filter (String.IsNullOrWhiteSpace >> not)
        |> fun items -> String.Join (".", items)
        
type Sub =
    static member Feature (subFeatureName, setup: SetupIndicator<'featureType, 'subFeatureType>, teardown: TeardownIndicator<'subFeatureType>, testBuilder: IScriptFeature<'subFeatureType> -> unit) =
        let buildIt (feature: Feature<'featureType, 'featureResultType>) =
            let builder = feature :> IBuilder<'featureType, 'featureResultType>
            
            let transformer (internals: TestInternals, executor: ISetupTeardownExecutor<'subFeatureType>) =
                let (Setup setup) = setup
                let (Teardown teardown) = teardown
                internals, (WrappedTeardownExecutor (setup, teardown, executor) :> ISetupTeardownExecutor<'featureType>)
            
            let subFeature = Feature<'subFeatureType, TestInternals * ISetupTeardownExecutor<'featureType>> (feature.ToString (), subFeatureName, transformer)
            
            subFeature :> IScriptFeature<'subFeatureType> |> testBuilder
            
            subFeature.GetTests ()
            |> List.map builder.Add
            |> ignore
            
        buildIt
        
    static member Feature (subFeatureName, setup: SetupIndicator<'featureType, 'subFeatureType>, testBuilder: IScriptFeature<'subFeatureType> -> unit) =
        Sub.Feature (subFeatureName, setup, Teardown (fun _ _ -> Ok ()), testBuilder)
        
    static member Feature (subFeatureName, teardown: TeardownIndicator<unit>, testBuilder: IScriptFeature<unit> -> unit) =
        Sub.Feature (subFeatureName, Setup (fun _ -> Ok ()), teardown, testBuilder)
        
    static member Feature (subFeatureName, testBuilder: IScriptFeature<unit> -> unit) =
        Sub.Feature (subFeatureName, Setup (fun _ -> Ok ()), Teardown (fun _ _ -> Ok ()), testBuilder)