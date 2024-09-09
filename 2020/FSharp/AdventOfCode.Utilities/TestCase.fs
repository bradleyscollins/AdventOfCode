module AdventOfCode.Utilities.TestCase

let fromPair = Tuple2.toObjArray
let fromTriple = Tuple3.toObjArray

let pairs cases = cases |> Seq.map fromPair
let triples cases = cases |> Seq.map fromTriple

