﻿open Archer.Arrows.Tests
open Archer.Arrows.Tests.Feature
open Archer.Arrows.Tests.RawTestObjects
open Archer.Bow
open Archer
open Archer.CoreTypes.InternalTypes
open Archer.CoreTypes.InternalTypes.RunnerTypes
open MicroLang.Lang

let runner = bow.Runner ()

let testFilter (_test: ITest) =
    true
    
runner.RunnerLifecycleEvent
|> Event.add (fun args ->
    match args with
    | RunnerStartExecution _ ->
        printfn ""
    | RunnerTestLifeCycle (test, testEventLifecycle, _) ->
        match testEventLifecycle with
        | TestEndExecution testExecutionResult ->
            let successMsg =
                match testExecutionResult with
                | TestExecutionResult TestSuccess -> "Success"
                | _ -> "Fail"
                
            let report = $"%A{test} : (%s{successMsg})"
            printfn $"%s{report}"
        | _ -> ()
    | RunnerEndExecution ->
        printfn "\n"
)

runner
|> addMany [
    ``Feature Should``.``Test Cases``
    ``TestCase Should``.``Test Cases``
    ``TestCaseExecutor Should``.``Test Cases``
    ``TestCaseExecutor Execute Should``.``Test Cases``
    ``TestCaseExecutor Events Should``.``Test Cases``
    ``TestCaseExecutor Event Cancellation Should``.``Test Cases``
    ``Arrow NewFeature``.``Test Cases``
    ``Arrow NewFeature With Setup``.``Test Cases``
    ``Arrow NewFeature With Teardown``.``Test Cases``
    ``Arrow Tests``.``Test Cases``
    ``Arrow Tests With Setup``.``Test Cases``
    ``Feature Ignore Should``.``Test Cases``
    ``Test Method without environment should``.``Test Cases``
    ``Test Method with environment should``.``Test Cases``
    ``Ignore Method without environment should``.``Test Cases``
    ``Ignore Method with environment should``.``Test Cases``
    ``Arrow Ignore``.``Test Cases``
    ``Test Method name first without environment should``.``Test Cases``
    ``Test Method name first with environment should``.``Test Cases``
    ``Ignore Method name first without environment should``.``Test Cases``
    ``Ignore Method name first with environment should``.``Test Cases``
]
|> filterRunAndReport testFilter
