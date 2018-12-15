module Tests

open System
open Xunit
open Swensen.Unquote
open Program

module Rectangle =
    [<Theory>]
    [<InlineData(594, 575, 24, 15, 594)>]
    [<InlineData(257, 382, 13, 16, 257)>]
    [<InlineData(  4, 705, 20, 28,   4)>]
    [<InlineData(420, 667, 10, 17, 420)>]
    [<InlineData(336,   6, 11, 21, 336)>]
    let ``get the left coordinate`` x y width height expected =
        let rect = { Origin = { X = x; Y = y }; Width = width; Height = height }
        test <@ Rectangle.left rect = expected @>

    [<Theory>]
    [<InlineData(594, 575, 24, 15, 617)>]
    [<InlineData(257, 382, 13, 16, 269)>]
    [<InlineData(  4, 705, 20, 28,  23)>]
    [<InlineData(420, 667, 10, 17, 429)>]
    [<InlineData(336,   6, 11, 21, 346)>]
    let ``get the right coordinate`` x y width height expected =
        let rect = { Origin = { X = x; Y = y }; Width = width; Height = height }
        test <@ Rectangle.right rect = expected @>

    [<Theory>]
    [<InlineData(594, 575, 24, 15, 575)>]
    [<InlineData(257, 382, 13, 16, 382)>]
    [<InlineData(  4, 705, 20, 28, 705)>]
    [<InlineData(420, 667, 10, 17, 667)>]
    [<InlineData(336,   6, 11, 21,   6)>]
    let ``get the top coordinate`` x y width height expected =
        let rect = { Origin = { X = x; Y = y }; Width = width; Height = height }
        test <@ Rectangle.top rect = expected @>

    [<Theory>]
    [<InlineData(594, 575, 24, 15, 589)>]
    [<InlineData(257, 382, 13, 16, 397)>]
    [<InlineData(  4, 705, 20, 28, 732)>]
    [<InlineData(420, 667, 10, 17, 683)>]
    [<InlineData(336,   6, 11, 21,  26)>]
    let ``get the bottom coordinate`` x y width height expected =
        let rect = { Origin = { X = x; Y = y }; Width = width; Height = height }
        test <@ Rectangle.bottom rect = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  16)>]
    [<InlineData(3, 1, 4, 4,  16)>]
    [<InlineData(5, 5, 2, 2,   4)>]
    let ``calculates the area`` x y width height expected =
        let rect = { Origin = { X = x; Y = y }; Width = width; Height = height }
        test <@ Rectangle.area rect = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  3, 1, 4, 4,  false)>]
    [<InlineData(1, 3, 4, 4,  5, 5, 2, 2,  true)>]
    [<InlineData(3, 1, 4, 4,  5, 5, 2, 2,  false)>]
    let ``determines whether one rectangle is to the left of another`` x1 y1 w1 h1 x2 y2 w2 h2 expected =
        let rect1 = { Origin = { X = x1; Y = y1 }; Width = w1; Height = h1 }
        let rect2 = { Origin = { X = x2; Y = y2 }; Width = w2; Height = h2 }
        test <@ rect1 |> Rectangle.isLeftOf rect2 = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  3, 1, 4, 4,  false)>]
    [<InlineData(5, 5, 2, 2,  1, 3, 4, 4,  true)>]
    [<InlineData(3, 1, 4, 4,  5, 5, 2, 2,  false)>]
    let ``determines whether one rectangle is to the right of another`` x1 y1 w1 h1 x2 y2 w2 h2 expected =
        let rect1 = { Origin = { X = x1; Y = y1 }; Width = w1; Height = h1 }
        let rect2 = { Origin = { X = x2; Y = y2 }; Width = w2; Height = h2 }
        test <@ rect1 |> Rectangle.isRightOf rect2 = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  3, 1, 4, 4,  false)>]
    [<InlineData(5, 5, 2, 2,  1, 3, 4, 4,  false)>]
    [<InlineData(3, 1, 4, 4,  5, 5, 2, 2,  true)>]
    let ``determines whether one rectangle is above another`` x1 y1 w1 h1 x2 y2 w2 h2 expected =
        let rect1 = { Origin = { X = x1; Y = y1 }; Width = w1; Height = h1 }
        let rect2 = { Origin = { X = x2; Y = y2 }; Width = w2; Height = h2 }
        test <@ rect1 |> Rectangle.isAbove rect2 = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  3, 1, 4, 4,  false)>]
    [<InlineData(5, 5, 2, 2,  1, 3, 4, 4,  false)>]
    [<InlineData(5, 5, 2, 2,  3, 1, 4, 4,  true)>]
    let ``determines whether one rectangle is below another`` x1 y1 w1 h1 x2 y2 w2 h2 expected =
        let rect1 = { Origin = { X = x1; Y = y1 }; Width = w1; Height = h1 }
        let rect2 = { Origin = { X = x2; Y = y2 }; Width = w2; Height = h2 }
        test <@ rect1 |> Rectangle.isBelow rect2 = expected @>

    [<Theory>]
    [<InlineData(1, 3, 4, 4,  3, 1, 4, 4,  true)>]
    [<InlineData(1, 3, 4, 4,  5, 5, 2, 2,  false)>]
    [<InlineData(3, 1, 4, 4,  5, 5, 2, 2,  false)>]
    let ``determines whether two rectangles overlap`` x1 y1 w1 h1 x2 y2 w2 h2 expected =
        let rect1 = { Origin = { X = x1; Y = y1 }; Width = w1; Height = h1 }
        let rect2 = { Origin = { X = x2; Y = y2 }; Width = w2; Height = h2 }
        test <@ rect1 |> Rectangle.overlaps rect2 = expected @>
    
    let intersectTestData : obj array seq = seq {
        yield [| { Origin = { X = 1; Y = 3 }; Width = 4; Height = 4 }
                 { Origin = { X = 3; Y = 1 }; Width = 4; Height = 4 }
                 Some { Origin = { X = 3; Y = 3 }; Width = 2; Height = 2 } |]
        yield [| { Origin = { X = 3; Y = 1 }; Width = 4; Height = 4 }
                 { Origin = { X = 1; Y = 3 }; Width = 4; Height = 4 }
                 Some { Origin = { X = 3; Y = 3 }; Width = 2; Height = 2 } |]
        yield [| { Origin = { X = 3; Y = 1 }; Width = 4; Height = 4 }
                 { Origin = { X = 5; Y = 5 }; Width = 2; Height = 2 }
                 None |]
        yield [| { Origin = { X = 1; Y = 3 }; Width = 4; Height = 4 }
                 { Origin = { X = 5; Y = 5 }; Width = 2; Height = 2 }
                 None |]
    }

    [<Theory>]
    [<MemberData("intersectTestData")>]
    let ``calculates the intersection of two rectangles`` rect1 rect2 expected =
        test <@ rect1 |> Rectangle.intersect rect2 = expected @>

