// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Internal

open TikZDoc.Internal.PrettyPrint

[<RequireQualifiedAccess>]
module PrintLaTeX = 
    
    let command (name:string) (options:Doc option) (parameters:Doc option) : Doc = 
        let doptions =
            match options with
            | None -> empty
            | Some doc -> brackets doc
        let dparams =
            match parameters with
            | None -> empty
            | Some doc -> braces doc
        text ("\\" + name ) ^^ doptions ^^ dparams

    let rawtext (source:string) : Doc = 
        text source