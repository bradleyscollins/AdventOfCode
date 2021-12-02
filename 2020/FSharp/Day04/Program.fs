module Day04

open System
open System.IO
open System.Text.RegularExpressions
open AdventOfCode.Utilities

let (|Integer|_|) (str : string) =
   match Int32.TryParse str with
   | true, x -> Some x
   | _       -> None

let (|ParseRegex|_|) pattern str =
    let regex = Regex (pattern, RegexOptions.Compiled)
    let m = regex.Match str
    if m.Success
    then Some (List.tail [ for x in m.Groups -> x.Value ])
    else None

let (|Year|_|) text =
    match text with
    | ParseRegex "^(\d{4})$" [Integer year] -> Some year
    | _                                     -> None

let (|Inches|_|) text =
    match text with
    | ParseRegex "^(\d{2})in$" [Integer inches] -> Some inches
    | _                                         -> None

let (|Centimeters|_|) text =
    match text with
    | ParseRegex "^(\d{3})cm$" [Integer cm] -> Some cm
    | _                                     -> None

let tokenize = String.splitNoEmpties [| " " |]

let tokenToPair token =
    match token |> String.split [| ":" |] with
    | [| key; value |] -> Ok (key, value)
    | _                -> Error $"Invalid token: '{token}'"

let parsePassportFields line =
    line
    |> tokenize
    |> Array.choose (tokenToPair >> Result.toOption)
    |> Map.ofArray

let combinePassportFields passports fields =
    if fields |> Map.isEmpty then
        fields :: passports
    else
        match passports with
        | passport :: rest ->
            let passport' = passport |> Map.merge fields
            passport' :: rest
        | _ -> fields :: passports

let parsePassports lines =
    lines
    |> Seq.map parsePassportFields
    |> Seq.fold combinePassportFields [Map.empty]
    |> Seq.filter (not << Map.isEmpty)
    |> Seq.rev
    |> Seq.toList

let hasRequiredPassportFields passport =
    let requiredFields = [ "byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid" ]
    
    requiredFields
    |> List.forall (fun k -> passport |> Map.containsKey k)

let isValidYear minimum maximum text =
    match text with
    | Year yr -> minimum <= yr && yr <= maximum
    | _       -> false

let isValidBirthYear = isValidYear 1920 2002
let isValidIssueYear = isValidYear 2010 2020
let isValidExpirationYear = isValidYear 2020 2030

let isValidHeight text =
    match text with
    | Inches inches  -> 59 <= inches && inches <= 76
    | Centimeters cm -> 150 <= cm && cm <= 193
    | _              -> false

let isValidHairColor text =
    match text with
    | ParseRegex "^#[0-9A-Fa-f]{6}$" _ -> true
    | _                                -> false

let isValidEyeColor text =
    set ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth"]
    |> Seq.contains text

let isValidPassportId text =
    match text with
    | ParseRegex "^(\d{9})$" _ -> true
    | _                        -> false

let isPassportFieldValid key value =
    match key with
    | "byr" -> isValidBirthYear value
    | "iyr" -> isValidIssueYear value
    | "eyr" -> isValidExpirationYear value
    | "hgt" -> isValidHeight value
    | "hcl" -> isValidHairColor value
    | "ecl" -> isValidEyeColor value
    | "pid" -> isValidPassportId value
    | _     -> true
    
let areAllPassportFieldsValid passport =
    passport
    |> Map.forall isPassportFieldValid

[<EntryPoint>]
let main argv =
    let passports = 
        File.ReadAllLines "input.txt"
        |> parsePassports

    passports
    |> Seq.filter hasRequiredPassportFields
    |> Seq.length
    |> printfn "%d passports contain the required fields"

    passports
    |> Seq.filter hasRequiredPassportFields
    |> Seq.filter areAllPassportFieldsValid
    |> Seq.length
    |> printfn "%d passports are valid"

    0 // return an integer exit code
