module Archer.Arrows.Tests.Feature.Ignore.TestName.Setup.Data.``009 - Feature Ignore with test name, setup, data, test, teardown should``

open System
open Archer
open Archer.Arrows
open Archer.Arrows.Internal.Types
open Archer.Arrows.Tests
open Archer.Arrows.Tests.IgnoreBuilders
open Archer.CoreTypes.InternalTypes
open Archer.MicroLang.Verification

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Feature"
        Category "Ignore"
    ],
    Setup setupFeatureUnderTest
)

let private rand = Random ()

let private getContainerName (test: ITest) =
    $"%s{test.ContainerPath}.%s{test.ContainerName}"

let ``Create a valid ITest`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (_, tests), (_, data, testNameRoot), (path, fileName, lineNumber) =
            IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardownNameHints testFeature

        let name1, name2, name3 = IgnoreBuilder.GetTestNames (fun _ -> sprintf "%s %s" testNameRoot) data

        tests
        |> Should.PassAllOf [
            ListShould.HaveLengthOf 3 >> withMessage "Number of tests"

            ListShould.HaveAllValuesPassAllOf [
                getTags >> Should.BeEqualTo [] >> withMessage "Test Tags"
                getFilePath >> Should.BeEqualTo path >> withMessage "File Path"
                getFileName >> Should.BeEqualTo fileName >> withMessage "File Name"
                getContainerName >> Should.BeEqualTo (testFeature.ToString ()) >> withMessage "Container Name"
                getLineNumber >> Should.BeEqualTo lineNumber >> withMessage "Line Number"
            ]

            List.head >> getTestName >> Should.BeEqualTo name1 >> withMessage "Test Name"

            List.skip 1 >> List.head >> getTestName >> Should.BeEqualTo name2 >> withMessage "Test Name"

            List.last >> getTestName >> Should.BeEqualTo name3 >> withMessage "Test Name"
        ]
    )

let ``Create a test name with name hints and repeating data`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (_, tests), (_, data, testNameRoot), _ =
            IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardownNameHints (testFeature, true)

        let name1, name2, name3 = IgnoreBuilder.GetTestNames (fun i v -> sprintf "%s %s%s" testNameRoot v (if 0 = i then "" else $"^%i{i}")) data

        tests
        |> Should.PassAllOf [
            List.head >> getTestName >> Should.BeEqualTo name1 >> withMessage "Test Name"
            List.skip 1 >> List.head >> getTestName >> Should.BeEqualTo name2 >> withMessage "Test Name"
            List.last >> getTestName >> Should.BeEqualTo name3 >> withMessage "Test Name"
        ]
    )

let ``Create a test name with no name hints`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (_, tests), (data, testName), _ =
            IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardown testFeature

        let name1, name2, name3 = IgnoreBuilder.GetTestNames (fun _ -> sprintf "%s (%A)" testName) data

        tests
        |> Should.PassAllOf [
            List.head >> getTestName >> Should.BeEqualTo name1 >> withMessage "Test Name"
            List.skip 1 >> List.head >> getTestName >> Should.BeEqualTo name2 >> withMessage "Test Name"
            List.last >> getTestName >> Should.BeEqualTo name3 >> withMessage "Test Name"
        ]
    )

let ``Create a test name with no name hints same data repeated`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (_, tests), (data, testName), _ =
            IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardown (testFeature, true)

        let name1, name2, name3 = IgnoreBuilder.GetTestNames (fun i v -> sprintf "%s (%A)%s" testName v (if 0 = i then "" else $"^%i{i}")) data

        tests
        |> Should.PassAllOf [
            List.head >> getTestName >> Should.BeEqualTo name1 >> withMessage "Test Name"
            List.skip 1 >> List.head >> getTestName >> Should.BeEqualTo name2 >> withMessage "Test Name"
            List.last >> getTestName >> Should.BeEqualTo name3 >> withMessage "Test Name"
        ]
    )

let ``Not call setup when executed`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (monitor, tests), _, _ = IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardown testFeature

        tests
        |> silentlyRunAllTests

        monitor
        |> verifyNoSetupFunctionsHaveBeenCalled
    )

let ``Not call Test when executed`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (monitor, tests), _, _ = IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardown testFeature

        tests
        |> silentlyRunAllTests

        monitor
        |> verifyNoTeardownFunctionsHaveBeenCalled
    )
    
let ``Not call teardown when executed`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (monitor, tests), _, _ = IgnoreBuilder.BuildTestWithTestNameSetupDataTestBodyTeardown testFeature
            
        tests
        |> silentlyRunAllTests

        monitor
        |> verifyNoTeardownFunctionsHaveBeenCalled
    )

let ``Test Cases`` = feature.GetTests ()