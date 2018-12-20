// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

#load "..\src\TikZDoc\Internal\PrettyPrint.fs"
#load "..\src\TikZDoc\Internal\PrintLaTeX.fs"
#load "..\src\TikZDoc\Internal\LaTeXSyntax.fs"
#load "..\src\TikZDoc\TikZ.fs"

open TikZDoc.Internal.PrettyPrint
open TikZDoc
open TikZDoc.Internal.LaTeXSyntax.LaTeXSyntax

let output (doc:LaTeX) : unit = 
    printfn "%s" << render 80 <| doc.RenderToDoc ()

let test01 () = 
    output <| cmdbegin "document" None
