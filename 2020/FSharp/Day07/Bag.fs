namespace Day07

open AdventOfCode.Utilities

type Content = Color * int
type Bag = Tree<Content, Content>

module Content =
    let color = fst
    let count = snd

module Bag =
    let build (rm : RuleMap) color : Bag =
        let rec buildBag (color', count') =
            let content = color', count'
            match rm.[color'] |> Map.toList with
            | []          -> Leaf content
            | subcontents -> Branch (content, subcontents |> Seq.map buildBag)
            
        buildBag (color, 1)

    let contains color bag =
        let hasColor = Content.color >> ((=) color)
        let pred alreadyFound color' = alreadyFound || hasColor color'

        bag |> Tree.subtrees |> Seq.exists (Tree.any pred pred)

    let size bag =
        let fLeaf = Content.count
        let fBranch content sizesOfSubcontents =
            let count = (content |> Content.count)
            count + (count * (sizesOfSubcontents |> Seq.sum))
        
        (bag |> Tree.catamorph fBranch fLeaf) - 1 // don't count bag itself
