module Day03

open System
open System.IO
open System.Text.RegularExpressions

[<AutoOpen>]
module Functions =
    let flip f y x = f x y

module Tuple2 =
    let cons x y = x, y

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


type Direction = Up | Down | Left | Right

module Direction =
    let parse = function
                | "R" -> Some Right
                | "L" -> Some Left
                | "U" -> Some Up
                | "D" -> Some Down
                | _ -> None


type Segment = Direction * int

module Segment =
    let parse str =
        let re = Regex @"^(?<direction>[LRUD])(?<length>\d+)$"
        let result = re.Match str

        match result.Success with
        | true -> 
            let direction = Direction.parse result.Groups.["direction"].Value
            let length = Some (int result.Groups.["length"].Value)
            
            (direction, length)
            ||> Option.map2 Tuple2.cons

        | _ -> None

module Segments =
    let parse str =
        String.split "," str
        |> Seq.map Segment.parse
        |> Seq.sequence


type Point = int * int

module Point =
    let centralPort = 0,0

    let up    (x, y) distance = (x,            y + distance)
    let down  (x, y) distance = (x,            y - distance)
    let right (x, y) distance = (x + distance, y           )
    let left  (x, y) distance = (x - distance, y           )

    let manhattanDistanceTo (x2, y2) (x1, y1) =
        abs (x2 - x1) + abs (y2 - y1)

module Points =
    let along (direction, distance) startingAt =
        let move = match direction with
                   | Up    -> Point.up
                   | Down  -> Point.down
                   | Left  -> Point.left
                   | Right -> Point.right

        [1 .. distance]
        |> List.map (move startingAt)

    let distanceTo point points =
        points
        |> List.tryFindIndex ((=) point)
        |> Option.map ((+) 1)
        |> Option.defaultValue Int32.MaxValue


type Wire = Point list // not including central port

module Wire =
    let empty = []

    let fromSegments segments =
        segments
        |> List.fold
            (fun points segment ->
                let startingPoint = points |> List.last
                let segmentPoints = startingPoint |> Points.along segment
                points @ segmentPoints)
            [ Point.centralPort ]
        |> List.tail

    let intersections wire1 wire2 =
        Set.intersect (Set.ofList wire1) (Set.ofList wire2)

    let distanceTo point wire = wire
                                |> List.tryFindIndex ((=) point)
                                |> Option.map ((+) 1)

    let circuitDistance wire1 wire2 point = [wire1; wire2]
                                            |> Seq.choose (distanceTo point)
                                            |> Seq.sum

    
let calcManhattanDistanceToNearestIntersection wire1 wire2 =
    let wire1' = wire1
                 |> Segments.parse
                 |> Option.map Wire.fromSegments
                 |> Option.defaultValue Wire.empty
    let wire2' = wire2
                 |> Segments.parse
                 |> Option.map Wire.fromSegments
                 |> Option.defaultValue Wire.empty

    Wire.intersections wire1' wire2'
    |> Seq.map (Point.manhattanDistanceTo Point.centralPort)
    |> Seq.min

let calcDistanceOfShortestCircuit wire1 wire2 =
    let wire1' = wire1
                 |> Segments.parse
                 |> Option.map Wire.fromSegments
                 |> Option.defaultValue Wire.empty
    let wire2' = wire2
                 |> Segments.parse
                 |> Option.map Wire.fromSegments
                 |> Option.defaultValue Wire.empty

    Wire.intersections wire1' wire2'
    |> Seq.map (Wire.circuitDistance wire1' wire2')
    |> Seq.min


[<EntryPoint>]
let main argv =
    let wires = File.ReadAllLines "input.txt"
                |> Seq.map String.trim
                |> Seq.filter (not << String.IsNullOrEmpty)
                |> Seq.toList

    match wires with
    | wire1 :: wire2 :: _ ->
        calcManhattanDistanceToNearestIntersection wire1 wire2
        |> printfn "Manhattan distance to nearest intersection: %d"

        calcDistanceOfShortestCircuit wire1 wire2
        |> printfn "Distance of shortest circuit: %d"

    | _ -> eprintfn "Bad input!"

    0 // return an integer exit code
