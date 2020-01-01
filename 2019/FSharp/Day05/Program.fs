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

type Parameter = Position | Immediate
module Parameter =
    let fromInt = function
                  | 1 -> Immediate
                  | _ -> Position

type Code = OpCode * Parameter * Parameter * Parameter
module Code =
    let fromInt (code : int) =
        let paddedCode = string code |> String.padLeft 5 '0'

        let param3Mode = paddedCode.[0..0] |> (int >> Parameter.fromInt)
        let param2Mode = paddedCode.[1..1] |> (int >> Parameter.fromInt)
        let param1Mode = paddedCode.[2..2] |> (int >> Parameter.fromInt)
        let opcode     = paddedCode.[3..4] |> (int >> OpCode.fromInt)

        opcode, param1Mode, param2Mode, param3Mode

type ReadIO  = unit -> int
type WriteIO = int -> unit

type Read =
| Constant of value : int
| ReadFrom of index : int
module Read =
    let cons x parameter =
        match parameter with
        | Immediate -> Constant x
        | Position -> ReadFrom x

    let execute (memory : Memory) read =
        match read with
        | Constant value -> value
        | ReadFrom index -> memory.[index]

type Write =
| WriteTo of index : int
module Write =
    let cons x parameter =
        match parameter with
        | Position | _ -> WriteTo x

    let execute (memory : Memory) value write =
        match write with
        | WriteTo index -> memory.[index] <- value

type Instruction =
| Add of length : int * augend : Read * addend : Read * save : Write
| Multiply of length : int * multiplier : Read * multiplicand : Read * save : Write
| Input of length : int * save : Write
| Output of length : int * load : Read
| Halt
module Instruction =
    let at index (memory : Memory) =
        let opcode, mode1, mode2, mode3 = memory.[index] |> Code.fromInt
        let length = opcode |> OpCode.instructionLength
        let segment = memory |> Array.skip index |> Array.take length

        match opcode with
        | OpAdd ->
            let augend = mode1 |> Read.cons segment.[1]
            let addend = mode2 |> Read.cons segment.[2]
            let save   = mode3 |> Write.cons segment.[3]

            Add (length, augend, addend, save)

        | OpMultiply ->
            let multiplier   = mode1 |> Read.cons segment.[1]
            let multiplicand = mode2 |> Read.cons segment.[2]
            let save         = mode3 |> Write.cons segment.[3]

            Multiply (length, multiplier, multiplicand, save)

        | OpInput ->
            let save = mode1 |> Write.cons segment.[1]
            Input (length, save)

        | OpOutput ->
            let load = mode1 |> Read.cons segment.[1]
            Output (length, load)

        | _ -> Halt

    let executeWith (readio : ReadIO) (writeio : WriteIO) (memory : Memory) instruction =
        match instruction with
        | Add (length, augend, addend, save) ->
            let x = augend |> Read.execute memory
            let y = addend |> Read.execute memory
            save |> Write.execute memory (x + y)
            length
        | Multiply (length, multiplier, multiplicand, save) ->
            let x = multiplier |> Read.execute memory
            let y = multiplicand |> Read.execute memory
            save |> Write.execute memory (x * y)
            length
        | Input (length, save) ->
            let input = readio ()
            save |> Write.execute memory input
            length
        | Output (length, load) ->
            let output = load |> Read.execute memory
            writeio output
            length
        | Halt -> 0


module Computer =
    let run program =
        let memory = program

        let readio () =
            printf "Input: "
            Console.ReadLine () |> int

        let writeio = printfn "Output: %d"

        let execute = Instruction.executeWith readio writeio memory

        let rec run' programCounter =
            let instruction = memory |> Instruction.at programCounter

            match instruction with
            | Halt -> ()
            | _ ->
                let length = execute instruction
                run' (programCounter + length)

        run' 0
        memory

[<EntryPoint>]
let main argv =
    let program = File.ReadAllLines "input.txt"
                  |> Seq.head
                  |> String.split ","
                  |> Array.map int

    Computer.run program
    |> printfn "Result:\n%A"

    0 // return an integer exit code
