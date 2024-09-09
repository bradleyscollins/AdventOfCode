module AdventOfCode.Utilities.String

open System

let chars (s : string) = s.ToCharArray ()
let trim (s : string) = s.Trim ()
let split (sep : string array) (s : string) = s.Split (sep, StringSplitOptions.None)
let splitNoEmpties (sep : string array) (s : string) = s.Split (sep, StringSplitOptions.RemoveEmptyEntries)
let lines = split [| "\r\n"; "\n"; "\r" |]
let replace (oldStr : string) newStr (s : string) = s.Replace (oldStr, newStr)
