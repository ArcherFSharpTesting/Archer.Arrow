namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer
open Archer.Arrows
open Archer.CoreTypes.InternalTypes

type IFeature<'featureType> =
    interface
        inherit ITestBuilder<'featureType>
        abstract member FeatureTags: TestTag list with get
        abstract member GetTests: unit -> ITest list

        //-------------------------------------------------------//
        //                    Ignore Builders                    //
        //-------------------------------------------------------//

        // -- test name, tags, setup, data, test, (teardown)
        (*001*)abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*002*)abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test name, tags, setup, test, (teardown)
        (*003*)abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*004*)abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- test name, tags, data, test, (teardown)
        (*005*)abstract member Ignore: testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*006*)abstract member Ignore: testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test name, tags, test, (teardown)
        (*007*)abstract member Ignore: testName: string * tags: TagsIndicator * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*008*)abstract member Ignore: testName: string * tags: TagsIndicator * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- test name, setup, data, test, (teardown)
        (*009*)abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*010*)abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test name, setup, test, (teardown)
        (*011*)abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*012*)abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- test name, data, test, (teardown)
        (*013*)abstract member Ignore: testName: string * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*014*)abstract member Ignore: testName: string * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test name, test, (teardown)
        (*015*)abstract member Ignore: testName: string * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*016*)abstract member Ignore: testName: string * test: 'testBodyType * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, setup, data, test, (teardown)
        (*017*)abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*018*)abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, setup, test, (teardown)
        (*019*)abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*020*)abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- tags, data, test, (teardown)
        (*021*)abstract member Ignore: tags: TagsIndicator * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*022*)abstract member Ignore: tags: TagsIndicator * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- tags, test, (teardown)
        (*023*)abstract member Ignore: tags: TagsIndicator * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*024*)abstract member Ignore: tags: TagsIndicator * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- setup, data, test, (teardown)
        (*025*)abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*026*)abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- setup, test, (teardown)
        (*027*)abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*028*)abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        // -- data, test, (teardown)
        (*029*)abstract member Ignore: data: DataIndicator<'dataType> * test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*030*)abstract member Ignore: data: DataIndicator<'dataType> * test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test, (teardown)
        (*031*)abstract member Ignore: test: 'testBodyType * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        (*032*)abstract member Ignore: test: 'testBodyType * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    end