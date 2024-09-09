module Day06.Test.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities
open Day06

let testInput =
    File.ReadAllLines "test-input.txt"
    |> Seq.map String.trim
    |> Seq.toArray

[<Fact>]
let ``countGroupAffirmatives counts the number of questions to which anyone answered 'yes'`` () =
    11 =! countGroupAffirmatives testInput
    
[<Fact>]
let ``countUnanimousGroupAffirmatives counts the number of questions to which anyone answered 'yes'`` () =
    6 =! countUnanimousGroupAffirmatives testInput