module Claim =
    [<Theory>]
    [<InlineData("#26 @ 594,575: 24x15",   26, 594, 575, 24, 15)>]
    [<InlineData("#43 @ 257,382: 13x16",   43, 257, 382, 13, 16)>]
    [<InlineData("#150 @ 4,705: 20x28",   150,   4, 705, 20, 28)>]
    [<InlineData("#232 @ 420,667: 10x17", 232, 420, 667, 10, 17)>]
    [<InlineData("#724 @ 336,6: 11x21",   724, 336,   6, 11, 21)>]
    let ``parses a string into a Claim`` input id x y width height =
        let expected = 
            Some { Id = id; Rectangle = { Origin = { X = x; Y = y };
                                          Width = width; Height = height } }
        test <@ Claim.parse input = expected @>

[<Fact>]
let ``identifies the number of overlapping square inches of fabric`` () =
    let claims = [
        { Id = 1; Rectangle = { Origin = { X = 1; Y = 3 }; Width = 4; Height = 4 } }
        { Id = 2; Rectangle = { Origin = { X = 3; Y = 1 }; Width = 4; Height = 4 } }
        { Id = 3; Rectangle = { Origin = { X = 5; Y = 5 }; Width = 2; Height = 2 } } ]
    test <@ calculateOverlap claims = 4 @>
