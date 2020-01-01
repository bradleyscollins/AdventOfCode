module Day05.Tests

open System
open Xunit
open Swensen.Unquote


module Tuple2 =
    let toObjArray (x, y) = [| box x; box y |]

module Tuple3 =
    let toObjArray (x, y, z) = [| box x; box y; box z |]

module Tuple4 =
    let toObjArray (w, x, y, z) = [| box w; box x; box y; box z |]
    
module Tuple5 =
    let toObjArray (v, w, x, y, z) = [| box v; box w; box x; box y; box z |]


let ``Parameter.fromInt test cases`` =
    [
        0, Position
        1, Immediate
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Parameter.fromInt test cases")>]
let ``Parameter.fromInt converts an integer into a parameter mode`` input expected =
    Parameter.fromInt input =! expected


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


let ``Instruction.at test cases`` = 
  [
    0, [| 1101;100;-1;4; 0 |], Add (4, Constant 100, Constant -1, WriteTo 4)
    4, [| 1101;100;-1;3; 102;4;1;8; 0 |], Multiply (4, Constant 4, ReadFrom 1, WriteTo 8)
    0, [| 3;4; 4;3; 0 |], Input (2, WriteTo 4)
    2, [| 3;4; 104;3; 0 |], Output (2, Constant 3)
    4, [| 1101;100;-1;4; 99 |], Halt ]
  |> Seq.map Tuple3.toObjArray

[<Theory>]
[<MemberData("Instruction.at test cases")>]
let ``Instruction.at produces the instruction in memory at the given index`` (index, memory, expected) =
    test <@ memory |> Instruction.at index = expected @>


let ``Instruction.exectuteWith test cases`` = 
   [
     Add (4, Constant 100, Constant -1, WriteTo 4),   [| 1101;100;-1;4; 0 |],            (Increment 4), [| 1101;100;-1;4; 99 |],             None
     Multiply (4, Constant 4, ReadFrom 1, WriteTo 8), [| 1101;100;-1;3; 102;4;1;8; 0 |], (Increment 4), [| 1101;100;-1;3; 102;4;1;8; 400 |], None
     Input (2, WriteTo 4),                            [| 3;4; 4;3; 0 |],                 (Increment 2), [| 3;4; 4;3; 42 |],                  None
     Output (2, Constant 3),                          [| 3;4; 104;3; 0 |],               (Increment 2), [| 3;4; 104;3; 0 |],                 Some 3
     Output (2, ReadFrom 1),                          [| 3;4; 4;1; 0 |],                 (Increment 2), [| 3;4; 4;1; 0 |],                   Some 4
     Halt,                                            [| 1101;100;-1;4; 99 |],           Stop,          [| 1101;100;-1;4; 99 |],             None ]
   |> Seq.map Tuple5.toObjArray

[<Theory>]
[<MemberData("Instruction.exectuteWith test cases")>]
let ``Instruction.executeWith produces the expected move operation, modifies memory as expected, and produces expected output``
    (move, memory, expectedLength, expectedMemoryAfterOperation, expectedIOOutput) =
        let readIO () = 42

        let mutable actualIOOutput = None
        let writeIO (n : int) = actualIOOutput <- Some n

    
        test <@ move |> Instruction.executeWith readIO writeIO memory = expectedLength @>
        expectedMemoryAfterOperation =! memory
        expectedIOOutput =! actualIOOutput

[<Fact>]
let ``Computer.run runs program in memory and returns the resulting memory`` =
    let program = [| 1101; 100; -1; 4; 0 |]
    let expected = [| 1101; 100; -1; 4; 99 |]
    test <@ Computer.run program = expected @>


let ``Computer.runWith test cases`` =
    let input8 () = 8

    [
      input8, [| 3;9; 8;9;10;9; 4;9; 99; -1;8 |], [| 3;9; 8;9;10;9; 4;9; 99; 1;8 |], (Some 1)
    ]
    |> Seq.map Tuple4.toObjArray


[<Theory>]
[<MemberData("Computer.runWith test cases")>]
let ``Computer.runWith mutates memory as expected and produces expected output``
    (readIO, program, expectedMemoryAfterRun, expectedOutput) =
        let mutable actualIOOutput = None
        let writeIO (n : int) = actualIOOutput <- Some n

        test <@ Computer.runWith readIO writeIO program = expectedMemoryAfterRun @>
        actualIOOutput =! expectedOutput

