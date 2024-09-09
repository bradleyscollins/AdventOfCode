module AdventOfCode.Utilities.Test.Tests

open System
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities

[<Fact>]
let ``Seq drop`` () =
    Seq.empty =! (seq [1..5] |> Seq.drop 6)
