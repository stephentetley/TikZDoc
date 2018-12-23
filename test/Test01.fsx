// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

#load "..\src\TikZDoc\Internal\Common.fs"
#load "..\src\TikZDoc\Internal\PrettyPrint.fs"
#load "..\src\TikZDoc\Internal\LaTeXDoc.fs"
#load "..\src\TikZDoc\LaTeX.fs"
#load "..\src\TikZDoc\TikZ.fs"

open TikZDoc


let output (tex:LaTeX) : unit = 
    printfn "%s" <| tex.Render(lineWidth = 80)

let test01 () = 
    output <| beginCmd "document" []

let test02 () = 
    output <| 
             comment "Author: SPT"
        ^@@^ documentclass "minimal" []
        ^@@^ usepackage "tikz" []

    
