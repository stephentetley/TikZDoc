// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Internal

open System.Text
open System.Threading


[<AutoOpen>]
module Common = 

    

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

