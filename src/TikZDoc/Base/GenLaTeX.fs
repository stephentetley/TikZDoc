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
    //let escapeTeX (source : string) : string = 
    //    let replace1 (src : string)  oldString newString = 
    //        src.Replace(oldValue = oldString, newValue = newString)
    //    source 
    //        |> replace1 "\\"    "$\\backslash$"
    //        |> replace1 "{"     "$\\{$"
    //        |> replace1 "}"     "$\\}$"
    //        |> replace1 "%"     "\\%"
    //        |> replace1 "&"     "\\&"
    //        |> replace1 "~"     "\\~{}"
    //        |> replace1 "$"     "\\$"
    //        |> replace1 "^"     "\\^{}"
    //        |> replace1 "_"     "\\_{}"
    //        |> replace1 "#"     "\\#"

    let escapeTeX s = s

    /// Change a list into either None if the list is empty or
    /// Some (list) if it is non-empty.
    let itemsToOption (items : 'a list) : option<'a list> = 
        match items with 
        | [] -> None
        | _ -> Some items

    /// 'General' LaTeX = not specialized to a specific type of LaTeX fragment
    type GenLaTeX<'a> = 
        internal | GenLaTeX of doc : Pretty.Doc

        member internal x.Body 
            with get() = match x with | GenLaTeX(doc) -> doc

    /// Note - the answer type can be different to input types.
    let internal liftOp (op:Pretty.Doc -> Pretty.Doc) 
                        (latex : GenLaTeX<'b>) : GenLaTeX<'x> = 
            GenLaTeX (op <| latex.Body)

    /// Note - the answer type can be diffrent to input types.
    let internal liftCat (op:Pretty.Doc -> Pretty.Doc -> Pretty.Doc) 
                (d1 : GenLaTeX<'a>) 
                (d2 : GenLaTeX<'b>) : GenLaTeX<'x> = 
        GenLaTeX (op d1.Body d2.Body)

    /// Note - the answer type can be diffrent to input types.
    let internal liftCats (op : Pretty.Doc list -> Pretty.Doc) 
                          (docs : GenLaTeX<'a> list) : GenLaTeX<'x>  = 
        let ds = List.map (fun (d1 : GenLaTeX<'a>) -> d1.Body) docs
        GenLaTeX (op ds)


    let castLaTeX (doc : GenLaTeX<'a>) : GenLaTeX<'x> = 
        let d1 = doc.Body in GenLaTeX d1
        

    let emptyLaTeX () : GenLaTeX<'a> = 
        GenLaTeX <| Pretty.empty


    /// No string escaping
    let rawtext (source : string) : GenLaTeX<'a> = 
        GenLaTeX <| Pretty.text source

    let text (source : string) : GenLaTeX<'a> = 
        GenLaTeX <| Pretty.text (escapeTeX source)

    let character (source : char) : GenLaTeX<'a> = 
        GenLaTeX <| Pretty.text (escapeTeX <| source.ToString())

    let latexInt (i : int) : GenLaTeX<'a> = 
        GenLaTeX <| Pretty.intDoc i

    let indent (i : int) (body : GenLaTeX<'a>) : GenLaTeX<'a> = 
        liftOp (Pretty.indent i) body

    let hang (i : int) (body : GenLaTeX<'a>) : GenLaTeX<'a> = 
        liftOp (Pretty.hang i) body

    let ( ^^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   = 
        liftCat Pretty.(^^) doc1 doc2

    let ( ^+^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x>   =
        liftCat Pretty.(^+^) doc1 doc2

    let ( ^!!^ ) (doc1:GenLaTeX<'a>) (doc2:GenLaTeX<'b>) : GenLaTeX<'x> = 
        liftCat Pretty.(^!!^) doc1 doc2

    let hcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        liftCats Pretty.hcat docs

    let hsep (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        liftCats Pretty.hcatSpace docs

    let vcat (docs:GenLaTeX<'a> list) : GenLaTeX<'x> = 
        liftCats Pretty.vcat docs

    let parens (body : GenLaTeX<'a>) : GenLaTeX<'x> = 
        liftOp Pretty.parens body

    let braces (body : GenLaTeX<'a>) : GenLaTeX<'x> = 
        liftOp Pretty.braces body

    let brackets (body : GenLaTeX<'a>) : GenLaTeX<'x> = 
        liftOp Pretty.brackets body


    let comment (text: string) : GenLaTeX<'a> = 
        let comment1 (s:string) = rawtext ("% " + s)
        toLines text |> List.map comment1 |> vcat


        

    /// arguments (rendered {})
    /// Note - the answer type can be diffrent to input types.
    let formatArguments (items : GenLaTeX<'a> list) : GenLaTeX<'x> = 
        liftOp Pretty.braces <| liftCats Common.commaSpaceSep items

    /// optional params (rendered [])
    /// Note - the answer type can be diffrent to input types.
    let formatOptions (items : GenLaTeX<'a> list) : GenLaTeX<'x> = 
        liftOp Pretty.brackets <| liftCats Common.commaSpaceSep items
    
    
    /// <propertyName>=<propertyValue>
    let ( ^=^ ) (propertyName : GenLaTeX<'a>) 
                (propertyValue : GenLaTeX<'a>) : GenLaTeX<'x> = 
        propertyName  ^^ character '=' ^^ propertyValue


    /// \<name>
    let commandZero (name:string) : GenLaTeX<'a> = rawtext ("\\" + name)

    /// \<name>[<options>]{<arguments>}
    let command (name : string) 
                (options : option<GenLaTeX<'a> list>) 
                (arguments : option<GenLaTeX<'b> list>) : GenLaTeX<'x> = 
        let opts = 
            match options with
            | None -> emptyLaTeX ()
            | Some xs -> formatOptions xs
        let args = 
            match arguments with
            | None -> emptyLaTeX ()
            | Some xs -> formatArguments xs
        commandZero name ^^ opts ^^ args

    /// \def\<name><body>
    /// No attempt is made to match TeX syntax for the macro body.
    let def (name:string) (body:GenLaTeX<'a>) : GenLaTeX<'x> = 
        commandZero "def" ^^ commandZero name ^^ body



    /// \begin[<options>]{<name>}
    /// _Cmd suffix as begin is a keyword in F#.
    let beginCmd (options : GenLaTeX<'a> list) (name:string) : GenLaTeX<'x> = 
        command "begin" (itemsToOption options) (Some [rawtext name])
    
    /// \end{<name>}
    /// _Cmd suffix as end is a keyword in F#.
    let endCmd (name:string) : GenLaTeX<'a> = 
        command "end" None (Some [rawtext name])



    let environment (options: GenLaTeX<'a> list) 
                    (name:string)  
                    (body:GenLaTeX<'b>) : GenLaTeX<'x> =
        beginCmd options name ^!!^ body ^!!^ endCmd name





        