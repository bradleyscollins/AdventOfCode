namespace AdventOfCode.Utilities

[<AutoOpen>]
module CommonFunctions =
    let flip f x y = f y x

    let juxt f g x = f x, g x

