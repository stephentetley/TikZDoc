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

    type DviProcedure = 
        | UseLuaLaTeX
        | UseLaTeX
         
    let internal documentProlog (format : OutputFormat) : Pretty.Doc = 
        match format with 
        | PostScript -> Pretty.text "\\documentclass{article}"
        | PDF -> Pretty.vcat [ Pretty.text "\documentclass{article}" 
                            ; Pretty.text "\\def\\pgfsysdriver{pgfsys-dvipdfm.def}" ]
        | SVG -> Pretty.text "\\documentclass[dvisvgm]{minimal}"

    

    type TeXDoc = 
        private { Format : OutputFormat
                  LineWidth : int 
                  DviPath : DviProcedure
                  BodyDoc : LaTeX
                  }
       

        member x.SaveToTeX(outputDirectory:string, texFileName:string) : string = 
            let doc = Pretty.vcat [ documentProlog x.Format; x.BodyDoc.Body]
            let texFilePath = Path.Combine(outputDirectory, texFileName)
            Pretty.writeDoc x.LineWidth texFilePath doc
            texFilePath

        member x.Render () : string = 
            let doc = Pretty.vcat [ documentProlog x.Format; x.BodyDoc.Body]
            Pretty.render x.LineWidth doc

    let makeTeXForPdf (body : LaTeX) : TeXDoc = 
        { Format = PDF
          LineWidth = 120
          DviPath = UseLuaLaTeX
          BodyDoc = body
        }

    let makeTeXForPs (body : LaTeX) : TeXDoc = 
        { Format = PostScript
          LineWidth = 120
          DviPath = UseLuaLaTeX
          BodyDoc = body
        }

    let makeTeXForSvg (body : LaTeX) : TeXDoc = 
        { Format = SVG
          LineWidth = 120
          DviPath = UseLuaLaTeX
          BodyDoc = body
        }

    let alterLineWidth (lineWidth : int) (doc : TeXDoc) : TeXDoc = 
        { doc with LineWidth = lineWidth } 

    let alterDviGenerator (dviProcedure : DviProcedure) (doc : TeXDoc) : TeXDoc = 
        { doc with DviPath = dviProcedure }


    let private makeDviFile (dviProc : DviProcedure)
                            (outputDirectory : string) 
                            (texFilePath : string) : Result<int, string> = 
        match dviProc with
        | UseLuaLaTeX -> 
            Invoke.runLualatex outputDirectory texFilePath
        | UseLaTeX -> 
            Invoke.runLatex outputDirectory texFilePath



    let outputTeXDoc (doc : TeXDoc) 
                     (outputDirectory : string) 
                     (fileName : string) : Result<int, string> = 
        let outputFilePath = Path.Combine(outputDirectory, fileName)
        let texFileName = Path.ChangeExtension(path = fileName, extension = "tex")
        let texFilePath = doc.SaveToTeX(outputDirectory, texFileName)

        match makeDviFile doc.DviPath outputDirectory texFilePath with
        | Error msg -> Error msg
        | Ok _ -> 
            let dviFilePath = Path.ChangeExtension(outputFilePath, "dvi")
            match doc.Format with
            | PostScript -> 
                Invoke.runDvips outputDirectory dviFilePath outputFilePath
            | PDF -> 
                Invoke.runDvipdfm outputDirectory dviFilePath outputFilePath
            | SVG -> 
                Invoke.runDvisvgm outputDirectory dviFilePath outputFilePath
