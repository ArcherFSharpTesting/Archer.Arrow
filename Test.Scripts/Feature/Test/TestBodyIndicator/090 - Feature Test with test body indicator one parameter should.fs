module Archer.Arrows.Tests.Feature.Test.TestBodyIndicator.``090 - Feature Test with test body indicator one parameter should``

open System
open Archer
open Archer.Arrows
open Archer.Arrows.Internal.Types
open Archer.Arrows.Tests
open Archer.Arrows.Tests.TestBuilders
open Archer.CoreTypes.InternalTypes
open Archer.MicroLang.Verification

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Feature"
        Category "Test"
    ],
    Setup setupFeatureUnderTest
)

let private rand = Random ()

let private getContainerName (test: ITest) =
    $"%s{test.ContainerPath}.%s{test.ContainerName}"

let ``Create a valid ITest`` =
    feature.Test (fun (_, testFeature: IFeature<string>) ->
        let (_, test), testName, (path, fileName, lineNumber) =
            TestBuilder.BuildTestWithTestBodyOneParameter testFeature

        test
        |> Should.PassAllOf [
            getTags >> Should.BeEqualTo [] >> withMessage "Test Tags"
            getTestName >> Should.BeEqualTo testName >> withMessage "Test Name"
            getFilePath >> Should.BeEqualTo path >> withMessage "File Path"
            getFileName >> Should.BeEqualTo fileName >> withMessage "File Name"
            getLineNumber >> Should.BeEqualTo lineNumber >> withMessage "Line Number"
            getContainerName >> Should.BeEqualTo (testFeature.ToString ()) >> withMessage "Container Name"
        ]
    )

let ``Call Test when executed`` =
    feature.Test (fun (featureSetupValue, testFeature: IFeature<string>) ->
        let (monitor, test), _, _ = TestBuilder.BuildTestWithTestBodyOneParameter testFeature

        test
        |> silentlyRunTest

        monitor
        |> Should.PassAllOf [
            numberOfTimesTestFunctionWasCalled >> Should.BeEqualTo 1 >> withFailureComment "Incorrect number of tests"

            verifyNoTestFunctionWasCalledWithData

            verifyAllTestFunctionsShouldHaveBeenCalledWithFeatureSetupValueOf featureSetupValue
            
            verifyNoTestFunctionWasCalledWithATestSetupValue
            
            verifyNoTestFunctionWasCalledWithTestEnvironment
        ]
        |> withMessage "Test was not called"
    )

let ``Test Cases`` = feature.GetTests ()