namespace Day07

open AdventOfCode.Utilities

type Rule = Map<Color, int>
module Rule =
    let parse line =
        let parseBag bag =
            match bag with
            | ParseRegex @"(\d+) ([\w ]+) bags?" [number; color] ->
                Some (color, int number)
            | _ -> None

        let parseContents = String.split [| ", " |] >> Seq.choose parseBag >> Map

        match line with
        | ParseRegex @"([\w ]+) bags contain no other bags\." [color] ->
            Some (color, Map.empty)
        | ParseRegex @"([\w ]+) bags contain (.+)\." [color; bags] ->
            Some (color, parseContents bags)
        | _ -> None

    let colors = Map.keys
    let counts = Map.values

