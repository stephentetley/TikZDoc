// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc



open System.IO

open TikZDoc.Internal


[<AutoOpen>]
module TikZ = 

    open TikZDoc.Internal.LaTeXDoc
    open TikZDoc.Internal

    type LaTeX = LaTeXDocument

    let raw (text:string) : LaTeX = rawText text

    let (^^) (d1:LaTeX) (d2:LaTeX) = liftCat PrettyPrint.beside d1 d2

    let (^+^) (d1:LaTeX) (d2:LaTeX) = liftCat PrettyPrint.besideSpace d1 d2

    let (^@@^) (d1:LaTeX) (d2:LaTeX) = liftCat PrettyPrint.below d1 d2

    let commandZero (name:string) : LaTeX = raw ("\\" + name)

    let command (name:string) (options: LaTeX list) (parameters: LaTeX list) : LaTeX = 
        let cmdOptions = 
            match options with
            | [] -> empty
            | xs -> optionsList xs
        let cmdParams = 
            match parameters with
            | [] -> empty
            | xs -> parametersList xs
        commandZero name ^^ cmdOptions ^^ cmdParams




    let cmdBegin (name:string) (options:LaTeX list) : LaTeX = 
        command "begin" options [raw name]
    
    let cmdEnd (name:string) : LaTeX = 
        command "end" [] [raw name]

    let block (name:string) (options:LaTeX list) (body:LaTeX) : LaTeX =
        cmdBegin name options ^@@^ body ^@@^ cmdEnd name
        