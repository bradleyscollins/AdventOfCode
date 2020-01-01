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

type OpCode =
| OpAdd
| OpMultiply
| OpInput
| OpOutput
| OpJumpIfTrue
| OpJumpIfFalse
| OpLessThan
| OpEquals
| OpHalt
module OpCode =
    let fromInt =
        function
        | 1 -> OpAdd
        | 2 -> OpMultiply
        | 3 -> OpInput
        | 4 -> OpOutput
        | 5 -> OpJumpIfTrue
        | 6 -> OpJumpIfFalse
        | 7 -> OpLessThan
        | 8 -> OpEquals
        | _ -> OpHalt

    let instructionLength =
        function
        | OpAdd -> 4
        | OpMultiply -> 4
        | OpInput -> 2
        | OpOutput -> 2
        | OpJumpIfTrue -> 3
        | OpJumpIfFalse -> 3
        | OpLessThan -> 4
        | OpEquals -> 4
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
        | Position  -> ReadFrom x

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

type Move =
| Increment of int
| JumpTo    of int
| Stop

let FALSE = 0
let TRUE  = 1
type Bool = False | True
let (|False|True|) = function
                     | 0 -> False
                     | _ -> True

type Instruction =
| Add         of length : int * left : Read  * right : Read  * save : Write
| Multiply    of length : int * left : Read  * right : Read  * save : Write
| Input       of length : int * save : Write
| Output      of length : int * load : Read
| JumpIfTrue  of length : int * value : Read * jumpTo : Read
| JumpIfFalse of length : int * value : Read * jumpTo : Read
| LessThan    of length : int * left : Read  * right : Read  * save : Write
| Equals      of length : int * left : Read  * right : Read  * save : Write
| Halt
module Instruction =
    let at index (memory : Memory) =
        let opcode, mode1, mode2, mode3 = memory.[index] |> Code.fromInt
        let length = opcode |> OpCode.instructionLength
        let segment = memory |> Array.skip index |> Array.take length

        match opcode with
        | OpAdd ->
            let left  = mode1 |> Read.cons segment.[1]
            let right = mode2 |> Read.cons segment.[2]
            let save  = mode3 |> Write.cons segment.[3]
            Add (length, left, right, save)

        | OpMultiply ->
            let left  = mode1 |> Read.cons segment.[1]
            let right = mode2 |> Read.cons segment.[2]
            let save  = mode3 |> Write.cons segment.[3]
            Multiply (length, left, right, save)

        | OpInput ->
            let save = mode1 |> Write.cons segment.[1]
            Input (length, save)

        | OpOutput ->
            let load = mode1 |> Read.cons segment.[1]
            Output (length, load)

        | OpJumpIfTrue ->
            let value  = mode1 |> Read.cons segment.[1]
            let jumpTo = mode2 |> Read.cons segment.[2]
            JumpIfTrue (length, value, jumpTo)

        | OpJumpIfFalse ->
            let value  = mode1 |> Read.cons segment.[1]
            let jumpTo = mode2 |> Read.cons segment.[2]
            JumpIfFalse (length, value, jumpTo)

        | OpLessThan ->
            let left  = mode1 |> Read.cons segment.[1]
            let right = mode2 |> Read.cons segment.[2]
            let save  = mode3 |> Write.cons segment.[3]
            LessThan (length, left, right, save)
            
        | OpEquals ->
            let left  = mode1 |> Read.cons segment.[1]
            let right = mode2 |> Read.cons segment.[2]
            let save  = mode3 |> Write.cons segment.[3]
            Equals (length, left, right, save)

        | _ -> Halt

    let executeWith (readIO : ReadIO) (writeIO : WriteIO) (memory : Memory) instruction =
        match instruction with
        | Add (length, left, right, save) ->
            let x = left  |> Read.execute memory
            let y = right |> Read.execute memory
            save |> Write.execute memory (x + y)
            Increment length

        | Multiply (length, left, right, save) ->
            let x = left  |> Read.execute memory
            let y = right |> Read.execute memory
            save |> Write.execute memory (x * y)
            Increment length

        | Input (length, save) ->
            let input = readIO ()
            save |> Write.execute memory input
            Increment length

        | Output (length, load) ->
            let output = load |> Read.execute memory
            writeIO output
            Increment length

        | JumpIfTrue (length, value, jumpTo) ->
            let x     = value  |> Read.execute memory
            let index = jumpTo |> Read.execute memory
            match x with
            | True -> JumpTo index
            | _    -> Increment length

        | JumpIfFalse (length, value, jumpTo) ->
            let x     = value  |> Read.execute memory
            let index = jumpTo |> Read.execute memory
            match x with
            | False -> JumpTo index
            | _     -> Increment length

        | LessThan (length, left, right, save) ->
            let x = left  |> Read.execute memory
            let y = right |> Read.execute memory
            let result = if x < y then TRUE else FALSE
            save |> Write.execute memory result
            Increment length

        | Equals (length, left, right, save) ->
            let x = left  |> Read.execute memory
            let y = right |> Read.execute memory
            let result = if x = y then TRUE else FALSE
            save |> Write.execute memory result
            Increment length

        | Halt -> Stop


module Computer =
    let runWith readIO writeIO memory = 
        let execute = Instruction.executeWith readIO writeIO memory

        let rec run' programCounter =
            let move = memory
                       |> Instruction.at programCounter
                       |> execute

            match move with
            | Stop -> ()
            | Increment n -> run' (programCounter + n)
            | JumpTo programCounter' -> run' programCounter'

        run' 0
        memory

    let run program =
        let readIO () =
            printf "Input: "
            Console.ReadLine () |> int

        let writeIO = printfn "Output: %d"

        runWith readIO writeIO program

[<EntryPoint>]
let main argv =
    let program = File.ReadAllLines "input.txt"
                  |> Seq.head
                  |> String.split ","
                  |> Array.map int

    Computer.run program
    |> printfn "Result:\n%A"

    0 // return an integer exit code
