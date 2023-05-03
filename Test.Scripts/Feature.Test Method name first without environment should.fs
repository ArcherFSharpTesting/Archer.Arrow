﻿module Archer.Arrows.Tests.Feature.``Test Method name first without environment should``

open Archer
open Archer.Arrows
open Archer.Arrows.Internal.Types
open Archer.Arrows.Tests
open Archer.MicroLang.Verification

let private feature = Arrow.NewFeature ()

// Tags, Setup, TestBody, Teardown 
let ``return an ITest with everything when everything is passed`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testTags = 
                [
                   Only
                   Category "My Category"
               ]
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestTags testTags,
                    Setup (fun _ -> Ok ()),
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo testTags
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run setup method passed to it when everything is passed`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    Setup monitor.CallSetup,
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.SetupWasCalled
            |> Should.BeTrue
            |> withMessage "Setup was not called"
        )
    )
    
let ``run the test method passed to it when everything is passed`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )
    
let ``run the teardown method passed to it when everything is passed`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TeardownWasCalled
            |> Should.BeTrue
            |> withMessage "teardown was not called"
        )
    )

// Tags, Setup, TestBody!
let ``return an ITest with everything when given no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testTags = 
                [
                   Only
                   Category "My Category"
               ]
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestTags testTags,
                    Setup (fun _ -> Ok ()),
                    TestBody (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo testTags
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run setup method passed to it when given no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    Setup monitor.CallSetup,
                    TestBody (fun _ -> TestSuccess),
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.SetupWasCalled
            |> Should.BeTrue
            |> withMessage "Setup was not called"
        )
    )
    
let ``run the test method passed to it when given no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )

// Tags, TestBody, Teardown!
let ``return an ITest with everything when given no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testTags = 
                [
                   Only
                   Category "My Category"
               ]
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestTags testTags,
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo testTags
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run the test method passed to it when given no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )
    
let ``run the teardown method passed to it when given no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TeardownWasCalled
            |> Should.BeTrue
            |> withMessage "teardown was not called"
        )
    )

// Tags, TestBody!
let ``return an ITest with everything when given no setup or teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testTags = 
                [
                   Only
                   Category "My Category"
               ]
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestTags testTags,
                    TestBody (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo testTags
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``return an ITest with everything when given no setup, teardown, or test body indicator`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testTags = 
                [
                   Only
                   Category "My Category"
               ]
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestTags testTags,
                    (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo testTags
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run the test method passed to it when given no setup, teardown, or test body indicator`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestTags [
                                Only
                                Category "My Category"
                            ],
                    monitor.CallTestActionWithoutEnvironment,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test fas not called"
        )
    )

// Setup, TestBody, Teardown
let ``return an ITest with everything when given no tags`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    Setup (fun _ -> Ok ()),
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo []
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run setup method passed to it when given no tags`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    Setup monitor.CallSetup,
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.SetupWasCalled
            |> Should.BeTrue
            |> withMessage "Setup was not called"
        )
    )
    
let ``run the test method passed to it when given no tags`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )
    
let ``run the teardown method passed to it when given no tags`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TeardownWasCalled
            |> Should.BeTrue
            |> withMessage "teardown was not called"
        )
    )

// Setup, TestBody
let ``return an ITest with everything when given no tags, no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    Setup (fun _ -> Ok ()),
                    TestBody (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo []
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run setup method passed to it when given no tags, no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    Setup monitor.CallSetup,
                    TestBody (fun _ -> TestSuccess),
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.SetupWasCalled
            |> Should.BeTrue
            |> withMessage "Setup was not called"
        )
    )
    
let ``run the test method passed to it when given no tags, no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    Setup monitor.CallSetup,
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )

// TestBody, Teardown
let ``return an ITest with everything when given no tags, no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestBody (fun _ -> TestSuccess),
                    Teardown (fun _ _ -> Ok ()),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo []
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run the test method passed to it when given no tags, no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )
    
let ``run the teardown method passed to it when given no tags, no setup`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    Teardown monitor.CallTeardown,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TeardownWasCalled
            |> Should.BeTrue
            |> withMessage "teardown was not called"
        )
    )

// TestBody
let ``return an ITest with everything when given no tags, no setup, no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    TestBody (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo []
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run the test method passed to it when given no tags, no setup, no teardown`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    TestBody monitor.CallTestActionWithoutEnvironment,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )

let ``return an ITest with everything when given no tags, no setup, no teardown, no test body indicator`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let testName = "My test"
            
            let fileName = "dog.bark"
            let path = "D:\\"
            let fullPath = $"%s{path}%s{fileName}"
            let lineNumber = 73
            
            let test =
                testFeature.Test (
                    testName,
                    (fun _ -> TestSuccess),
                    fullPath,
                    lineNumber
                )
        
            test.Tags
            |> Should.BeEqualTo []
            |> withMessage "Tags"
            |> andResult (
                test.TestName
                |> Should.BeEqualTo testName
                |> withMessage "TestName"
            )
            |> andResult (
                $"%s{test.ContainerPath}.%s{test.ContainerName}"
                |> Should.BeEqualTo (testFeature.ToString ())
                |> withMessage "Container Information"
            )
            |> andResult (
                test.Location.FilePath
                |> Should.BeEqualTo path
                |> withMessage "file path"
            )
            |> andResult (
                test.Location.FileName
                |> Should.BeEqualTo fileName
                |> withMessage "File Name"
            )
            |> andResult (
                test.Location.LineNumber
                |> Should.BeEqualTo lineNumber
                |> withMessage "Line Number"
            )
        )
    )
    
let ``run the test method passed to it when given no tags, no setup, no teardown, no test body indicator`` =
    feature.Test (
        Setup setupFeatureUnderTest,
        TestBody (fun (testFeature: IFeature<unit>) ->
            let monitor = Monitor<unit, unit> (Ok ())
            let test =
                testFeature.Test (
                    "My test",
                    monitor.CallTestActionWithoutEnvironment,
                    "D:\\dog.bark",
                    73
                )
                
            test.GetExecutor ()
            |> executeFunction
            |> runIt
            |> ignore
            
            monitor.TestWasCalled
            |> Should.BeTrue
            |> withMessage "test was not called"
        )
    )

let ``Test Cases`` = feature.GetTests ()