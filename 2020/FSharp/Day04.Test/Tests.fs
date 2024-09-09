module Day04.Test.Tests

open System
open System.IO
open Xunit
open Swensen.Unquote
open AdventOfCode.Utilities
open Day04


[<Fact>]
let ``tokenize converts a string with spaces to an array of tokens`` () =
    [| "ecl:gry"; "pid:860033327"; "eyr:2020"; "hcl:#fffffd" |] =! (tokenize "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd")

let ``tokenToPair test cases`` =
    seq [
        "ecl:gry",       Ok ("ecl", "gry")
        "pid:860033327", Ok ("pid", "860033327")
        "eyr:2020",      Ok ("eyr", "2020")
        "hcl:#fffffd",   Ok ("hcl", "#fffffd")
        "byr:1937",      Ok ("byr", "1937")
        "iyr:2017",      Ok ("iyr", "2017")
        "cid:147",       Ok ("cid", "147")
        "hgt:183cm",     Ok ("hgt", "183cm")

        "ecl-gry",       Error "Invalid token: 'ecl-gry'"
        "ecl gry",       Error "Invalid token: 'ecl gry'"
        "ecl\tgry",      Error "Invalid token: 'ecl\tgry'"
    ]
    |> Seq.map TestCase.fromPair

[<Theory>]
[<MemberData(nameof(``tokenToPair test cases``))>]
let ``tokenToPair converts tokens to key-value pairs`` token expected =
    expected =! (tokenToPair token)

[<Fact>]
let ``parsePassports converts an array of lines from an input text file into passports`` () =
    let input = File.ReadAllLines "test-input.txt"
    let expected = [
        Map [
            "ecl", "gry";  "pid", "860033327"; "eyr", "2020"; "hcl", "#fffffd"
            "byr", "1937"; "iyr", "2017";      "cid", "147";  "hgt", "183cm"
        ]
        Map [
            "iyr", "2013";    "ecl", "amb"; "cid", "350"; "eyr", "2023"; "pid", "028048884"
            "hcl", "#cfa07d"; "byr", "1929"
        ]
        Map [
            "hcl", "#ae17e1"; "iyr", "2013"
            "eyr", "2024"
            "ecl", "brn";     "pid", "760753108"; "byr", "1931"
            "hgt", "179cm"
        ]
        Map [
            "hcl", "#cfa07d"; "eyr", "2025"; "pid", "166559648"
            "iyr", "2011";    "ecl", "brn";  "hgt", "59in"
        ]
    ]

    expected =! (parsePassports input)

let ``hasRequiredPassportFields test cases`` =
    let passport1 = Map [
        "ecl", "gry";  "pid", "860033327"; "eyr", "2020"; "hcl", "#fffffd"
        "byr", "1937"; "iyr", "2017";      "cid", "147";  "hgt", "183cm"
    ]
    let passport2 = Map [
        "iyr", "2013";    "ecl", "amb"; "cid", "350"; "eyr", "2023"; "pid", "028048884"
        "hcl", "#cfa07d"; "byr", "1929"
    ]
    let passport3 = Map [
        "hcl", "#ae17e1"; "iyr", "2013"
        "eyr", "2024"
        "ecl", "brn";     "pid", "760753108"; "byr", "1931"
        "hgt", "179cm"
    ]
    let passport4 = Map [
        "hcl", "#cfa07d"; "eyr", "2025"; "pid", "166559648"
        "iyr", "2011";    "ecl", "brn";  "hgt", "59in"
    ]

    [
        passport1, true
        passport2, false
        passport3, true
        passport4, false
    ]
    |> Seq.map TestCase.fromPair

[<Theory>]
[<MemberData(nameof(``hasRequiredPassportFields test cases``))>]
let ``hasRequiredPassportFields checks to make sure that the passport contains all the required fields`` passport expected =
    expected =! (hasRequiredPassportFields passport)

let ``areAllPassportFieldsValid test cases`` =
    let validPassports = File.ReadAllLines "valid-passports.txt" |> parsePassports
    let invalidPassports = File.ReadAllLines "invalid-passports.txt" |> parsePassports
    let allTrue = Seq.initInfinite (fun _ -> true)
    let allFalse = Seq.initInfinite (fun _ -> false)

    Seq.append (Seq.zip validPassports allTrue) (Seq.zip invalidPassports allFalse)
    |> Seq.map TestCase.fromPair

[<Theory>]
[<MemberData(nameof(``areAllPassportFieldsValid test cases``))>]
let ``areAllPassportFieldsValid checks to make sure that the passport's fields are valid`` passport expected =
    expected =! (areAllPassportFieldsValid passport)
