open System

module List =
    let cartesian xs ys =
        xs |> List.collect (fun x -> ys |> List.map (fun y -> x, y))
    
module String =
    let trim (s : string) = s.Trim ()

    // Returns a list of tuples:
    //  - the first element is an alphanumeric character
    //  - the second is the number of times that character occurs in `s`
    let charCounts (s : string) =
        s.ToCharArray ()
        |> Seq.countBy id
        |> Seq.toList
    
    let zip (str1 : string) (str2 : string) =
        Array.zip (str1.ToCharArray ()) (str2.ToCharArray ())

let calcResult1 (codes : string list) =
    let withExactly n = snd >> ((=) n)
    let allWordCounts = codes |> Seq.map String.charCounts
    let with2 = allWordCounts
                |> Seq.filter (Seq.exists (withExactly 2))
                |> Seq.length
    let with3 = allWordCounts
                |> Seq.filter (Seq.exists (withExactly 3))
                |> Seq.length
    with2 * with3

let calcResult2 (codes : string list) =
    let charDiffCounts (str1 : string, str2 : string) =
        String.zip str1 str2
        |> Seq.filter (fun (x, y) -> x <> y)
        |> Seq.length
    let keepMatchingChars (str1 : string, str2 : string) = 
        String.zip str1 str2
        |> Seq.filter (fun (x, y) -> x = y)
        |> Seq.map fst
        |> Seq.toArray
        |> String
    let differentBy n (str1, str2) = n = charDiffCounts (str1, str2)

    List.cartesian codes codes
    |> Seq.filter (differentBy 1)
    |> Seq.map keepMatchingChars
    |> Seq.distinct
    |> Seq.toList

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.toList
    
    let result1 = calcResult1 input // 5478
    printfn "Result 1: %d" result1

    let result2 = calcResult2 input // ["qyzphxoiseldjrntfygvdmanu"]
    printfn "Result 2: %A" result2

    0 // return an integer exit code
