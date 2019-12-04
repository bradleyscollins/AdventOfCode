module Day02

open System.IO

type Program = int array

type Instruction =
| Add of augendIdx : int * addendIdx : int * resultIdx : int
| Mult of multiplierIdx : int * multiplicandIdx : int * resultIdx : int
| Halt


let toInstruction chunk =
    match chunk with
    | [| 1; augendIdx; addendIdx; resultIdx |] ->
        Add (augendIdx, addendIdx, resultIdx)
    | [| 2; multiplierIdx; multiplicandIdx; resultIdx |] ->
        Mult (multiplierIdx, multiplicandIdx, resultIdx)
    | _ -> Halt

let parse (intcode : string) = intcode.Split ','
                               |> Array.map int

let toIntcode program = program
                        |> Seq.map string
                        |> String.concat ","

let initializeWith changes intcode =
    let program = parse intcode

    changes
    |> Seq.iter (fun (i, newValue) -> program.[i] <- newValue)

    toIntcode program

let executeOn (program : Program) instruction =
    match instruction with
    | Add (augendIdx, addendIdx, i) ->
        program.[i] <- program.[augendIdx] + program.[addendIdx]
    | Mult (multiplierIdx, multiplicandIdx, i) ->
        program.[i] <- program.[multiplierIdx] * program.[multiplicandIdx]
    | _ -> ()

    program

let execute intcode =
    let program = parse intcode
    
    program
    |> Seq.ofArray
    |> Seq.chunkBySize 4
    |> Seq.map toInstruction
    |> Seq.takeWhile ((<>) Halt)
    |> Seq.fold executeOn program
    |> toIntcode

let runWith noun verb intcode =
    intcode
    |> initializeWith [ (1, noun); (2, verb) ]
    |> execute
    |> parse
    |> Array.head

let run intcode = intcode |> runWith 12 2

let yields output intcode =
    let rec tryRunWith noun verb =
        let noun', verb' = if noun > 99
                           then 0, verb + 1
                           else noun, verb

        if intcode |> runWith noun' verb' = output
        then noun', verb'
        else tryRunWith (noun' + 1) verb'
    
    tryRunWith 0 0

[<EntryPoint>]
let main argv =
    let intcode = (File.ReadAllText "input.txt").Trim ()

    printfn "Output of noun (12) and verb (2): %A" (run intcode)
    
    intcode
    |> yields 19690720
    ||> printfn "Inputs needed to result in output of 19690720: noun (%d), verb (%d)"
    
    0 // return an integer exit code
