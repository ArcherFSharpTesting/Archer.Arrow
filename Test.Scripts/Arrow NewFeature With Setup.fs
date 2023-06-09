module Archer.Arrows.Tests.``Arrow NewFeature With Setup``

open Archer
open Archer.Arrows
open Archer.Arrows.Internals
open Archer.MicroLang.Verification

let private feature = Arrow.NewFeature (
    TestTags [
        Category "Arrow"
        Category "NewFeature"
        Category "Feature"
        Category "Test"
    ]
)

let ``Should call setup when it is the only thing passed to it`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
            
        let testFeature = Arrow.NewFeature (
            Setup (monitor.FunctionSetupWith setupValue)
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )

let ``Should call setup when name and setup are specified`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
            
        let testFeature = Arrow.NewFeature (
            "My magic feature",
            Setup (monitor.FunctionSetupWith setupValue)
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )

let ``Should call setup when path, name, and setup are specified`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
            
        let testFeature = Arrow.NewFeature (
            "A Path",
            "My magic feature",
            Setup (monitor.FunctionSetupWith setupValue)
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )
    
let ``Should call setup when it is passed with a teardown`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
        
        let testFeature = Arrow.NewFeature (
            Setup (monitor.FunctionSetupWith setupValue),
            emptyTeardown
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )
    
let ``Should call setup when name, setup, and teardown are specified`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
        
        let testFeature = Arrow.NewFeature (
            "Hello Feature",
            Setup (monitor.FunctionSetupWith setupValue),
            emptyTeardown
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )
    
let ``Should call setup when path, name, setup, and teardown are specified`` =
    feature.Test (fun _ ->
        let monitor = getFeatureMonitor<unit> ()
        let setupValue = ()
        
        let testFeature = Arrow.NewFeature (
            "Hello Path",
            "Hello Feature",
            Setup (monitor.FunctionSetupWith setupValue),
            emptyTeardown
        )
        
        let test = testFeature.Test (fun _ -> TestSuccess)
        
        test
        |> silentlyRunTest
        
        monitor.HasSetupFunctionBeenCalled
        |> Should.BeTrue
        |> withMessage "Setup didn't run"
    )

let ``Test Cases`` = feature.GetTests ()