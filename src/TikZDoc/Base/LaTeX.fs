// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base




[<AutoOpen>]
module LaTeX = 

    open System.IO
  
    open SLFormat  
    open TikZDoc.Internal.Common  
    open TikZDoc.Internal


    type LaTeXPhantom = class end

    /// A specific type for LaTeX, e.g. in the prolog before "\\tikzpicture"
    type LaTeX = GenLaTeX<LaTeXPhantom>


    /// \usepackage{<name>}
    let usepackage (name:string) : LaTeX = 
        command "usepackage" [] [rawtext name]

    /// \documentclass[<options>]{<name>}
    let documentclass (options:GenLaTeX<'a> list) (name:string) : LaTeX = 
        command "documentclass" options [rawtext name]


    let document  (options:GenLaTeX<'a> list) (body:GenLaTeX<'b>) : LaTeX = 
        environment options "document" body



        