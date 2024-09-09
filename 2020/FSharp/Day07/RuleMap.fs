namespace Day07

open AdventOfCode.Utilities

type RuleMap = Map<Color, Rule>
module RuleMap =
    let build : (string seq -> RuleMap) = Seq.choose Rule.parse >> Map.ofSeq

    let colors = Map.keys

    let ruleFor (rm : RuleMap) color = rm |> Map.find color

    let without = Map.remove

