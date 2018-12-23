﻿// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc




[<AutoOpen>]
module LaTeX = 

    open TikZDoc.Internal.LaTeXDoc
    open TikZDoc.Internal

    type LaTeX = LaTeXDocument

    let raw : string -> LaTeX = rawText

    let (^^) : LaTeX -> LaTeX -> LaTeX  = liftCat PrettyPrint.beside

    let (^+^) : LaTeX -> LaTeX -> LaTeX = liftCat PrettyPrint.besideSpace

    let (^@@^) : LaTeX -> LaTeX -> LaTeX = liftCat PrettyPrint.below

    let hcat : LaTeX list -> LaTeX = liftCats PrettyPrint.hcat

    let hsep : LaTeX list -> LaTeX = liftCats PrettyPrint.hsep

    let vcat : LaTeX list -> LaTeX = liftCats PrettyPrint.vcat


    let comment (text: string) : LaTeX = 
        let comment1 (s:string) = raw ("% " + s)
        vcat << List.map comment1 <| unlines text 

    /// \<name>
    let commandZero (name:string) : LaTeX = raw ("\\" + name)

    /// \<name>[<options>]{<arguments>}
    let command (name:string) (options: LaTeX list) (arguments: LaTeX list) : LaTeX = 
        let opts = 
            match options with
            | [] -> empty
            | xs -> optionsList xs
        let args = 
            match arguments with
            | [] -> empty
            | xs -> argumentsList xs
        commandZero name ^^ opts ^^ args

    /// \def\<name><body>
    /// No attempt is made to match TeX syntax for the macro body.
    let def (name:string) (body:LaTeX) : LaTeX = 
        command "def" [] [] ^^ command name [] [] ^^ body

    /// \documentclass[<options>]{<name>}
    let documentclass (options:LaTeX list) (name:string) : LaTeX = 
        command "documentclass" options [raw name]

    /// \usepackage[<options>]{<name>}
    let usepackage (options:LaTeX list) (name:string) : LaTeX = 
        command "usepackage" options [raw name]

    /// \begin[<options>]{<name>}
    /// _Cmd suffix as begin is a keyword
    let beginCmd (options:LaTeX list) (name:string) : LaTeX = 
        command "begin" options [raw name]
    
    /// \end{<name>}
    /// _Cmd suffix as end is a keyword
    let endCmd (name:string) : LaTeX = 
        command "end" [] [raw name]

    let block (options:LaTeX list) (name:string)  (body:LaTeX) : LaTeX =
        beginCmd options name ^@@^ body ^@@^ endCmd name
        