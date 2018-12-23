// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc



[<AutoOpen>]
module LaTeXInvoke = 
    
    open TikZDoc.TikZLaTeX

    type Output = 
        | PostScript 
        | PDF 
        | SVG

        member x.DocumentProlog with get() : LaTeX = 
            match x with 
            | PostScript -> documentclass "article" [] 
    
