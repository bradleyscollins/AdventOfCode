module Day02.Test.Tests

open System
open Xunit
open Swensen.Unquote
open Day02

module TestCase =
    let fromPair (a, b) = [| box a; box b |]
    let fromTriple (a, b, c) = [| box a; box b; box c |]

module PasswordRecord =

    [<Theory>]
    [<InlineData("1-3 a: abcde",     1, 3, 'a', "abcde")>]
    [<InlineData("1-3 b: cdefg",     1, 3, 'b', "cdefg")>]
    [<InlineData("2-9 c: ccccccccc", 2, 9, 'c', "ccccccccc")>]
    let ``parse parses a line of input into a policy and a password`` input num1 num2 token password =
        { Number1 = num1; Number2 = num2; Token = token; Password = password } =! PasswordRecord.parse input


module Policy =
    let ``Test cases for applyTo`` =
        [
            MinMaxBased (1, 3, 'a'),     "abcde",     Ok "'abcde' contains 'a' 1 time"
            MinMaxBased (1, 3, 'b'),     "cdefg",     Error "'cdefg' contains 'b' 0 times"
            MinMaxBased (2, 9, 'c'),     "ccccccccc", Ok "'ccccccccc' contains 'c' 9 times"

            PositionBased ([1; 3], 'a'), "abcde",     Ok "'abcde' contains 'a' at position 1"
            PositionBased ([1; 3], 'b'), "cdefg",     Error "'cdefg' does not contain 'b'"
            PositionBased ([2; 9], 'c'), "ccccccccc", Error "'ccccccccc' contains 'c' at positions 2 and 9"
        ]
        |> Seq.map TestCase.fromTriple

    [<Theory>]
    [<MemberData(nameof(``Test cases for applyTo``))>]
    let ``applyTo applies a policy to a password`` policy password expected =
        expected =! (policy |> Policy.applyTo password)
