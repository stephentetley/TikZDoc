// Copyright (c) Stephen Tetley 2018
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


    let commandZero (name:string) : LaTeX = raw ("\\" + name)

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

    
    let documentclass (name:string) (options:LaTeX list) : LaTeX = 
        command "documentclass" options [raw name]

    let usepackage (name:string) (options:LaTeX list) : LaTeX = 
        command "usepackage" options [raw name]

    /// _Cmd suffix as begin is a keyword
    let beginCmd (name:string) (options:LaTeX list) : LaTeX = 
        command "begin" options [raw name]
    
    /// _Cmd suffix as end is a keyword
    let endCmd (name:string) : LaTeX = 
        command "end" [] [raw name]

    let block (name:string) (options:LaTeX list) (body:LaTeX) : LaTeX =
        beginCmd name options ^@@^ body ^@@^ endCmd name
        