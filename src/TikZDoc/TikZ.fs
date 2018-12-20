// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc



open System.IO

open TikZDoc.Internal


[<AutoOpen>]
module TikZ = 

    let command (name:string) = 
        printfn "Hello from TikzDoc"