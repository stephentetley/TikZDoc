// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc



open System.IO

open TikZDoc.Internal


[<AutoOpen>]
module TikZ = 
    open Internal.LaTeXSyntax.LaTeXSyntax

    let command (name:string) (options: LaTeX option) (parameters: LaTeX option) : LaTeX = 
        Command(name, options, parameters)

    // A command with no otions or parameters
    let commandZero (name:string) : LaTeX = 
        Command(name,None,None)

    let cmdbegin (name:string) (options:LaTeX option) : LaTeX = 
        command "begin" options None

    //let block (name:string) (body:LaTeX) : LaTeX =
    //    command "begin"
        