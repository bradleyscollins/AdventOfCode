open System
open System.Text.RegularExpressions

[<Struct>]
type Point = { X : int; Y : int }

[<Struct>]
type Rectangle = { Origin : Point; Width : int; Height : int }

module Rectangle =
    let left rect = rect.Origin.X
    let right rect = rect.Origin.X + rect.Width - 1
    let top rect = rect.Origin.Y
    let bottom rect = rect.Origin.Y + rect.Height - 1

    let area rect = rect.Width * rect.Height

    let isLeftOf rect2 rect1 = (right rect1) < (left rect2)
    let isRightOf rect2 rect1 = (left rect1) > (right rect2)
    let isAbove rect2 rect1 = (bottom rect1) < (top rect2)
    let isBelow rect2 rect1 = (top rect1) > (bottom rect2)

    let overlaps rect2 rect1 = not (rect1 |> isLeftOf rect2
                                    || rect1 |> isRightOf rect2
                                    || rect1 |> isAbove rect2
                                    || rect1 |> isBelow rect2)

    let intersect rect2 rect1 =
        if rect1 |> overlaps rect2 then
            let rects = [rect1; rect2]
            let left' = rects |> Seq.map left |> Seq.max
            let right' = rects |> Seq.map right |> Seq.min
            let top' = rects |> Seq.map top |> Seq.max
            let bottom' = rects |> Seq.map bottom |> Seq.min
            let width = right' - left' + 1
            let height = bottom' - top' + 1
            Some { Origin = { X = left'; Y = top' };
                   Width = width; Height = height }
        else None

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
            let id = int m.Groups.["id"].Value
            let x = int m.Groups.["x"].Value
            let y = int m.Groups.["y"].Value
            let width = int m.Groups.["w"].Value
            let height = int m.Groups.["h"].Value

            Some { Id = id; Rectangle = { Origin = { X = x; Y = y }
                                          Width = width; Height = height } }
        else None

module String =
    let trim (s : string) = s.Trim ()

let calculateOverlap (claims : Claim list) =
    let rec calc overlap rects =
        match rects with
        | rect :: others ->
            let overlap' = others
                           |> Seq.choose (Rectangle.intersect rect)
                           |> Seq.map Rectangle.area
                           |> Seq.sum
            calc (overlap + overlap') others
        | _ -> overlap
    
    claims
    |> List.map Claim.rectangle
    |> calc 0


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
