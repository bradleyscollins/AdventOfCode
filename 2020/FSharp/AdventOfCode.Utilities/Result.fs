module AdventOfCode.Utilities.Result

let isOk = function Ok _ -> true | _ -> false

let isError = function Error _ -> true | _ -> false // (not << isOk) caused errors (?!)

let resolve valueIfError =
    function
    | Error _ -> valueIfError
    | Ok x    -> x

let map2 mapping res1 res2 =
    match res1, res2 with
    | (Ok x, Ok y) -> Ok (mapping x y)
    | (Error x, _)
    | (_      , Error x) -> Error x

let sequence (results : Result<'TOk, 'TError> list) : Result<'TOk list, 'TError> =
    let appendOne xs x = xs @ [x]

    results
    |> List.fold (map2 appendOne) (Ok [])

let toOption =
    function
    | Ok x -> Some x
    | _    -> None

