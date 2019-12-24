module Day04.Tests

open System
open Swensen.Unquote
open Xunit

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
