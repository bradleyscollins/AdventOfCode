module Day07.Test.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities
open Day07

let ruleset1 =
    File.ReadAllLines "test-input-1.txt"
    |> Seq.map String.trim
    |> Seq.toArray

let ruleset2 =
    File.ReadAllLines "test-input-2.txt"
    |> Seq.map String.trim
    |> Seq.toArray

module RuleMap =
    [<Fact>]
    let ``build builds a map bag colors to the bag colors they contain`` () =
        let expected = Map [
            "light red",    Map ["bright white", 1; "muted yellow", 2]
            "dark orange",  Map ["bright white", 3; "muted yellow", 4]
            "bright white", Map ["shiny gold", 1]
            "muted yellow", Map ["shiny gold", 2; "faded blue", 9]
            "shiny gold",   Map ["dark olive", 1; "vibrant plum", 2]
            "dark olive",   Map ["faded blue", 3; "dotted black", 4]
            "vibrant plum", Map ["faded blue", 5; "dotted black", 6]
            "faded blue",   Map []
            "dotted black", Map []
        ]

        expected =! (ruleset1 |> RuleMap.build)

module Bag =
    [<Fact>]
    let ``contains indicates whether a bag ultimately contains a bag of a given color`` () =
        let ruleMap = ruleset1 |> RuleMap.build
        let vibrantPlum = ruleMap |> Bag.build <| "vibrant plum"
        false =! (vibrantPlum |> Bag.contains "shiny gold")

    [<Fact>]
    let ``can filter on contains`` () =
        let ruleMap = ruleset1 |> RuleMap.build
        let bags =
            ruleMap
            |> RuleMap.colors
            |> Seq.map (Bag.build ruleMap)
        4 =! (bags |> Seq.filter (Bag.contains "shiny gold") |> Seq.length)

    let ``size test cases`` = TestCase.pairs [
        ruleset1, 32
        ruleset2, 126
    ]

    [<Theory>]
    [<MemberData(nameof ``size test cases``)>]
    let ``size returns the number of bags within a bag of a given color`` ruleset expected =
        let bag = ruleset |> RuleMap.build |> Bag.build <| "shiny gold"
        expected =! (bag |> Bag.size)


