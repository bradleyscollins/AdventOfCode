module Day01.Test.Tests

open System
open Xunit
open Swensen.Unquote
open Day01

[<Fact>]
let ``pairThatSumsTo returns the first pair of expenses that sums to 2020`` () =
    let expenses = [| 1721; 979; 366; 299; 675; 1456 |]
    let expected = Some [ 1721; 299 ]

    expected =! (expenses |> pairThatSumsTo 2020)

[<Fact>]
let ``tripleThatSumsTo returns the first triple of expenses that sums to 2020`` () =
    let expenses = [| 1721; 979; 366; 299; 675; 1456 |]
    let expected = Some [ 979; 366; 675 ]

    expected =! (expenses |> tripleThatSumsTo 2020)
