namespace AdventOfCode.Utilities

open System

module Byte =
    let parseFromBase (fromBase : int) (s : string) = Convert.ToByte (s, fromBase)

module Int32 =
    let parseFromBase (fromBase : int) (s : string) = Convert.ToInt32 (s, fromBase)

[<AutoOpen>]
module NumberPatterns =
    let (|Integer|_|) (str : string) =
       match Int32.TryParse str with
       | true, x -> Some x
       | _       -> None

