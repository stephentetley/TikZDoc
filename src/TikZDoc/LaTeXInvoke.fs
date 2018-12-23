// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc



[<AutoOpen>]
module LaTeXInvoke = 
    
    open System.IO  

    open TikZDoc.Internal.Common
    open TikZDoc.TikZLaTeX
    
    let private doubleQuote (s:string) : string = "\"" + s + "\""

    type Output = 
        | PostScript 
        | PDF 
        | SVG

        member x.DocumentProlog 
            with get() : LaTeX = 
                match x with 
                | PostScript -> documentclass [] "article"
                | PDF -> documentclass [] "article" ^@@^ def "pgfsysdriver" (arguments [raw "pgfsys-dvipdfm.def"])
                | SVG -> documentclass [raw "dvisvgm"] "minimal"

    type LaTeXArgs = 
        { InputPath: string 
          OutputDirectory: string  }
    
    /// > latex   --output-directory="<OutputDirectory>"  "<InputPath>"         
    let runLatex (shellWorkingDirectory:string) (args:LaTeXArgs) : unit =
        let command = sprintf "--output-directory=%s %s" (doubleQuote args.OutputDirectory) (doubleQuote args.InputPath)
        shellRun shellWorkingDirectory "latex" command

    /// > dvips -o "<PsFile>" "<DviFile>"
    let runDvips (shellWorkingDirectory:string) (args:LaTeXArgs) : unit =
        let dviFile = Path.ChangeExtension(args.InputPath, "dvi")   // TODO - not quite right
        let psFile = Path.ChangeExtension(args.InputPath, "ps")     // TODO - not quite right
        let command = sprintf "-o %s %s" (doubleQuote psFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvips" command

    /// > dvipdfm -o "<PdfFile>" "<DviFile>"
    let runDvipdfm (shellWorkingDirectory:string) (args:LaTeXArgs) : unit =
        let dviFile = Path.ChangeExtension(args.InputPath, "dvi")   // TODO - not quite right
        let pdfFile = Path.ChangeExtension(args.InputPath, "pdf")     // TODO - not quite right
        let command = sprintf "-o %s %s" (doubleQuote pdfFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvipdfm" command

    /// > dvisvgm --output="<SvgFile>" --bbox=none "<DviFile>"
    let runDvisvgm (shellWorkingDirectory:string) (args:LaTeXArgs) : unit =
        let dviFile = Path.ChangeExtension(args.InputPath, "dvi")   // TODO - not quite right
        let pdfFile = Path.ChangeExtension(args.InputPath, "svg")     // TODO - not quite right
        let command = sprintf "-output=%s --bbox=none %s" (doubleQuote pdfFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvisvgm" command
