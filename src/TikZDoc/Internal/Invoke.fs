// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

namespace TikZDoc.Internal

module Invoke = 

    open System.IO
    
    open SLFormat.CommandOptions
    open TikZDoc.Internal.Common




    // ************************************************************************
    // Invoking TeX programs

    // Running a process (e.g markdown)
    let private executeProcess (workingDirectory:string) (toolPath:string) (command:string) : Choice<string,int> = 
        try
            let procInfo = new System.Diagnostics.ProcessStartInfo ()
            procInfo.WorkingDirectory <- workingDirectory
            procInfo.FileName <- toolPath
            procInfo.Arguments <- command
            procInfo.CreateNoWindow <- true
            let proc = new System.Diagnostics.Process()
            proc.StartInfo <- procInfo
            proc.Start() |> ignore
            proc.WaitForExit () 
            Choice2Of2 <| proc.ExitCode
        with
        | ex -> Choice1Of2 (sprintf "executeProcess: \n%s" ex.Message)

    let shellRun (workingDirectory:string) (toolPath:string) (command:string)  : unit = 
        try
            match executeProcess workingDirectory toolPath command with
            | Choice1Of2(errMsg) -> failwith errMsg
            | Choice2Of2(code) -> 
                if code <> 0 then
                    failwithf "shellRun fail - error code: %i" code
                else ()
        with
        | ex -> 
            let diagnosis = 
                String.concat "\n" <| 
                    [ ex.Message
                    ; sprintf "Working Directory: %s" workingDirectory 
                    ; sprintf "Command Args: %s" command
                    ]
            failwithf "shellRun exception: \n%s" diagnosis


    // Missing from SLFormat
    let (&^) (cmd:CmdOpt) (s:string) : CmdOpt = cmd ^^ character ' ' ^^ literal s
    
    /// > latex "<InputFile>.tex"         
    let runLatex (shellWorkingDirectory:string) (finalName:string) : unit =
        let texFile = Path.ChangeExtension(finalName, "tex")
        let args = [ literal <| doubleQuote texFile ]
        shellRun shellWorkingDirectory "latex" (arguments args)

    /// > dvips -o "<FinalName>" "<RootName>.dvi"
    let runDvips (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let psFile = finalName
        let args = 
            [ argument "-o" &^ doubleQuote psFile
            ; literal (doubleQuote dviFile) ]
        shellRun shellWorkingDirectory "dvips" (arguments args)

    /// > dvipdfm -o "<FinalName>" "<RootName>.dvi"
    let runDvipdfm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let pdfFile = finalName
        let args = 
            [ argument "-o" &^ doubleQuote pdfFile
            ; literal (doubleQuote dviFile) ]
        shellRun shellWorkingDirectory "dvipdfm" (arguments args)

    /// > dvisvgm --output="<FinalName>" --bbox=none "<RootName>.dvi"
    let runDvisvgm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let svgFile = finalName
        let args = 
            [ argument "--output"   &= (doubleQuote svgFile) 
            ; argument "--bbox"     &= "none"
            ; literal (doubleQuote dviFile) ]
        shellRun shellWorkingDirectory "dvisvgm" (arguments args)



