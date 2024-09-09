module Day05

open System
open System.IO
open System.Text.RegularExpressions
open AdventOfCode.Utilities

type BoardingPass = string
module BoardingPass =
    let rowCode (boardingPass : BoardingPass) = boardingPass.[0..6]
    let colCode (boardingPass : BoardingPass) = boardingPass.[7..9]

    let rowCodeToBinary = String.replace "F" "0" >> String.replace "B" "1"
    let colCodeToBinary = String.replace "L" "0" >> String.replace "R" "1"

    let row = rowCode >> rowCodeToBinary >> Int32.parseFromBase 2
    let column = colCode >> colCodeToBinary >> Int32.parseFromBase 2

    let rowColumnToSeatId row col = (row * 8) + col

    let seatId boardingPass =
        rowColumnToSeatId (row boardingPass) (column boardingPass)

    let allSeatIds =
        let rows = seq [0..0b111_1111]
        let cols = seq [0..0b111]
        
        Seq.allPairs rows cols
        |> Seq.map (fun (r,c) -> rowColumnToSeatId r c)
        |> Set.ofSeq

[<EntryPoint>]
let main argv =
    let boardingPasses = 
        File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.filter (not << String.IsNullOrEmpty)
        |> Seq.toArray

    let seatIdsOnPlane =
        boardingPasses
        |> Seq.map BoardingPass.seatId
        |> Set.ofSeq

    let maxSeatId = seatIdsOnPlane |> Seq.max

    let seatIdsNotInList =
        Set.difference BoardingPass.allSeatIds seatIdsOnPlane
        |> Set.toList
    let seatsWithoutAdjacent =
        seatIdsNotInList
        |> Seq.sort
        |> Seq.windowed 3
        |> Seq.filter (fun seats -> (abs (seats.[0] - seats.[1])) > 1 && (abs (seats.[1] - seats.[2])) > 1)
        |> Seq.map (fun seats -> seats.[1])
        |> Seq.toList

    printfn "Maximum seat ID: %d" maxSeatId
    printfn "Seats without adjacent: %A" seatsWithoutAdjacent

    0 // return an integer exit code
