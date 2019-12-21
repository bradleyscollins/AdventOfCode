module Day03.Tests

open System
open Xunit
open Swensen.Unquote

module Tuple2 =
    let toObjArray (x, y) = [| box x; box y |]

module Tuple3 =
    let toObjArray (x, y, z) = [| box x; box y; box z |]

module Tuple4 =
    let toObjArray (w, x, y, z) = [| box w; box x; box y; box z |]

let ``Segment parse test cases`` =
    [|
        "R75", Some (Right, 75)
        "D30", Some (Down , 30)
        "R83", Some (Right, 83)
        "U83", Some (Up   , 83)
        "L12", Some (Left , 12)
        "D49", Some (Down , 49)
        "R71", Some (Right, 71)
        "U7" , Some (Up   ,  7)
        "L72", Some (Left , 72)
        "X27", None
        "Rxx", None
    |]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Segment parse test cases")>]
let ``Segment parse converts a string to Segment option`` str expected =
    Segment.parse str =! expected


let ``Segment parseList test cases`` =
    [|
        "R75,D30,R83,U83,L12,D49,R71,U7,L72",
        Some [ Right, 75
               Down , 30
               Right, 83
               Up   , 83
               Left , 12
               Down , 49
               Right, 71
               Up   ,  7
               Left , 72 ]

        "U62,R66,U55,R34,D71,R55,D58,R83",
        Some [ Up   , 62
               Right, 66
               Up   , 55
               Right, 34
               Down , 71
               Right, 55
               Down , 58
               Right, 83 ]

        "R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51",
        Some [ Right, 98
               Up   , 47
               Right, 26
               Down , 63
               Right, 33
               Up   , 87
               Left , 62
               Down , 20
               Right, 33
               Up   , 53
               Right, 51 ]

        "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7",
        Some [ Up   , 98
               Right, 91
               Down , 20
               Right, 16
               Down , 67
               Right, 40
               Up   ,  7
               Right, 15
               Up   ,  6
               Right,  7 ]

        "U98 R91", None
        "X98,R91", None
        "U98,Rxx", None
    |]
    |> Seq.map Tuple2.toObjArray

[<Theory>]
[<MemberData("Segment parseList test cases")>]
let ``Segments parse converts a comma-separated string to list of Segments`` str expected =
    Segments.parse str =! expected


[<Theory>]
[<InlineData(0, 0,  3, 3,   6)>]
[<InlineData(3, 3,  0, 0,   6)>]
[<InlineData(1, 1,  4, 4,   6)>]
[<InlineData(4, 4,  1, 1,   6)>]
[<InlineData(1, 1,  7, 6,  11)>]
[<InlineData(7, 6,  1, 1,  11)>]
let ``Point manhattanDistanceTo calculates the Manhattan distance between to points`` x1 y1 x2 y2 expected =
    (x1, y1) |> Point.manhattanDistanceTo (x2, y2) =! expected



let ``Points along test cases`` =
    [|
        Up   , 3, [ ( 0,  1); ( 0,  2); ( 0,  3) ]
        Down , 3, [ ( 0, -1); ( 0, -2); ( 0, -3) ]
        Left , 3, [ (-1,  0); (-2,  0); (-3,  0) ]
        Right, 3, [ ( 1,  0); ( 2,  0); ( 3,  0) ]
    |]
    |> Seq.map Tuple3.toObjArray

[<Theory>]
[<MemberData("Points along test cases")>]
let ``Points along returns the list of points along a segment starting with a given point`` direction distance expected =
    let segment = direction, distance
    (0, 0) |> Points.along segment =! expected


[<Fact>]
let ``Wire fromSegments builds a wire from a list of segments`` () =
    let segments = (Right, 8) :: (Up, 5) :: (Left, 5) :: (Down, 3) :: []
    let expected = [ (1,0); (2,0); (3,0); (4,0); (5,0); (6,0); (7,0); (8,0);
                     (8,1); (8,2); (8,3); (8,4); (8,5);
                     (7,5); (6,5); (5,5); (4,5); (3,5);
                     (3,4); (3,3); (3,2) ]
    segments |> Wire.fromSegments =! expected


let ``Wire distanceBefore test cases`` =
    [|
        [Right, 8; Up, 5; Left, 5; Down, 3], 8,3, (Some 10)
        [Right, 8; Up, 5; Left, 5; Down, 3], 2,2, None
        [Right, 8; Up, 5; Left, 5; Down, 3], 3,2, (Some 20)
        [Right, 8; Up, 5; Left, 5; Down, 3], 5,8, None
    |]
    |> Seq.map Tuple4.toObjArray

[<Theory>]
[<MemberData("Wire distanceBefore test cases")>]
let ``Wire distanceBefore returns the distance before reaching the given point in a wire or None if the point is not there`` segments x y expected =
    let wire = Wire.fromSegments segments
    let point = x,y
    wire |> Wire.distanceBefore point =! expected


[<Theory>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72",          "U62,R66,U55,R34,D71,R55,D58,R83",      159)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)>]
let ``calcManhattanDistanceToNearestIntersection determines the Manhattan Distance to the nearst intersection of two wires`` wire1 wire2 expected =
    calcManhattanDistanceToNearestIntersection wire1 wire2 =! expected


[<Theory>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72",          "U62,R66,U55,R34,D71,R55,D58,R83",      610)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)>]
let ``calcDistanceOfShortestCircuit determines the number of steps in the shortest circuit of two intersecting wires`` wire1 wire2 expected =
    test <@ calcDistanceOfShortestCircuit wire1 wire2 = expected @>
