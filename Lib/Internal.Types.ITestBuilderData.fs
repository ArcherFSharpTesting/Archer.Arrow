namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer.Arrows
open Archer.CoreTypes.InternalTypes
open Archer.Arrows.Internal.Types

type ITestBuilderData<'featureType> =
    interface
        // -- test body
        (*035*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*036*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunctionTwoParameters<'dataType, 'featureType>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*037*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<TestFunction<'dataType>> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

        // -- test function    
        (*038*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*039*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        (*040*) abstract member Test: testName: string * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    end
