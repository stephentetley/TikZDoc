// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

#r "netstandard"

#I @"C:\Users\stephen\.nuget\packages\slformat\1.0.2-alpha-20190207\lib\netstandard2.0"
#r "SLFormat.dll"

#load "..\src\TikZDoc\Internal\Common.fs"
#load "..\src\TikZDoc\Internal\Invoke.fs"
#load "..\src\TikZDoc\Internal\LaTeXDocument.fs"
#load "..\src\TikZDoc\Base\LaTeX.fs"
#load "..\src\TikZDoc\Base\TikZBase.fs"
#load "..\src\TikZDoc\Base\Properties.fs"

open System.IO
open TikZDoc.Base


let workingDirectory = Path.Combine(__SOURCE_DIRECTORY__, "..", "output")

let output (tex:GenLaTeX<'x>) : unit = 
    tex.Render(lineWidth = 80) |> printfn "%s"

let test01 () = 
    output <| beginCmd [] "document"

let test02 () = 
    output <| 
             comment "Author: SPT"
        ^@@^ documentclass [] "minimal"
        ^@@^ usepackage [] "tikz"

let doc1 () : LaTeX = 
    vcat 
        [ usepackage [] "tikz"      
        ; beginCmd [] "document" 
        ; beginCmd [] "tikzpicture" 
        ; castLaTeX <| draw [thick; roundedCornersDims (PT 8.0)]
        ; raw "(0,0) -- (0,2) -- (1,3.25) -- (2,2) -- (2,0) -- (0,2) -- (2,2) -- (0,0) -- (2,0);"
        ; endCmd "tikzpicture"
        ; endCmd "document"
        ]

let test03 () = 
    let doc = doc1 ()
    doc.SaveToSVG(workingDirectory, "example1.svg")

let dummy () = 
    output <| Coord(2.0,3.0).LaTeX
