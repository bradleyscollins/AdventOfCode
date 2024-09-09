module AdventOfCode.Utilities.Seq

open System

let apply fs xs = Seq.map2 (fun f x -> f x) fs xs

let cycle xs = seq { while true do yield! xs }

let rec drop n xs =
    if n < 1 then
        xs
    else
        match xs |> Seq.tryHead with
        | None -> Seq.empty
        | _    -> drop (n - 1) (xs |> Seq.skip 1)

let inline product xs = xs |> Seq.fold (*) Core.LanguagePrimitives.GenericOne

let reject pred = Seq.filter (not << pred)

