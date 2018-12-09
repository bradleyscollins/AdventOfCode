open System

let calcResult1 (changes : int list) =
    changes |> Seq.sum

let calcResult2 (changes : int list) =
    Seq.initInfinite (fun _ -> changes)
    |> Seq.concat
    |> Seq.scan (fun (x :: xs) change -> (change + x) :: x :: xs) [0]
    |> Seq.find (fun (x :: xs) -> xs |> List.contains x)
    |> Seq.head

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadAllLines "input.txt"
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.map int
        |> Seq.toList

    let result1 = calcResult1 input // 590
    printfn "Result 1: %d" result1

    let result2 = calcResult2 input // Takes a long time, but
                                    // eventually produces 83445
    printfn "Result 2: %A" result2

    printfn "\nPress any key ..."
    Console.ReadKey () |> ignore;

    0 // return an integer exit code
