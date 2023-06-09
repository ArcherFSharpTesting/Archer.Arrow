﻿namespace Archer.Arrows

open System
open Archer

type ApiEnvironment = {
    ApiName: string
    ApiVersion: Version
}

type TestEnvironment = {
    RunEnvironment: RunnerEnvironment
    ApiEnvironment: ApiEnvironment
    TestInfo: ITestInfo
}

type TagsIndicator = | TestTags of TestTag list

type SetupIndicator<'a, 'b> = | Setup of ('a -> Result<'b, SetupTeardownFailure>)

type DataIndicator<'a> = | Data of 'a seq

type TestFunction<'a> = 'a -> TestResult
type TestFunctionTwoParameters<'a, 'b> = 'a -> 'b -> TestResult
type TestFunctionThreeParameters<'a, 'b, 'c> = 'a -> 'b -> 'c -> TestResult

type TestBodyIndicator<'a> = | TestBody of 'a 

type TeardownIndicator<'a> = | Teardown of (Result<'a, SetupTeardownFailure> -> TestResult option -> Result<unit, SetupTeardownFailure>)
