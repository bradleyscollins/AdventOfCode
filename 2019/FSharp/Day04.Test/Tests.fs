module Day04.Tests

open System
open Swensen.Unquote
open Xunit

module Tuple2 =
    let toObjArray (x, y) = [| box x; box y |]


[<Theory>]
[<InlineData("122345",  true)>]
[<InlineData("111123",  true)>]
[<InlineData("135679",  false)>]
[<InlineData("111111",  true)>]
[<InlineData("223450",  false)>]
[<InlineData("123789",  false)>]
[<InlineData("12378",   false)>]
[<InlineData("1236789", false)>]
let ``Password.isValid evaluates whether a password meets the criteria`` password expected =
    Password.isValid password =! expected

let ``Password.consecutiveDigits test cases`` =
    [
        "112233", ['1',2; '2',2; '3',2]
        "123444", ['1',1; '2',1; '3',1; '4',3]
        "111122", ['1',4; '2',2]
    ]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Password.consecutiveDigits test cases")>]
let ``Password.consecutiveDigits returns a list of pairs indicating the digit and the number of times it occurs in a row`` password expected =
    password |> Password.consecutiveDigits =! expected

[<Theory>]
[<InlineData("122444",  true)>]
[<InlineData("112233",  true)>]
[<InlineData("123444",  false)>]
[<InlineData("111122",  true)>]
[<InlineData("111123",  false)>]
[<InlineData("122345",  true)>]
[<InlineData("111234",  false)>]
[<InlineData("135679",  false)>]
[<InlineData("111111",  false)>]
[<InlineData("333444",  false)>]
[<InlineData("333334",  false)>]
[<InlineData("223450",  false)>]
[<InlineData("123789",  false)>]
[<InlineData("12378",   false)>]
[<InlineData("1236789", false)>]
let ``Password.isValid2 evaluates whether a password meets the criteria`` password expected =
    Password.isValid2 password =! expected

[<Theory>]
[<InlineData("122345",  "122346")>]
[<InlineData("277777",  "277778")>]
[<InlineData("277778",  "277779")>]
[<InlineData("277779",  "277788")>]
[<InlineData("277788",  "277789")>]
[<InlineData("277789",  "277799")>]
[<InlineData("277799",  "277888")>]
[<InlineData("277999",  "278888")>]
[<InlineData("279999",  "288888")>]
[<InlineData("299999",  "333333")>]
let ``Password.next takes one password and generates the next valid one`` password expected =
    Password.next password =! expected
