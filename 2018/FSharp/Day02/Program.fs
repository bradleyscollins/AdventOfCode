// https://adventofcode.com/2018/day/2

// --- Day 2: Inventory Management System ---
   
// You stop falling through time, catch your breath, and check the screen on
// the device. "Destination reached. Current Year: 1518. Current Location:
// North Pole Utility Closet 83N10." You made it! Now, to find those
// anomalies.
   
// Outside the utility closet, you hear footsteps and a voice. "...I'm not
// sure either. But now that so many people have chimneys, maybe he could
// sneak in that way?" Another voice responds, "Actually, we've been working
// on a new kind of suit that would let him fit through tight spaces like
// that. But, I heard that a few days ago, they lost the prototype fabric,
// the design plans, everything! Nobody on the team can even seem to remember
// important details of the project!"
   
// "Wouldn't they have had enough fabric to fill several boxes in the
// warehouse? They'd be stored together, so the box IDs should be similar.
// Too bad it would take forever to search the warehouse for two similar
// box IDs..." They walk too far away to hear any more.
   
// Late at night, you sneak to the warehouse - who knows what kinds of
// paradoxes you could cause if you were discovered - and use your fancy
// wrist device to quickly scan every box and produce a list of the likely
// candidates (your puzzle input).
   
// To make sure you didn't miss any, you scan the likely candidate boxes
// again, counting the number that have an ID containing exactly two of any
// letter and then separately counting those with exactly three of any
// letter. You can multiply those two counts together to get a rudimentary
// checksum and compare it to what your device predicts.
   
// For example, if you see the following box IDs:
   
//     abcdef contains no letters that appear exactly two or three times.
//     bababc contains two a and three b, so it counts for both.
//     abbcde contains two b, but no letter appears exactly three times.
//     abcccd contains three c, but no letter appears exactly two times.
//     aabcdd contains two a and two d, but it only counts once.
//     abcdee contains two e.
//     ababab contains three a and three b, but it only counts once.
   
// Of these box IDs, four of them contain a letter which appears exactly
// twice, and three of them contain a letter which appears exactly three
// times. Multiplying these together produces a checksum of 4 * 3 = 12.
   
// What is the checksum for your list of box IDs?

// Your puzzle answer was 5478.

// --- Part Two ---
   
// Confident that your list of box IDs is complete, you're ready to find the
// boxes full of prototype fabric.
   
// The boxes will have IDs which differ by exactly one character at the same
// position in both strings. For example, given the following box IDs:
   
// abcde
// fghij
// klmno
// pqrst
// fguij
// axcye
// wvxyz
   
// The IDs abcde and axcye are close, but they differ by two characters (the
// second and fourth). However, the IDs fghij and fguij differ by exactly one
// character, the third (h and u). Those must be the correct boxes.
   
// What letters are common between the two correct box IDs? (In the example
// above, this is found by removing the differing character from either ID,
// producing fgij.)

// Your puzzle answer was qyzphxoiseldjrntfygvdmanu.


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
