module Day05.Tests

open System
open Xunit
open Swensen.Unquote


module Tuple2 =
    let toObjArray (x, y) = [| box x; box y |]

module Tuple3 =
    let toObjArray (x, y, z) = [| box x; box y; box z |]

let ``Mode.fromInt test cases`` =
    [
        0, Position
        1, Immediate
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Mode.fromInt test cases")>]
let ``Mode.fromInt converts an integer into a parameter mode`` input expected =
    ParameterMode.fromInt input =! expected


let ``Opcode.fromInt test cases`` =
    [
        1, OpAdd
        2, OpMultiply
        3, OpInput
        4, OpOutput
        99, OpHalt
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Opcode.fromInt test cases")>]
let ``Opcode.fromInt converts an integer into an opcode`` input expected =
    OpCode.fromInt input =! expected

let ``Opcode.instructionLength test cases`` =
    [
        OpAdd, 4
        OpMultiply, 4
        OpInput, 2
        OpOutput, 2
        OpHalt, 1
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Opcode.instructionLength test cases")>]
let ``Opcode.instructionLength gets the length of an instruction from its opcode`` input expected =
    OpCode.instructionLength input =! expected

    
let ``Code.fromInt test cases`` =
    [
        1, (OpAdd, Position, Position, Position)
        101, (OpAdd, Immediate, Position, Position)
        1001, (OpAdd, Position, Immediate, Position)
        10001, (OpAdd, Position, Position, Immediate)
        11101, (OpAdd, Immediate, Immediate, Immediate)
        2, (OpMultiply, Position, Position, Position)
        102, (OpMultiply, Immediate, Position, Position)
        1002, (OpMultiply, Position, Immediate, Position)
        10002, (OpMultiply, Position, Position, Immediate)
        11102, (OpMultiply, Immediate, Immediate, Immediate)
        3, (OpInput, Position, Position, Position)
        4, (OpOutput, Position, Position, Position)
        99, (OpHalt, Position, Position, Position)
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Code.fromInt test cases")>]
let ``Code.fromInt converts turns an integer into an instruction code`` (input, expected) =
    Code.fromInt input =! expected


[<Fact>]
let ``Computer.execute runs program in memory and returns the resulting memory`` =
    let program = [| 1101; 100; -1; 4; 0 |]
    let expected = [| 1101; 100; -1; 4; 99 |]
    test <@ Computer.execute program = expected @>
