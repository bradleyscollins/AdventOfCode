module Day02

open System
open System.Text.RegularExpressions
open System.IO

module String =
    let chars (s : string) = s.ToCharArray ()
    let trim (s : string) = s.Trim ()

module Result =
    let isOk = function Ok _ -> true | _ -> false

    let isError = function Error _ -> true | _ -> false // (not << isOk) caused errors (?!)

    let resolve valueIfError =
        function
        | Error _ -> valueIfError
        | Ok x    -> x

    let map2 mapping res1 res2 =
        match res1, res2 with
        | (Ok x, Ok y) -> Ok (mapping x y)
        | (Error x, _)
        | (_      , Error x) -> Error x

    let sequence (results : Result<'TOk, 'TError> list) : Result<'TOk list, 'TError> =
        let appendOne xs x = xs @ [x]

        results
        |> List.fold (map2 appendOne) (Ok [])

type Policy =
| MinMaxBased of Min : int * Max : int * Token : char
| PositionBased of Positions : int list * Token : char

module Policy =
    let applyTo password policy =
        match policy with
        | MinMaxBased (minimum, maximum, token) ->
            let countTokens token' password' =
                password'
                |> String.chars
                |> Seq.filter ((=) token')
                |> Seq.length

            let grammarize word n = if n = 1 then word else $"{word}s"

            let count = password |> countTokens token
            let message = $"""'{password}' contains '{token}' {count} {grammarize "time" count}"""

            if count < minimum || count > maximum then
                Error message
            else
                Ok message

        | PositionBased (positions, token) ->
            let chars = password |> String.chars

            let oks =
                positions
                |> List.map (fun n -> if chars.[n-1] = token then Ok n else Error n)
                |> List.filter Result.isOk
                |> List.map (Result.resolve -1)

            match oks with
            | [] ->
                Error $"'{password}' does not contain '{token}'"
            | [pos] ->
                Ok $"'{password}' contains '{token}' at position {pos}"
            | _ ->
                Error $"""'{password}' contains '{token}' at positions {String.Join(" and ", oks)}"""

type PasswordRecord = { Number1 : int; Number2 : int; Token : char; Password : string }
module PasswordRecord =
    let parse text =
        let regex = Regex(@"^(?<Number1>\d+)-(?<Number2>\d+)\W+(?<Token>\w):\W+(?<Password>\w+)$")
        let matches = regex.Match text
        let num1 = Int32.Parse matches.Groups.["Number1"].Value
        let num2 = Int32.Parse matches.Groups.["Number2"].Value
        let token = matches.Groups.["Token"].Value.[0]
        let password = matches.Groups.["Password"].Value
    
        { Number1 = num1; Number2 = num2; Token = token; Password = password }

    let toMinMaxBasedPolicy record =
        MinMaxBased (record.Number1, record.Number2, record.Token)

    let toPositionBasedPolicy record =
        PositionBased ([record.Number1; record.Number2], record.Token)

    let password record = record.Password

[<EntryPoint>]
let main argv =
    let passwordRecords =
        File.ReadAllLines "input.txt"
        |> Seq.map (String.trim >> PasswordRecord.parse)

    let passwords = passwordRecords |> Seq.map PasswordRecord.password
    let minMaxPolicies = passwordRecords |> Seq.map PasswordRecord.toMinMaxBasedPolicy
    let positionPolicies = passwordRecords |> Seq.map PasswordRecord.toPositionBasedPolicy

    Seq.zip passwords minMaxPolicies
    |> Seq.map (fun (password, policy) -> policy |> Policy.applyTo password)
    |> Seq.filter Result.isOk
    |> Seq.length
    |> printfn "%d passwords meet min-max–based policy" 

    Seq.zip passwords positionPolicies
    |> Seq.map (fun (password, policy) -> policy |> Policy.applyTo password)
    |> Seq.filter Result.isOk
    |> Seq.length
    |> printfn "%d passwords meet position-based policy" 

    0 // return an integer exit code
