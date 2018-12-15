open System
open System.Text.RegularExpressions

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
    
    let mark (fabric : Fabric) ((x, y) : Point) =
        fabric.[x,y] <- fabric.[x,y] + 1

    let claim (fabric : Fabric) (claim : Claim) =
        claim
        |> Claim.rectangle
        |> Rectangle.points
        |> List.iter (mark fabric)

module String =
    let trim (s : string) = s.Trim ()

module Array2D =
    let flatten array =
        let len1 = array |> Array2D.length1
        let len2 = array |> Array2D.length2
        seq { for j in 0 .. len2 - 1 do
                for i in 0 .. len1 - 1 do
                    yield array.[i,j] }
        |> Seq.toArray

let calculateOverlap (claims : Claim list) =
    let fabric = claims |> Fabric.generate
    claims |> Seq.iter (Fabric.claim fabric)
    
    fabric
    |> Array2D.flatten
    |> Seq.filter (fun x -> x > 1)
    |> Seq.length


[<EntryPoint>]
let main argv =
    let input =
        System.IO.File.ReadAllLines "input.txt"
        |> Seq.map String.trim
        |> Seq.filter (not << String.IsNullOrWhiteSpace)
        |> Seq.choose Claim.parse
        |> Seq.toList
    


    printfn "Result: %d sq. in." (calculateOverlap input)

    0 // return an integer exit code
