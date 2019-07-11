// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

#r "netstandard"

#I @"C:\Users\stephen\.nuget\packages\slformat\1.0.2-alpha-20190616\lib\netstandard2.0"
#r "SLFormat.dll"

#load "..\src\TikZDoc\Internal\Common.fs"
#load "..\src\TikZDoc\Internal\Invoke.fs"
#load "..\src\TikZDoc\Internal\Syntax.fs"
#load "..\src\TikZDoc\Base\GenLaTeX.fs"
#load "..\src\TikZDoc\Base\LaTeX.fs"
#load "..\src\TikZDoc\Base\TikZBase.fs"
#load "..\src\TikZDoc\Base\Properties\Misc.fs"
#load "..\src\TikZDoc\Base\Properties\Colors.fs"
#load "..\src\TikZDoc\Extensions\Forest\Forest.fs"

open System.IO
open TikZDoc.Base
open TikZDoc.Base.Properties
open TikZDoc.Extensions.Forest


let workingDirectory = Path.Combine(__SOURCE_DIRECTORY__, "..", "output")

let fillProp (value : GenLaTeX<'a>) : TikZProperty = 
    rawtext "fill" ^=^ value
     
let directioni (i : int) = latexInt i

let redNode (label : GenLaTeX<'a>) (kids : ForestTikZ list) : ForestTikZ =
    let label1 = addNodeArgs label [ fillProp Colors.red]
    forestNode label1 kids


let forestDoc () = 
    forestDocument ["edges"] 
        <| vcat [ forTree [growTick <| latexInt 0; folderProp; drawProp]
                ; redNode (text "tree") []
                ]

let test01 () = 
    let doc = forestDoc ()
    printfn "%s" workingDirectory
    doc.Render 180 |> printfn "%s"
    doc.SaveToSVG(workingDirectory, "forest01.svg")


