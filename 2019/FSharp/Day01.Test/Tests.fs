module Day01.Tests

open System
open Xunit
open Swensen.Unquote

[<Theory>]
[<InlineData(2, 0)>]
[<InlineData(12, 2)>]
[<InlineData(14, 2)>]
[<InlineData(1969, 654)>]
[<InlineData(100756, 33583)>]
let ``calcFuelForMass calculates the fuel to lift a given mass`` mass expected =
    calcFuelForMass mass =! expected

[<Theory>]
[<InlineData(12, 2)>]
[<InlineData(14, 2)>]
[<InlineData(1969, 966)>]
[<InlineData(100756, 50346)>]
let ``calcModuleFuel calculates the total fuel to lift a given mass including its own fuel`` mass expected =
    calcModuleFuel mass =! expected
