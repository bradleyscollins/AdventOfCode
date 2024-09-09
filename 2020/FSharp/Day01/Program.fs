module Day01

open System
open System.IO

module String =
    let trim (s : string) = s.Trim ()

let cartesian2WithoutDiagonal expenses =
    let lastIndex = Array.length expenses - 1
    seq {
        for i = 0 to lastIndex do
            for j = 0 to lastIndex do
                if j <> i then
                    let a = expenses.[i]
                    let b = expenses.[j]
                    [ a; b ]
    }

let cartesian3WithoutDiagonal expenses =
    let lastIndex = Array.length expenses - 1
    seq {
        for i = 0 to lastIndex do
            for j = 0 to lastIndex do
                if j <> i then
                    for k = 0 to lastIndex do
                        if k <> i && k <> j then
                            let a = expenses.[i]
                            let b = expenses.[j]
                            let c = expenses.[k]
                            [ a; b; c ]
    }

let sumsTo sum xs =
    sum = Seq.sum xs

let pairThatSumsTo sum expenses =
    cartesian2WithoutDiagonal expenses
    |> Seq.filter (sumsTo 2020)
    |> Seq.tryHead

let tripleThatSumsTo sum expenses =
    cartesian3WithoutDiagonal expenses
    |> Seq.filter (sumsTo 2020)
    |> Seq.tryHead

[<EntryPoint>]
let main argv =
    let expenses =
        File.ReadAllLines "input.txt"
        |> Seq.map (String.trim >> int)
        |> Seq.toArray

    let total = 2020

    match expenses |> pairThatSumsTo total with
    | Some [x; y] -> printfn $"{x} + {y} = {total}. {x} × {y} = {x * y}"
    | _ -> eprintfn $"Could not find two transactions that add up to {total}"

    match expenses |> tripleThatSumsTo total with
    | Some [x; y; z] -> printfn $"{x} + {y} + {z} = {total}. {x} × {y} × {z} = {x * y * z}"
    | _ -> eprintfn $"Could not find three transactions that add up to {total}"

    0 // return an integer exit code
