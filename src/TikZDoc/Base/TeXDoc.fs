// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base




[<AutoOpen>]
module TeXDoc = 

    open System.IO
  
    open SLFormat  

    open TikZDoc.Internal
    open TikZDoc.Base

    type OutputFormat = 
         | PostScript 
         | PDF 
         | SVG
         
    let internal documentProlog (format : OutputFormat) : Pretty.Doc = 
        match format with 
        | PostScript -> Pretty.text "\\documentclass{article}"
        | PDF -> Pretty.vcat [ Pretty.text "\documentclass{article}" 
                            ; Pretty.text "\\def\\pgfsysdriver{pgfsys-dvipdfm.def}" ]
        | SVG -> Pretty.text "\\documentclass[dvisvgm]{minimal}"

    type TeXDoc = 
        private | TeXDoc of format : OutputFormat * lineWidth : int * body : LaTeX
        

        member internal x.Format 
            with get() = match x with | TeXDoc(fmt, _, _) -> fmt

        member x.SaveToTeX(outputDirectory:string, texFileName:string) : string = 
            match x with
            | TeXDoc(fmt, lw, body) -> 
                let doc = Pretty.vcat [ documentProlog fmt; body.Body]
                let texFilePath = Path.Combine(outputDirectory, texFileName)
                Pretty.writeDoc lw texFilePath doc
                texFilePath

        member x.Render () : string = 
            match x with
            | TeXDoc(fmt, lw, body) -> 
                let doc = Pretty.vcat [ documentProlog fmt; body.Body]
                Pretty.render lw doc

        member x.Output (outputDirectory : string, fileName : string) = 
            let outputFilePath = Path.Combine(outputDirectory, fileName)
            let texFileName = Path.ChangeExtension(path = fileName, extension = "tex")
            let texFilePath = x.SaveToTeX(outputDirectory, texFileName)
            Invoke.runLatex outputDirectory texFilePath
            let dviFilePath = Path.ChangeExtension(outputFilePath, "dvi")
            match x.Format with
            | PostScript -> 
                Invoke.runDvips outputDirectory dviFilePath outputFilePath
            | PDF -> 
                Invoke.runDvipdfm outputDirectory dviFilePath outputFilePath
            | SVG -> 
                Invoke.runDvisvgm outputDirectory dviFilePath outputFilePath

    let makeTeXForPdf (body : LaTeX) : TeXDoc = TeXDoc(PDF, 120, body)
    let makeTeXForPs (body : LaTeX) : TeXDoc = TeXDoc(PostScript, 120, body)
    let makeTeXForSvg (body : LaTeX) : TeXDoc = TeXDoc(SVG, 120, body)

    let alterLineWidth (lineWidth : int) (doc : TeXDoc) : TeXDoc = 
        match doc with | TeXDoc(fmt, _, body) -> TeXDoc(fmt, lineWidth, body)





///// Note - this procedure creates a number of auxiliary files that you 
///// may want to (manually) delete.
//member x.SaveToPS(outputDirectory:string, psFileName:string) : unit = 
//    let doc = Pretty.vcat [PostScript.DocumentProlog; x.Body]
//    let outputPsFile = Path.Combine(outputDirectory, psFileName)
//    let texFile = Path.ChangeExtension(outputPsFile, "tex")
//    let dviFile = Path.ChangeExtension(texFile, "dvi")
//    Pretty.writeDoc 80 texFile doc
//    Invoke.runLatex outputDirectory texFile
//    Invoke.runDvips outputDirectory dviFile outputPsFile

///// Note - this procedure creates a number of auxiliary files that you 
///// may want to (manually) delete.
//member x.SaveToPDF(outputDirectory:string, pdfFileName:string) : unit = 
//    let doc = Pretty.vcat [PDF.DocumentProlog; x.Body]
//    let outputPdfFile = Path.Combine(outputDirectory, pdfFileName)
//    let texFile = Path.ChangeExtension(outputPdfFile, "tex")
//    let dviFile = Path.ChangeExtension(texFile, "dvi")
//    Pretty.writeDoc 80 texFile doc
//    Invoke.runLatex outputDirectory texFile
//    Invoke.runDvipdfm outputDirectory dviFile outputPdfFile

///// Note - this procedure creates a number of auxiliary files that you 
///// may want to (manually) delete.
//member x.SaveToSVG(outputDirectory:string, svgFileName:string) : unit = 
//    let doc = Pretty.vcat [SVG.DocumentProlog; x.Body]
//    let outputSvgFile = Path.Combine(outputDirectory, svgFileName)
//    let texFile = Path.ChangeExtension(outputSvgFile, "tex")
//    let dviFile = Path.ChangeExtension(texFile, "dvi")
//    Pretty.writeDoc 80 texFile doc
//    Invoke.runLatex outputDirectory texFile
//    Invoke.runDvisvgm outputDirectory dviFile outputSvgFile

