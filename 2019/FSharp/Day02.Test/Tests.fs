module Day02.Tests

open Xunit
open Swensen.Unquote

let parseTheoryTestCases =
    [|
        "1,0,0,0,99", [| 1;0;0;0;99 |]
        "2,3,0,3,99", [| 2;3;0;3;99 |]
        "2,4,4,5,99,0", [| 2;4;4;5;99;0 |]
        "1,1,1,4,99,5,6,0,99", [| 1;1;1;4;99;5;6;0;99 |] |]
    |> Array.map (fun (intcode, array) -> [| box intcode; box array |])
    |> Array.map box

[<Theory>]
[<MemberData("parseTheoryTestCases")>]
let ``parse parses intcode into an of integers`` intcode expected =
    expected =! parse intcode


let toInstructionTestCases =
    [|
        [| 1;0;0;0 |], Add (0, 0, 0)
        [| 2;3;0;3 |], Mult (3, 0, 3)
        [| 2;4;4;5 |], Mult (4, 4, 5)
        [| 1;1;1;4 |], Add (1, 1, 4) |]
    |> Array.map (fun (chunkOf4, array) -> [| box chunkOf4; box array |])
    |> Array.map box

[<Theory>]
[<MemberData("toInstructionTestCases")>]
let ``toInstruction converts an array of four numbers into an instruction`` chunkOf4 expected =
    expected =! toInstruction chunkOf4


let executeOnTestCases =
    [|
        [| 1;0;0;0;99 |],          Add (0, 0, 0),  [| 2;0;0;0;99 |]
        [| 2;3;0;3;99 |],          Mult (3, 0, 3), [| 2;3;0;6;99 |]
        [| 2;4;4;5;99;0 |],        Mult (4, 4, 5), [| 2;4;4;5;99;9801 |]
        [| 1;1;1;4;99;5;6;0;99 |], Add (1, 1, 4),  [| 1;1;1;4;2;5;6;0;99 |]
        [| 1;1;1;4;2;5;6;0;99 |],  Mult (5, 6, 0), [| 30;1;1;4;2;5;6;0;99 |]
    |]
    |> Array.map (fun (program, instruction, result) -> [| box program; box instruction; box result |])
    |> Array.map box

[<Theory>]
[<MemberData("executeOnTestCases")>]
let ``executeOn executes an instruction on the given program `` program instruction expected =
    expected =! executeOn program instruction


[<Fact>]
let ``initialize alters the given intcode, replacing the values at the given positions with the specified values`` () =
    let changes = [ (1, 12); (2, 2) ]
    let intcode = "1,0,0,0,99"
    let expected = "1,12,2,0,99"
    expected =! (intcode |> initializeWith changes)


[<Theory>]
[<InlineData("1,0,0,0,99", "2,0,0,0,99")>]
[<InlineData("2,3,0,3,99", "2,3,0,6,99")>]
[<InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")>]
[<InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")>]
let ``execute properly interprets and executes the given intcode`` intcode expected =
    expected =! execute intcode
