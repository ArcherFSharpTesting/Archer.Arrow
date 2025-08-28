namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Arrows
open Archer.CoreTypes.InternalTypes
open Archer.Arrows.Internal.Types

type ITestBuilderTags<'featureType> =
    interface
        // -- test name, tags, test body, teardown
    (*018*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    (*019*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<TestFunction<'featureType>> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // -- test name, tags, test body
    (*020*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    (*021*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<TestFunction<'featureType>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // -- test name, tags, test function
    (*022*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    (*023*) abstract member Test: testName: string * tags: TagsIndicator * testBody: TestFunction<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    end
