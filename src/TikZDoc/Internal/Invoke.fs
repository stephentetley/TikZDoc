// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

namespace TikZDoc.Internal

module Invoke = 

    open System.IO
    
    open SLFormat.CommandOptions
    open TikZDoc.Internal.Common




    // ************************************************************************
    // Invoking TeX programs


    // Missing from SLFormat
    let (&^) (cmd:CmdOpt) (s:string) : CmdOpt = cmd ^^ character ' ' ^^ literal s
    
    /// > latex "<InputFile>.tex"         
    let runLatex (shellWorkingDirectory:string) (finalName:string) : unit =
        let texFile = Path.ChangeExtension(finalName, "tex")
        let args = [ literal <| doubleQuote texFile ]
        SimpleInvoke.runProcessSimple (Some shellWorkingDirectory) "latex" args

    /// > dvips -o "<FinalName>" "<RootName>.dvi"
    let runDvips (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let psFile = finalName
        let args = 
            [ argument "-o" &^ doubleQuote psFile
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.runProcessSimple (Some shellWorkingDirectory) "dvips" args

    /// > dvipdfm -o "<FinalName>" "<RootName>.dvi"
    let runDvipdfm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let pdfFile = finalName
        let args = 
            [ argument "-o" &^ doubleQuote pdfFile
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.runProcessSimple (Some shellWorkingDirectory) "dvipdfm" args

    /// > dvisvgm --output="<FinalName>" --bbox=none "<RootName>.dvi"
    let runDvisvgm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let svgFile = finalName
        let args = 
            [ argument "--output"   &= (doubleQuote svgFile) 
            ; argument "--bbox"     &= "none"
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.runProcessSimple (Some shellWorkingDirectory) "dvisvgm" args



