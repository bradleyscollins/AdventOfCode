module Day03.Test.Tests

open System
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities
open Day03

let testInput =
    [|
        "..##......."
        "#...#...#.."
        ".#....#..#."
        "..#.#...#.#"
        ".#...##..#."
        "..#.##....."
        ".#.#.#....#"
        ".#........#"
        "#.##...#..."
        "#...##....#"
        ".#..#...#.#"
    |]
    |> String.concat Environment.NewLine

[<Fact>]
let ``My other test`` () =
    let expected = [|
        "..##......."
        "#...#...#.."
        ".#....#..#."
        "..#.#...#.#"
        ".#...##..#."
        "..#.##....."
        ".#.#.#....#"
        ".#........#"
        "#.##...#..."
        "#...##....#"
        ".#..#...#.#"
    |]

    expected =! (testInput |> String.trim |> String.lines)

[<Fact>]
let ``My test`` () =
    "..##......." =! (testInput |> String.trim |> String.lines |> Array.head)

[<Fact>]
let ``My test 2`` () =
    [|'.'; '.'; '#'; '#'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '.'; '#'; '#'; '.'; '.'; '.'; '.'; '.'; '.'|] =! (testInput |> String.trim |> String.lines |> Array.head |> Seq.cycle |> Seq.take 21 |> Seq.toArray)

[<Fact>]
let ``My test 3`` () =
    let treeMap = testInput |> String.trim |> String.lines |> inputToTreeMap
    let slope = 1, 3
    [] =! (plotPath treeMap slope)
