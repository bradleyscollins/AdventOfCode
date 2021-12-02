namespace AdventOfCode.Utilities

open System
open System.Text.RegularExpressions

module Regex =
    let matches str (regex : Regex) =
        let match' = regex.Match str
        if match'.Success
        then Some match'
        else None

module Match =
    let groups (m : Match) = m.Groups

module Group =
    let value (g : Group) = g.Value

[<AutoOpen>]
module RegexPatterns =
    let (|ParseRegex|_|) pattern str =
        Regex (pattern, RegexOptions.Compiled)
        |> Regex.matches str
        |> Option.map (fun m -> List.tail [ for g in m.Groups -> g.Value ])
