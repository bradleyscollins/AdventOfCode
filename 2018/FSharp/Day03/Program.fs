open System
open System.Text.RegularExpressions

module Array2D =
    let flatten array =
        let len1 = array |> Array2D.length1
        let len2 = array |> Array2D.length2
        seq { for j in 0 .. len2 - 1 do
                for i in 0 .. len1 - 1 do
                    yield array.[i,j] }
        |> Seq.toArray

type Point = int * int

module Point =
    let x point = fst point
    let y point = snd point

[<Struct>]
type Rectangle = { Origin : Point; Width : int; Height : int }

module Rectangle =
    let left rect = rect.Origin |> Point.x
    let top rect = rect.Origin |> Point.y
    let right rect = left rect + rect.Width - 1
    let bottom rect = top rect + rect.Height - 1

    let points (rect : Rectangle) =
        seq { for x in left rect .. right rect do
                for y in top rect .. bottom rect do
                    yield x, y }
        |> Seq.toList

[<Struct>]
type Claim = { Id : int; Rectangle : Rectangle }

module Claim =
    let identifier (c : Claim) = c.Id
    let rectangle (c : Claim) = c.Rectangle

    let parse (s : string) = 
        let regex =
            @"^#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)$"
            |> Regex
        let m = regex.Match s
        if m.Success then
            Some { Id = int m.Groups.["id"].Value;
                   Rectangle = { Origin = (int m.Groups.["x"].Value,
                                           int m.Groups.["y"].Value)
                                 Width = int m.Groups.["w"].Value
                                 Height = int m.Groups.["h"].Value } }
        else None

type Fabric = int [,]

module Fabric =
    let generate (claims : Claim seq) =
        let rects = claims |> Seq.map Claim.rectangle
        let maxRight = rects |> Seq.map Rectangle.right |> Seq.max
        let maxBottom = rects |> Seq.map Rectangle.bottom |> Seq.max
        Array2D.zeroCreate<int> (maxRight + 2) (maxBottom + 2)
    
    let claim (claim : Claim) (fabric : Fabric) =
        let mark (fabric' : Fabric) ((x, y) : Point) =
            fabric'.[x,y] <- fabric'.[x,y] + 1

        claim
        |> Claim.rectangle
        |> Rectangle.points
        |> List.iter (mark fabric)

        fabric
    
    let makeClaims (claims : Claim seq) (fabric : Fabric) =
        let makeClaim fabric' claim' = fabric' |> claim claim'
        claims |> Seq.fold makeClaim fabric

    let areaInDispute (fabric : Fabric) =
        fabric
        |> Array2D.flatten
        |> Seq.filter (fun x -> x > 1)
        |> Seq.length
    
    let isIntact (claim : Claim) (fabric : Fabric) =
        claim
        |> Claim.rectangle
        |> Rectangle.points
        |> Seq.map (fun (x,y) -> fabric.[x,y])
        |> Seq.forall ((=) 1)

module String =
    let trim (s : string) = s.Trim ()

let calculateOverlap (claims : Claim list) =
    claims
    |> Fabric.generate
    |> Fabric.makeClaims claims
    |> Fabric.areaInDispute

let findIntactClaims (claims : Claim list) =
    let fabric = claims |> Fabric.generate |> Fabric.makeClaims claims

    claims
    |> Seq.filter (fun claim -> fabric |> Fabric.isIntact claim)
    |> Seq.map Claim.identifier
    |> Seq.toList

[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.choose Claim.parse
        |> Seq.toList
    
    printfn "Result 1: %d sq. in." (calculateOverlap input)
    printfn "Result 2: %A" (findIntactClaims input)

    0 // return an integer exit code
