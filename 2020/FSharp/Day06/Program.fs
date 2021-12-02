module Day06

open System
open System.IO
open System.Text.RegularExpressions
open AdventOfCode.Utilities

let groupAnswers lines =
    lines
    |> Seq.foldBack (fun line groups ->
        if line |> String.IsNullOrEmpty then
            [] :: groups
        else
            match groups with
            | group :: tail -> (line :: group) :: tail
            | _             -> [[]])
    <| [[]]
    |> List.filter (not << List.isEmpty)


let countGroupAffirmatives lines =
    let answerGroups = groupAnswers lines

    let consolidateGroupAnswers =
        answerGroups
        |> Seq.map ((String.concat String.Empty) >> String.chars >> set)

    let sumOfCounts =
        consolidateGroupAnswers
        |> Seq.map Set.count
        |> Seq.sum

    sumOfCounts

let countUnanimousGroupAffirmatives lines =
    let unanimousAffirmatives group =
        group
        |> Seq.map (String.chars >> set)
        |> Set.intersectMany
        |> Set.count
    
    let answerGroups = groupAnswers lines

    let sumOfGroupAffirmatives =
        answerGroups
        |> Seq.map unanimousAffirmatives
        |> Seq.sum

    sumOfGroupAffirmatives

[<EntryPoint>]
let main argv =
    let lines = 
        File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.toArray

    printfn "Total number of group affirmatives: %d" (countGroupAffirmatives lines)
    printfn "Total number of group unanimous affirmatives: %d" (countUnanimousGroupAffirmatives lines)

    0 // return an integer exit code
