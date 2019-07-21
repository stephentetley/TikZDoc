// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

#r "netstandard"

#I @"C:\Users\stephen\.nuget\packages\slformat\1.0.2-alpha-20190721\lib\netstandard2.0"
#r "SLFormat.dll"

#load "..\src\TikZDoc\Internal\Common.fs"
#load "..\src\TikZDoc\Internal\Invoke.fs"
#load "..\src\TikZDoc\Base\GenLaTeX.fs"
#load "..\src\TikZDoc\Base\LaTeX.fs"
#load "..\src\TikZDoc\Base\TeXDoc.fs"
#load "..\src\TikZDoc\Base\TikZBase.fs"
#load "..\src\TikZDoc\Base\Properties\Misc.fs"
#load "..\src\TikZDoc\Extensions\TikZTiming\TikZTiming.fs"

open System.IO
open TikZDoc.Base
open TikZDoc.Extensions.TikZTiming


let workingDirectory = Path.Combine(__SOURCE_DIRECTORY__, "..", "output")


let output (latex:GenLaTeX<'a>) : unit = 
    let tex = makeTeXForPdf (castLaTeX latex) |> alterLineWidth 80 
    tex.Render() |> printfn "%s"

let test01 () = 
    texttiming [] [ High; High; Low ] |> output



