open System

let calcResult1 (changes : int list) =
    changes |> Seq.sum

let calcResult2 (changes : int list) =
    Seq.initInfinite (fun _ -> changes)
    |> Seq.concat
    |> Seq.scan (fun (current, prev) delta ->
                    let next = current + delta
                    next, (prev |> Set.add current))
                (0, Set [])
    |> Seq.find (fun (current, prev) -> prev |> Set.contains current)
    |> fst

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadAllLines "input.txt"
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map int
        |> Seq.toList

    let result1 = calcResult1 input // 590
    printfn "Result 1: %d" result1

    let result2 = calcResult2 input // 83445
    printfn "Result 2: %A" result2

    printfn "\nPress any key ..."
    Console.ReadKey () |> ignore;

    0 // return an integer exit code
