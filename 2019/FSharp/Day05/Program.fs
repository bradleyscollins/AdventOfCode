module Day05

open System
open System.IO

module String =
    let padLeft totalWidth paddingChar (s : string) =
        s.PadLeft (totalWidth, paddingChar)

    let split (sep : string) (s : string) = s.Split sep

type Memory = int array
module Memory =
    let constant value (memory : Memory) = value
    let readFrom index (memory : Memory) = memory.[index]
    let writeTo index value (memory : Memory) = memory.[index] <- value

type OpCode = OpAdd | OpMultiply | OpInput | OpOutput | OpHalt
module OpCode =
    let fromInt = function
                  | 1 -> OpAdd
                  | 2 -> OpMultiply
                  | 3 -> OpInput
                  | 4 -> OpOutput
                  | _ -> OpHalt

    let instructionLength = function
                            | OpAdd -> 4
                            | OpMultiply -> 4
                            | OpInput -> 2
                            | OpOutput -> 2
                            | OpHalt -> 1

type ParameterMode = Position | Immediate
module ParameterMode =
    let fromInt = function
                  | 1 -> Immediate
                  | _ -> Position
    let reader = function
                 | Immediate -> Memory.constant
                 | Position -> Memory.readFrom

type Code = OpCode * ParameterMode * ParameterMode * ParameterMode
module Code =
    let fromInt (code : int) =
        let paddedCode = string code |> String.padLeft 5 '0'

        let param3Mode = paddedCode.[0..0] |> (int >> ParameterMode.fromInt)
        let param2Mode = paddedCode.[1..1] |> (int >> ParameterMode.fromInt)
        let param1Mode = paddedCode.[2..2] |> (int >> ParameterMode.fromInt)
        let opcode     = paddedCode.[3..4] |> (int >> OpCode.fromInt)

        opcode, param1Mode, param2Mode, param3Mode

type Read = Memory -> int
type Write = int -> Memory-> unit

type Instruction =
| Add of length : int * augend : Read * addend : Read * save : Write
| Multiply of length : int * multiplier : Read * multiplicand : Read * save : Write
| Input of length : int * save : Write
| Output of length : int * load : Read
| Halt
module Instruction =
    let atIndex index (memory : Memory) =
        let opcode, mode1, mode2, _ = memory.[index] |> Code.fromInt
        let length = opcode |> OpCode.instructionLength
        let segment = memory |> Array.skip index |> Array.take length
    
        match opcode with
        | OpAdd ->
            let augend = mode1 |> ParameterMode.reader <| segment.[1]
            let addend = mode2 |> ParameterMode.reader <| segment.[2]
            let save = Memory.writeTo segment.[3]
            Add (length, augend, addend, save)
        | OpMultiply ->
            let multiplier = mode1 |> ParameterMode.reader <| segment.[1]
            let multiplicand = mode2 |> ParameterMode.reader <| segment.[2]
            let save = Memory.writeTo segment.[3]
            Multiply (length, multiplier, multiplicand, save)
        | OpInput ->
            let save = Memory.writeTo segment.[1]
            Input (length, save)
        | OpOutput ->
            let load = mode1 |> ParameterMode.reader <| segment.[1]
            Output (length, load)
        | OpHalt -> Halt

    let length =
        function
        | Add (length, _, _, _) -> length
        | Multiply (length, _, _, _) -> length
        | Input (length, _) -> length
        | Output (length, _) -> length
        | Halt -> 1

    let execute (memory : Memory) op =
        match op with
        | Add (_, augend, addend, save) ->
            let x = memory |> augend
            let y = memory |> addend
            memory |> save (x + y)
        | Multiply (_, multiplier, multiplicand, save) ->
            let x = memory |> multiplier
            let y = memory |> multiplicand
            memory |> save (x * y)
        | Input (_, save) ->
            printf "Input: "
            let input = Console.ReadLine () |> int
            memory |> save input
        | Output (_, load) ->
            let output = memory |> load
            printfn "Output: %d" output
        | _ -> ()
    
        memory

module Computer =
    let execute program =
        let rec run memory programCounter =
            let instruction = memory |> Instruction.atIndex programCounter

            match instruction with
            | Halt -> memory
            | _ ->
                let memory' = instruction |> Instruction.execute memory
                let length = instruction |> Instruction.length
                run memory' (programCounter + length)

        run program 0

[<EntryPoint>]
let main argv =
    let program = File.ReadAllLines "input.txt"
                  |> Seq.head
                  |> String.split ","
                  |> Array.map int

    Computer.execute program
    |> printfn "Result:\n%A"

    0 // return an integer exit code
