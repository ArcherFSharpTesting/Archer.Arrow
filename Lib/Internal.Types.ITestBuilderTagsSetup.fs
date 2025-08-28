namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Arrows
open Archer.CoreTypes.InternalTypes

type ITestBuilderTagsSetup<'featureType> =
    interface
        // -- test name, tags, setup, data, test body
        (*003*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'setupType, TestEnvironment>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*004*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'setupType>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        // -- test name, tags, setup, test body, teardown
        (*005*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*006*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        // -- test name, tags, setup, test body
        (*007*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*008*) abstract member Test: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    end
