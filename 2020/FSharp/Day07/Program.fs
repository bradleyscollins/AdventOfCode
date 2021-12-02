namespace Day07

open System.IO
open AdventOfCode.Utilities

module Program =
    [<EntryPoint>]
    let main argv =
        let lines = 
            File.ReadAllLines "input.txt"
            |> Seq.map String.trim
            |> Seq.toArray

        let ruleMap = RuleMap.build lines

        ruleMap
        |> RuleMap.colors
        |> Seq.map (Bag.build ruleMap)
        |> Seq.filter (Bag.contains "shiny gold")
        |> Seq.length
        |> printfn "Bags that can contain a shiny gold bag: %d"

        Bag.build ruleMap "shiny gold"
        |> Bag.size
        |> printfn "A single shiny gold bag must contain %d bags"

        0 // return an integer exit code
