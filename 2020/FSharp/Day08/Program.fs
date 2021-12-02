module Day08

open System
open System.IO
open AdventOfCode.Utilities

[<EntryPoint>]
let main argv =
    let lines = 
        File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.toArray

    0 // return an integer exit code
