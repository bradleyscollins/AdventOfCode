module Day05.Test.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities
open Day05

module BoardingPass =

    [<Theory>]
    [<InlineData("BFFFBBFRRR", "BFFFBBF")>]
    [<InlineData("FFFBBBFRRR", "FFFBBBF")>]
    [<InlineData("BBFFBBFRLL", "BBFFBBF")>]
    let ``rowCode gets the row code from a boarding pass`` boardingPass expected =
        expected =! BoardingPass.rowCode boardingPass

    [<Theory>]
    [<InlineData("BFFFBBFRRR", "RRR")>]
    [<InlineData("FFFBBBFRRR", "RRR")>]
    [<InlineData("BBFFBBFRLL", "RLL")>]
    let ``colCode gets the column code from a boarding pass`` boardingPass expected =
        expected =! BoardingPass.colCode boardingPass

    [<Theory>]
    [<InlineData("BFFFBBF", "1000110")>]
    [<InlineData("FFFBBBF", "0001110")>]
    [<InlineData("BBFFBBF", "1100110")>]
    let ``rowCodeToBinary converts a row code to its binary representation`` rowCode expected =
        expected =! BoardingPass.rowCodeToBinary rowCode

    [<Theory>]
    [<InlineData("RRR", "111")>]
    [<InlineData("RLL", "100")>]
    let ``colCodeToBinary converts a column code to its binary representation`` colCode expected =
        expected =! BoardingPass.colCodeToBinary colCode

    [<Theory>]
    [<InlineData("BFFFBBFRRR", 70)>]
    [<InlineData("FFFBBBFRRR", 14)>]
    [<InlineData("BBFFBBFRLL", 102)>]
    let ``row gets the row number from a boarding pass`` boardingPass expected =
        expected =! BoardingPass.row boardingPass

    [<Theory>]
    [<InlineData("FFFBBBFRRR", 7)>]
    [<InlineData("BBFFBBFRLL", 4)>]
    let ``column gets the column number from a boarding pass`` boardingPass expected =
        expected =! BoardingPass.column boardingPass

    [<Theory>]
    [<InlineData("BFFFBBFRRR", 567)>]
    [<InlineData("FFFBBBFRRR", 119)>]
    [<InlineData("BBFFBBFRLL", 820)>]
    let ``seatId converts a boarding pass to a seat ID`` boardingPass expected =
        expected =! BoardingPass.seatId boardingPass
