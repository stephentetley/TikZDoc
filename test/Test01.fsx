// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

#r "netstandard"

#I @"C:\Users\stephen\.nuget\packages\slformat\1.0.2-alpha-20190712\lib\netstandard2.0"
#r "SLFormat.dll"

#load "..\src\TikZDoc\Internal\Common.fs"
#load "..\src\TikZDoc\Internal\Invoke.fs"
#load "..\src\TikZDoc\Base\GenLaTeX.fs"
#load "..\src\TikZDoc\Base\LaTeX.fs"
#load "..\src\TikZDoc\Base\TeXDoc.fs"
#load "..\src\TikZDoc\Base\TikZBase.fs"
#load "..\src\TikZDoc\Base\Properties\Misc.fs"
#load "..\src\TikZDoc\Base\Properties\Path.fs"

open System.IO
open TikZDoc.Base
open TikZDoc.Base.Properties.Path

let workingDirectory = Path.Combine(__SOURCE_DIRECTORY__, "..", "output")

let output (latex:GenLaTeX<'x>) : unit = 
    let tex = alterLineWidth 80 (makeTeXForPdf <| castLaTeX latex)
    tex.Render() |> printfn "%s"

let test01 () = 
    output <| beginCmd [] "document"

let test02 () = 
    output <| 
             comment "Author: SPT"
        ^!!^ documentclass [] "minimal"
        ^!!^ usepackage "tikz"


let doc1 () : LaTeX = 
    vcat 
        [ usepackage "tikz"      
        ; document []
            (tikzpicture []
                (vcat [ draw [thick; roundedCornersDims (PT 8.0M)]
                      ; rawtext "(0,0) -- (0,2) -- (1,3.25) -- (2,2) -- (2,0) -- (0,2) -- (2,2) -- (0,0) -- (2,0);"
                      ]))
        ]

let test03 () = 
    let tex = doc1 () |> makeTeXForSvg
    tex.Output(workingDirectory, "example1.svg")

let dummy () = 
    output <| Coord(2.0,3.0).ToLaTeX ()
