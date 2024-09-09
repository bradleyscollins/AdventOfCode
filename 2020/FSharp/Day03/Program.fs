module Day03

open System.IO
open AdventOfCode.Utilities

let inputToTreeMap lines =
   lines
   |> Seq.map (String.trim >> String.chars >> Seq.cycle)
   |> Seq.toList
 

let plotPath treeMap slope =
    let y, x = slope

    let rec f' treeMap' yIncremental xTotal path =
        let restOfTreeMap = treeMap' |> Seq.drop yIncremental
        match restOfTreeMap |> Seq.tryHead with
        | None -> path |> List.rev
        | Some line ->
            let terrain = line |> Seq.skip xTotal |> Seq.head
            let path' = terrain :: path
            f' restOfTreeMap yIncremental (xTotal + x) path'
    
    f' treeMap y x []

let treesAlongPath = (Seq.filter ((=) '#')) >> Seq.length

let countTreesAlongPath treeMap slope =
    plotPath treeMap slope
    |> treesAlongPath

[<EntryPoint>]
let main argv =
    let treeMap = File.ReadAllLines "input.txt" |> inputToTreeMap
    let slope = 1, 3
    
    countTreesAlongPath treeMap slope
    |> printfn "%d trees along the path"

    let slopes = [
        1, 1
        1, 3
        1, 5
        1, 7
        2, 1
    ]
    
    slopes
    |> Seq.map (countTreesAlongPath treeMap)
    |> Seq.map int64
    |> Seq.product
    |> printfn "Product of trees along the path for given slopes: %d"

    0 // return an integer exit code
