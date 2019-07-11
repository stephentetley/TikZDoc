// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base




[<AutoOpen>]
module GenLaTeX = 

    open System.IO
  
    open SLFormat  
    open TikZDoc.Internal.Common  
    open TikZDoc.Internal

    /// Accents not escaped... (yet?)
    let escapeTeX (source : string) : string = 
        let replace1 (src : string)  oldString newString = 
            src.Replace(oldValue = oldString, newValue = newString)
        source 
            |> replace1 "\\"    "$\\backslash$"
            |> replace1 "{"     "$\\{$"
            |> replace1 "}"     "$\\}$"
            |> replace1 "%"     "\\%"
            |> replace1 "&"     "\\&"
            |> replace1 "~"     "\\~{}"
            |> replace1 "$"     "\\$"
            |> replace1 "^"     "\\^{}"
            |> replace1 "_"     "\\_{}"
            |> replace1 "#"     "\\#"

    /// Change a list into either None if the list is empty or
    /// Some (list) if it is non-empty.
    let itemsToOption (items : 'a list) : option<'a list> = 
        match items with 
        | [] -> None
        | _ -> Some items

    /// 'General' LaTeX = not specialized to a specific type of LaTeX fragment
    type GenLaTeX<'a> = Syntax.WrappedDoc<'a>

    let castLaTeX (doc:GenLaTeX<'a>) : GenLaTeX<'x> = Syntax.cast doc
        

    let empty () : GenLaTeX<_> = 
        Syntax.liftDoc Pretty.empty


    /// No string escaping
    let rawtext (source : string) : GenLaTeX<'a> = 
        Syntax.liftDoc <| Pretty.text source

    let text (source : string) : GenLaTeX<'a> = 
        Syntax.liftDoc <| Pretty.text (escapeTeX source)

    let indent (body : GenLaTeX<'a>) : GenLaTeX<'a> = 
        Syntax.liftOp (Pretty.indent 4) body

    let ( ^^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   = 
        Syntax.liftCat Pretty.beside doc1 doc2

    let ( ^+^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   =
        Syntax.liftCat Pretty.besideSpace doc1 doc2

    let ( ^//^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x> = 
        Syntax.liftCat Pretty.(^@@^) doc1 doc2

    let hcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        Syntax.liftCats Pretty.hcat docs

    let hsep (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        Syntax.liftCats Pretty.hcatSpace docs

    let vcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        Syntax.liftCats Pretty.vcat docs

    let parens (body : GenLaTeX<'a>) : GenLaTeX<'x> = 
        Syntax.liftOp Pretty.parens body

    let braces (body : GenLaTeX<'a>) : GenLaTeX<'x> = 
        Syntax.liftOp Pretty.braces body

    let comment (text: string) : GenLaTeX<'a> = 
        let comment1 (s:string) = rawtext ("% " + s)
        toLines text |> List.map comment1 |> vcat


        

    /// arguments (rendered {})
    /// Note - the answer type can be diffrent to input types.
    let formatArguments (items : GenLaTeX<'a> list) : GenLaTeX<'x> = 
        Syntax.liftOp Pretty.braces <| Syntax.commaSpaceSep items

    /// optional params (rendered [])
    /// Note - the answer type can be diffrent to input types.
    let formatOptions (items : GenLaTeX<'a> list) : GenLaTeX<'x> = 
        Syntax.liftOp Pretty.brackets <| Syntax.commaSpaceSep items
    
    
    /// <propertyName>=<propertyValue>
    let keyvalue (propertyName:string) (propertyValue:GenLaTeX<'a>) : GenLaTeX<'x> = 
        text propertyName  ^^ text "=" ^^ propertyValue


    /// \<name>
    let commandZero (name:string) : GenLaTeX<'a> = rawtext ("\\" + name)

    /// \<name>[<options>]{<arguments>}
    let command (name : string) 
                (options : option<GenLaTeX<'a> list>) 
                (arguments : option<GenLaTeX<'b> list>) : GenLaTeX<'x> = 
        let opts = 
            match options with
            | None -> empty ()
            | Some xs -> formatOptions xs
        let args = 
            match arguments with
            | None -> empty ()
            | Some xs -> formatArguments xs
        commandZero name ^^ opts ^^ args

    /// \def\<name><body>
    /// No attempt is made to match TeX syntax for the macro body.
    let def (name:string) (body:GenLaTeX<'a>) : GenLaTeX<'x> = 
        commandZero "def" ^^ commandZero name ^^ body



    /// \begin[<options>]{<name>}
    /// _Cmd suffix as begin is a keyword in F#.
    let beginCmd (options : option<GenLaTeX<'a> list>) (name:string) : GenLaTeX<'x> = 
        command "begin" options (Some [rawtext name])
    
    /// \end{<name>}
    /// _Cmd suffix as end is a keyword in F#.
    let endCmd (name:string) : GenLaTeX<'a> = 
        command "end" None (Some [rawtext name])



    let environment (options: option<GenLaTeX<'a> list>) 
                    (name:string)  
                    (body:GenLaTeX<'b>) : GenLaTeX<'x> =
        beginCmd options name ^//^ body ^//^ endCmd name





        