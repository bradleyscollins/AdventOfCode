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

    let consecutiveDigits password =
        let accumulate x acc =
            match acc with
            | (digit, count) :: tail when digit = x ->
                (digit, count + 1) :: tail
            | _ -> (x, 1) :: acc

        password
        |> String.characters
        |> Seq.foldBack accumulate <| []

    let containsAProperDouble password =
        password
        |> consecutiveDigits
        |> Seq.exists (snd >> ((=) 2))

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

    let isValid2 password =
        isCorrectLength password
        && containsAProperDouble password
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


let validPasswordsInRangeWith isValid low high =
    let high' = Int32.Parse high
    let rec f acc password =
        if Int32.Parse password > high'
        then acc
        else
            let acc' = if password |> isValid
                       then (password :: acc)
                       else acc
            f acc' (password |> Password.next)

    f [] low

[<EntryPoint>]
let main argv =
    validPasswordsInRangeWith Password.isValid "271973" "785961"
    |> List.length
    |> printfn "Number of valid passwords in range 271973-785961: %A"

    validPasswordsInRangeWith Password.isValid2 "271973" "785961"
    |> List.length
    |> printfn "Number of valid passwords in range 271973-785961 that meet new criteria: %A"

    0 // return an integer exit code
