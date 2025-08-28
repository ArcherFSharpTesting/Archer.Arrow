namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Arrows
open Archer.CoreTypes.InternalTypes
open Archer.Arrows.Internal.Types

type ITestBuilder<'featureType> =
    interface
        inherit ITestBuilderBase<'featureType>
        inherit ITestBuilderNameTagsSetup<'featureType>
        inherit ITestBuilderNameTagsData<'featureType>
        inherit ITestBuilderNameTags<'featureType>
        inherit ITestBuilderNameSetup<'featureType>
        inherit ITestBuilderNameDataTeardown<'featureType>
        inherit ITestBuilderNameData<'featureType>
        inherit ITestBuilderName<'featureType>

        // -- tags, setup, data, test body, teardown
        (*047*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'setupType, TestEnvironment>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*048*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'setupType>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, setup, data, test body
        (*049*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'setupType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*050*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'setupType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, setup, test body, teardown
        (*051*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*052*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, setup, test body
        (*053*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*054*) abstract member Test: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, data, test body, teardown
        (*055*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*056*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*057*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, data, test body
        (*058*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*059*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*060*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, data, test function
        (*061*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*062*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*063*) abstract member Test: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, test body, teardown
        (*064*) abstract member Test: tags: TagsIndicator * testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*065*) abstract member Test: tags: TagsIndicator * testBody: TestBodyIndicator<TestFunction<'featureType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, test body
        (*066*) abstract member Test: tags: TagsIndicator * testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*067*) abstract member Test: tags: TagsIndicator * testBody: TestBodyIndicator<TestFunction<'featureType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, test function
        (*068*) abstract member Test: tags: TagsIndicator * testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*069*) abstract member Test: tags: TagsIndicator * testBody: TestFunction<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- setup, data, test body, teardown
        (*070*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'setupType, TestEnvironment>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*071*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'setupType>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- setup, data, test body
        (*072*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'setupType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*073*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'setupType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- setup, test body, teardown
        (*074*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*075*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- setup, test body
        (*076*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'setupType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*077*) abstract member Test: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<TestFunction<'setupType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- data, test body, teardown
        (*078*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*079*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*080*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- data, test body
        (*081*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*082*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*083*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- data, test function
        (*084*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*085*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*086*) abstract member Test: data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test body, teardown
        (*087*) abstract member Test: testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*088*) abstract member Test: testBody: TestBodyIndicator<TestFunction<'featureType>> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- test body
        (*089*) abstract member Test: testBody: TestBodyIndicator<TestFunctionTwoParameters<'featureType, TestEnvironment>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*090*) abstract member Test: testBody: TestBodyIndicator<TestFunction<'featureType>> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- test function
        (*091*) abstract member Test: testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*092*) abstract member Test: testBody: TestFunction<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    end