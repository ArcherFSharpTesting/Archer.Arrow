﻿namespace Archer.Arrows.Internal.Types

open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open Archer
open Archer.Arrows
open Archer.CoreTypes.InternalTypes
        
type IFeature<'featureType> =
    abstract member FeatureTags: TestTag list with get
    abstract member GetTests: unit -> ITest list
    
    // --------- TEST TAGS (with environment) ---------
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   tags: TagsIndicator * testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Test:   tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

        
    // --------- TEST NAME / Tags (with environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    // --------- TEST TAGS (without environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    abstract member Test:   tags: TagsIndicator * testBody: TestBodyIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        
    abstract member Test:   tags: TagsIndicator * testBody: TestFunction<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
        
    abstract member Test:   tags: TagsIndicator * testBody: TestBodyIndicator<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    // --------- TEST NAME / TAGS (without environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType,'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestFunction<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestBodyIndicator<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * tags: TagsIndicator * testBody: TestFunction<'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- SET UP (with environment) ---------
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- TEST NAME / SET UP (with environment) ---------
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- SET UP (without environment) ---------
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- TEST NAME / SET UP (without environment) ---------
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- TEST BODY (with environment)---------
    abstract member Test:   testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // --------- TEST NAME / TEST BODY (with environment)---------
    abstract member Test:   testName: string * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * testBody: TestFunctionTwoParameters<'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    // --------- TEST BODY (without environment)---------
    abstract member Test:   testBody: TestBodyIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testBody: TestFunction<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testBody: TestBodyIndicator<'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    // --------- TEST NAME / TEST BODY (without environment)---------
    abstract member Test:   testName: string * testBody: TestBodyIndicator<'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * testBody: TestFunction<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    
    abstract member Test:   testName: string * testBody: TestBodyIndicator<'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    // -------------------------------------------------
    // ---------       Data Driven Tests       ---------
    // -------------------------------------------------
    // --------- TEST TAGS (with environment) ---------
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    // --------- TEST NAME / Tags (with environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- TEST TAGS (without environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

// --------- Data and Environment ignore setup ---------
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
        
    abstract member Test:   tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- TEST NAME / TAGS (without environment) ---------
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType,'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Test:   testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- SET UP (with environment) ---------
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- TEST NAME / SET UP (with environment) ---------
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- SET UP (without environment) ---------
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- TEST NAME / SET UP (without environment) ---------
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    // --------- TEST BODY (with environment)---------
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    // --------- TEST NAME / TEST BODY (with environment)---------
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    // --------- TEST BODY (without environment)---------
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    // --------- TEST NAME / TEST BODY (without environment)---------
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Test:   testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    
    abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType>                               * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType>                               * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * tags: TagsIndicator * testBody: TestFunction<'featureType>                                    * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * tags: TagsIndicator * testBody: TestFunction<'featureType>                                    * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType>                                                    * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType>                                                    * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType>                               * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType>                               * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * testBody: TestBodyIndicator<'featureType>                               * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * testBody: TestBodyIndicator<'featureType>                               * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * teardown: TeardownIndicator<'setupType> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType>                                               * teardown: TeardownIndicator<unit> * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: testName: string * data: DataIndicator<'dataType> * testBody: TestFunction<'dataType>                                                    * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicator<'setupType>                               * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: tags: TagsIndicator * setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: tags: TagsIndicator * testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: tags: TagsIndicator * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * testBody: TestBodyIndicatorTwoParameters<'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * teardown: TeardownIndicator<'setupType> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    



    



    abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'setupType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: setup: SetupIndicator<'featureType, 'setupType> * data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'setupType>                    * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Ignore: testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testBody: TestBodyIndicator<'featureType>                               * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    abstract member Ignore: testBody: TestFunctionTwoParameters<'featureType, TestEnvironment>      * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testBody: TestBodyIndicatorTwoParameters<'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testBody: TestFunction<'featureType>                                    * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest
    abstract member Ignore: testBody: TestBodyIndicator<'featureType>                               * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest

    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType>                    * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment>                 * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType>                                               * teardown: TeardownIndicator<unit> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestFunctionThreeParameters<'dataType, 'featureType, TestEnvironment>      * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorThreeParameters<'dataType, 'featureType, TestEnvironment> * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, 'featureType>                         * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestFunctionTwoParameters<'dataType, TestEnvironment>                      * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestFunction<'dataType>                                                    * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, 'featureType>                    * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicatorTwoParameters<'dataType, TestEnvironment>                 * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list
    abstract member Ignore: data: DataIndicator<'dataType> * testBody: TestBodyIndicator<'dataType>                                               * [<CallerMemberName; Optional; DefaultParameterValue("")>] testName: string * [<CallerFilePath; Optional; DefaultParameterValue("")>] fileFullName: string * [<CallerLineNumber; Optional; DefaultParameterValue(-1)>] lineNumber: int -> ITest list

