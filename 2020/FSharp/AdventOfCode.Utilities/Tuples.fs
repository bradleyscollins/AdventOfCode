namespace AdventOfCode.Utilities

module Tuple2 =
    let toObjArray (a, b) = [| box a; box b |]

module Tuple3 =
    let toObjArray (a, b, c) = [| box a; box b; box c |]
