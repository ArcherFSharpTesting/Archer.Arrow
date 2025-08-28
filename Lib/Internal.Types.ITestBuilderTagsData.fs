namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Arrows
open Archer.CoreTypes.InternalTypes
open Archer.Arrows.Internal.Types

type ITestBuilderTagsData<'featureType> =
    interface
        // -- tags, data, test body, teardown
        (*055*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*056*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*057*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, data, test body
        (*058*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*059*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*060*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, data, test function
        (*061*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*062*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*063*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    end
