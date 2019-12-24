module Day04

open System

module String =
    let characters (str : string) = str.ToCharArray ()

module Password =
    let isCorrectLength password =
        password 
        |> String.length
        |> ((=) 6)

    let containsADouble password =
        password
        |> Seq.windowed 2
        |> Seq.exists (Array.distinct >> Array.length >> ((=) 1))

    let neverDecreases password =
        password
        |> String.characters
        |> Seq.map (string >> int)
        |> Seq.windowed 2
        |> Seq.forall (fun pair -> pair.[0] <= pair.[1])

    let isValid password =
        isCorrectLength password
        && containsADouble password
        && neverDecreases password

    let next password =
        let next' = Int32.Parse password
                    |> ((+) 1)
                    |> string
                    |> String.characters
                    |> Seq.ofArray
                    |> Seq.map (string >> int)
                    |> Seq.toList

        let rec makeNeverDescending acc rest =
            match acc, rest with
            | [], n :: tail -> makeNeverDescending [n] tail
            | m :: _, n :: tail -> 
                if m <= n
                then makeNeverDescending (n :: acc) tail
                else
                    let repeated = List.init (List.length rest) (fun _ -> m)
                    makeNeverDescending (repeated @ acc) []
            | _, [] ->
                Seq.rev acc
                |> Seq.map string
                |> String.concat ""

        makeNeverDescending [] next'

let validPasswordsInRange low high =
    let high' = Int32.Parse high
    let rec f acc password =
        if Int32.Parse password > high'
        then acc
        else
            let acc' = if password |> Password.isValid
                       then (password :: acc)
                       else acc
            f acc' (password |> Password.next)

    f [] low

[<EntryPoint>]
let main argv =
    validPasswordsInRange "271973" "785961"
    |> List.length
    |> printfn "Number of valid passwords in range 271973-785961: %A"

    0 // return an integer exit code
