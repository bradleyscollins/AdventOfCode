namespace AdventOfCode.Utilities

type Tree<'TBranch, 'TLeaf> =
| Leaf   of 'TLeaf
| Branch of 'TBranch * Tree<'TBranch, 'TLeaf> seq

module Tree =
    let subtrees =
        function
        | Branch (_, subtrees') -> subtrees'
        | _                     -> Seq.empty

    let rec catamorph fBranch fLeaf =
        function
        | Leaf x -> fLeaf x
        | Branch (x, subtrees) ->
            let recurse = catamorph fBranch fLeaf
            fBranch x (subtrees |> Seq.map recurse)

    let rec fold fBranch fLeaf acc =
        function
        | Leaf x -> fLeaf acc x
        | Branch (x, subtrees) ->
            let recurse = fold fBranch fLeaf
            let acc' = fBranch acc x
            subtrees |> Seq.fold recurse acc'

    let rec map fBranch fLeaf = 
        function
        | Leaf x -> Leaf (fLeaf x) 
        | Branch (x, subtrees) -> 
            let recurse = map fBranch fLeaf
            Branch (fBranch x, subtrees |> Seq.map recurse)

    let size tree =
        let inline countNode sum _ = sum + 1
        tree |> fold countNode countNode 0

    let any pBranch pLeaf = fold pBranch pLeaf false
    let all pBranch pLeaf = fold pBranch pLeaf true
