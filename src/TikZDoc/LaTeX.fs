// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc




[<AutoOpen>]
module LaTeX = 

    open System.IO
  
    open SLFormat  
    open TikZDoc.Internal.Common  
    open TikZDoc.Internal

    /// 'General' LaTeX = not specialized to a specific type of LaTeX fragment
    type GenLaTeX<'a> = LaTeXDocument.LaTeXDoc<'a>

    let castLaTeX (doc:GenLaTeX<'a>) : GenLaTeX<'x> = LaTeXDocument.cast doc
        

    type LaTeXPhantom = class end

    /// A Specific type for LaTeX in the prolog before "\\tikzpicture"
    type LaTeX = GenLaTeX<LaTeXPhantom>


    let empty : GenLaTeX<'a> = LaTeXDocument.empty

    let raw : string -> GenLaTeX<'a> = LaTeXDocument.rawText

    let ( ^^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   = 
        LaTeXDocument.liftCat Pretty.beside doc1 doc2

    let ( ^+^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   =
        LaTeXDocument.liftCat Pretty.besideSpace doc1 doc2

    let ( ^@@^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x> = 
        LaTeXDocument.liftCat Pretty.(^@@^) doc1 doc2

    let hcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        LaTeXDocument.liftCats Pretty.hcat docs

    let hsep (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        LaTeXDocument.liftCats Pretty.hcatSpace docs

    let vcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        LaTeXDocument.liftCats Pretty.vcat docs

    let parens (doc1:GenLaTeX<'a>) : GenLaTeX<'x> = 
        LaTeXDocument.liftOp Pretty.parens doc1


    let comment (text: string) : GenLaTeX<'a> = 
        let comment1 (s:string) = raw ("% " + s)
        toLines text |> List.map comment1 |> vcat

    let arguments (args:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        LaTeXDocument.argumentsList args

    let options (opts:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        LaTeXDocument.optionsList opts
    
    
    /// <propertyName>=<propertyValue>
    let property (propertyName:string) (propertyValue:GenLaTeX<'a>) : GenLaTeX<'x> = 
        raw propertyName  ^^ raw "=" ^^ propertyValue


    /// \<name>
    let commandZero (name:string) : GenLaTeX<'a> = raw ("\\" + name)

    /// \<name>[<options>]{<arguments>}
    let command (name:string) (options: GenLaTeX<'a> list) (arguments: GenLaTeX<'b> list) : GenLaTeX<'x> = 
        let opts = 
            match options with
            | [] -> empty
            | xs -> LaTeXDocument.optionsList xs
        let args = 
            match arguments with
            | [] -> empty
            | xs -> LaTeXDocument.argumentsList xs
        commandZero name ^^ opts ^^ args

    /// \def\<name><body>
    /// No attempt is made to match TeX syntax for the macro body.
    let def (name:string) (body:GenLaTeX<'a>) : GenLaTeX<'x> = 
        commandZero "def" ^^ commandZero name ^^ body

    /// \documentclass[<options>]{<name>}
    let documentclass (options:GenLaTeX<'a> list) (name:string) : LaTeX = 
        command "documentclass" options [raw name]

    /// \usepackage[<options>]{<name>}
    let usepackage (options:GenLaTeX<'a> list) (name:string) : LaTeX = 
        command "usepackage" options [raw name]

    /// \begin[<options>]{<name>}
    /// _Cmd suffix as begin is a keyword in F#.
    let beginCmd (options:GenLaTeX<'a> list) (name:string) : GenLaTeX<'x> = 
        command "begin" options [raw name]
    
    /// \end{<name>}
    /// _Cmd suffix as end is a keyword in F#.
    let endCmd (name:string) : GenLaTeX<'a> = 
        command "end" [] [raw name]

    let block (options:GenLaTeX<'a> list) (name:string)  (body:GenLaTeX<'b>) : GenLaTeX<'x> =
        beginCmd options name ^@@^ body ^@@^ endCmd name

        