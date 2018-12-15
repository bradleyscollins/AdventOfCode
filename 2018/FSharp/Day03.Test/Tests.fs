module Tests

open System
open Xunit
open Swensen.Unquote
open Program

module Rectangle =
    let pointsTestData : obj array seq = seq {
        yield [| { Origin = (1, 3); Width = 4; Height = 4 }
                 [ (1, 3); (1, 4); (1, 5); (1, 6)
                   (2, 3); (2, 4); (2, 5); (2, 6)
                   (3, 3); (3, 4); (3, 5); (3, 6)
                   (4, 3); (4, 4); (4, 5); (4, 6) ] |]
        yield [| { Origin = (3, 1); Width = 4; Height = 4 }
                 [ (3, 1); (3, 2); (3, 3); (3, 4)
                   (4, 1); (4, 2); (4, 3); (4, 4)
                   (5, 1); (5, 2); (5, 3); (5, 4)
                   (6, 1); (6, 2); (6, 3); (6, 4) ] |]
        yield [| { Origin = (5, 5); Width = 2; Height = 2 }
                 [ (5, 5); (5, 6)
                   (6, 5); (6, 6) ] |]
    }

    [<Theory>]
    [<MemberData("pointsTestData")>]
    let ``calculates the points in a rectangle`` rect expected =
        test <@ rect |> Rectangle.points = expected @>

module Claim =
    [<Theory>]
    [<InlineData("#26 @ 594,575: 24x15",   26, 594, 575, 24, 15)>]
    [<InlineData("#43 @ 257,382: 13x16",   43, 257, 382, 13, 16)>]
    [<InlineData("#150 @ 4,705: 20x28",   150,   4, 705, 20, 28)>]
    [<InlineData("#232 @ 420,667: 10x17", 232, 420, 667, 10, 17)>]
    [<InlineData("#724 @ 336,6: 11x21",   724, 336,   6, 11, 21)>]
    let ``parses a string into a Claim`` input id x y width height =
        let expected = 
            Some { Id = id
                   Rectangle = { Origin = x, y; Width = width; Height = height } }
        test <@ Claim.parse input = expected @>

module Fabric =
    [<Fact>]
    let ``generates a grid that encompasses all claims`` () =
        let claims = [
            { Id = 1; Rectangle = { Origin = (1, 3); Width = 4; Height = 4 } }
            { Id = 2; Rectangle = { Origin = (3, 1); Width = 4; Height = 4 } }
            { Id = 3; Rectangle = { Origin = (5, 5); Width = 2; Height = 2 } } ]

        let expected = Array2D.zeroCreate<int> 8 8
        test <@ claims |> Fabric.generate = expected @>

    [<Fact>]
    let ``claims points in fabric`` () =
        let claims = [
            { Id = 1; Rectangle = { Origin = (1, 3); Width = 4; Height = 4 } }
            { Id = 2; Rectangle = { Origin = (3, 1); Width = 4; Height = 4 } }
            { Id = 3; Rectangle = { Origin = (5, 5); Width = 2; Height = 2 } } ]

        let fabric = claims |> Fabric.generate
        let expected = array2D [[0; 0; 0; 0; 0; 0; 0; 0]
                                [0; 0; 0; 1; 1; 1; 1; 0]
                                [0; 0; 0; 1; 1; 1; 1; 0]
                                [0; 1; 1; 2; 2; 1; 1; 0]
                                [0; 1; 1; 2; 2; 1; 1; 0]
                                [0; 1; 1; 1; 1; 1; 1; 0]
                                [0; 1; 1; 1; 1; 1; 1; 0]
                                [0; 0; 0; 0; 0; 0; 0; 0]]
        
        test <@ fabric |> Fabric.makeClaims claims = expected @>

    [<Fact>]
    let ``determines whether a claim is intact`` () =
        let claim1 =
            { Id = 1; Rectangle = { Origin = (1, 3); Width = 4; Height = 4 } }
        let claim2 =
            { Id = 2; Rectangle = { Origin = (3, 1); Width = 4; Height = 4 } }
        let claim3 =
            { Id = 3; Rectangle = { Origin = (5, 5); Width = 2; Height = 2 } }
        let claims = [claim1; claim2; claim3]

        let fabric = claims |> Fabric.generate |> Fabric.makeClaims claims
        
        
        test <@ fabric |> Fabric.isIntact claim1 = false @>
        test <@ fabric |> Fabric.isIntact claim2 = false @>
        test <@ fabric |> Fabric.isIntact claim3 = true @>
        
[<Fact>]
let ``identifies the number of overlapping square inches of fabric`` () =
    let claims = [
        { Id = 1; Rectangle = { Origin = (1, 3); Width = 4; Height = 4 } }
        { Id = 2; Rectangle = { Origin = (3, 1); Width = 4; Height = 4 } }
        { Id = 3; Rectangle = { Origin = (5, 5); Width = 2; Height = 2 } } ]
    test <@ calculateOverlap claims = 4 @>
        
[<Fact>]
let ``identifies the IDs of claims still intact after all claims are made`` () =
    let claims = [
        { Id = 1; Rectangle = { Origin = (1, 3); Width = 4; Height = 4 } }
        { Id = 2; Rectangle = { Origin = (3, 1); Width = 4; Height = 4 } }
        { Id = 3; Rectangle = { Origin = (5, 5); Width = 2; Height = 2 } } ]
    test <@ findIntactClaims claims = [3] @>
