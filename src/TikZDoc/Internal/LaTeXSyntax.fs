// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Internal.LaTeXSyntax

open TikZDoc.Internal.PrettyPrint
open TikZDoc.Internal
open System.Text

[<AutoOpen>]
module LaTeXSyntax = 
    open TikZDoc.Internal
    
    type LaTeX = 
        | Command of string * LaTeX option * LaTeX option  // name * optional params (rendered []) * params (rendered {})
        | Raw of string
        | Vertical of LaTeX * LaTeX
        | Horizontal of LaTeX * LaTeX

        // Move to CPS? ...
        member x.RenderToDoc() : Doc = 
            let rec work (doc:LaTeX) : Doc = 
                match doc with
                | Command(name, options, parameters) ->
                    let doptions = Option.map work options
                    let dparameters = Option.map work options
                    PrintLaTeX.command name doptions dparameters
                | Raw s -> 
                    PrintLaTeX.rawtext s
                | Vertical(a,b) -> 
                    work a ^@@^ work b
                | Horizontal(a,b) -> 
                    work a ^+^ work b
            work x


