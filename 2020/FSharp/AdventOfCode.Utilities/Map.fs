module AdventOfCode.Utilities.Map

let keys   m = m |> Map.toSeq |> Seq.map fst |> Seq.toList
let values m = m |> Map.toSeq |> Seq.map snd |> Seq.toList

let merge map2 map1 =
    let pairs1 = map1 |> Map.toSeq
    let pairs2 = map2 |> Map.toSeq
    
    Seq.append pairs1 pairs2
    |> Map.ofSeq
