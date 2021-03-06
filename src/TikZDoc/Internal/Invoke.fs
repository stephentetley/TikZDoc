﻿// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

namespace TikZDoc.Internal

module Invoke = 

    open System.IO
    
    open SLFormat.CommandOptions
    open TikZDoc.Internal.Common




    // ************************************************************************
    // Invoking TeX programs

    
    /// > latex "<InputFile.tex>" 
    /// The dvi file is inputfile with extension changed to ".dvi"
    let runLatex (shellWorkingDirectory:string) 
                 (texFile:string) : Result<int, string> =
        let args = [ literal <| doubleQuote texFile ]
        SimpleInvoke.executeProcess (Some shellWorkingDirectory) "latex" args



    /// > lualatex --output-format=dvi "<input.tex>"
    let runLualatex (shellWorkingDirectory:string) 
                    (texFile : string) : Result<int, string> =
        let args = 
            [ argument "--output-format"   &= "dvi"
            ; literal (doubleQuote texFile) ]
        SimpleInvoke.executeProcess (Some shellWorkingDirectory) "lualatex" args


    /// > dvips -o "<psFile>" "<dviFile>"
    let runDvips (shellWorkingDirectory:string) 
                 (dviFile:string) 
                 (psFile : string) : Result<int, string> =
        let args = 
            [ argument "-o" &^ doubleQuote psFile
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.executeProcess (Some shellWorkingDirectory) "dvips" args

    /// > dvipdfm -o "<pdfFile>" "<dviFile>"
    let runDvipdfm (shellWorkingDirectory:string) 
                   (dviFile:string) 
                   (pdfFile : string) : Result<int, string> =
        let args = 
            [ argument "-o" &^ doubleQuote pdfFile
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.executeProcess (Some shellWorkingDirectory) "dvipdfm" args

    /// > dvisvgm --output="<svgFile>" --bbox=none "<dviFile>"
    let runDvisvgm (shellWorkingDirectory:string) 
                   (dviFile:string) 
                   (svgFile : string) : Result<int, string> =
        let args = 
            [ argument "--output"   &= (doubleQuote svgFile) 
            ; argument "--bbox"     &= "none"
            ; literal (doubleQuote dviFile) ]
        SimpleInvoke.executeProcess (Some shellWorkingDirectory) "dvisvgm" args





