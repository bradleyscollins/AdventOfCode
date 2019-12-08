module Day03

open System
open System.IO
open System.Text.RegularExpressions

module String =
    let split (delimiter : string) (s : string) = s.Split delimiter
    let trim (s : string) = s.Trim ()

module List =
    let cons h t = h :: t

module Seq =
    let sequence options =
        let rec transform values opts =
            match opts with
            | [] -> Some values
            | None :: _ -> None
            | Some x :: tail -> transform (x :: values) tail
            
        options
        |> Seq.toList
        |> transform []
        |> Option.map List.rev

type Segment =
| Up of int
| Down of int
| Left of int
| Right of int

module Segment =
    let parse str =
        let re = Regex @"^(?<direction>[LRUD])(?<length>\d+)$"
        let result = re.Match str

        match result.Success with
        | true -> 
            let length = int <| result.Groups.["length"].Value
            let direction = result.Groups.["direction"].Value

            match direction with
            | "R" -> Some (Right length)
            | "L" -> Some (Left  length)
            | "U" -> Some (Up    length)
            | "D" -> Some (Down  length)
            | _ -> None
        | _ -> None


type Point = int * int

module Point =
    let up    (x, y) distance = (x,            y + distance)
    let down  (x, y) distance = (x,            y - distance)
    let right (x, y) distance = (x + distance, y           )
    let left  (x, y) distance = (x - distance, y           )

    let manhattanDistanceTo (x2, y2) (x1, y1) =
        abs (x2 - x1) + abs (y2 - y1)

module Points =
    let along segment startingAt =
        let move, distance =
            match segment with
            | Up by    -> Point.up, by
            | Down by  -> Point.down, by
            | Left by  -> Point.left, by
            | Right by -> Point.right, by

        [1 .. distance]
        |> List.map (move startingAt)
        

type Wire = Segment list

module Wire =
    let empty = []
    let parse str =
        String.split "," str
        |> Seq.map Segment.parse
        |> Seq.sequence

    let allPointsStartingFrom startingAt wire =
        wire
        |> List.fold
            (fun points segment ->
                let startingPoint = points |> List.last
                let segmentPoints = startingPoint |> Points.along segment
                points @ segmentPoints)
            [ startingAt ]
        |> List.tail


let calcDistanceToNearestIntersection wire1 wire2 =
    let centralPort = 0,0
    let pointsInWire1 = wire1
                        |> Wire.parse
                        |> Option.defaultValue Wire.empty
                        |> Wire.allPointsStartingFrom centralPort
                        |> Set.ofList
    let pointsInWire2 = wire2
                        |> Wire.parse
                        |> Option.defaultValue Wire.empty
                        |> Wire.allPointsStartingFrom centralPort
                        |> Set.ofList
    let intersections = Set.intersect pointsInWire1 pointsInWire2
    
    intersections
    |> Seq.map (Point.manhattanDistanceTo centralPort)
    |> Seq.min

[<EntryPoint>]
let main argv =
    let wires = File.ReadAllLines "input.txt"
                |> Seq.map String.trim
                |> Seq.filter (not << String.IsNullOrEmpty)
                |> Seq.toList

    match wires with
    | wire1 :: wire2 :: _ ->
        calcDistanceToNearestIntersection wire1 wire2
        |> printfn "Manhattan distance to nearest intersection: %d"

    | _ -> eprintfn "Bad input!"

    0 // return an integer exit code
