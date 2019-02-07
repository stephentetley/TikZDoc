// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Internal


module LaTeXDoc = 

    open SLFormat
    
    // Single case union 
    [<Struct>]
    type LaTeXDocument = 
        | RawDoc of Pretty.Doc

        member internal x.Body with get() = match x with | RawDoc(doc) -> doc
        
        member x.Render (lineWidth:int) : string = 
            Pretty.render lineWidth x.Body

        member x.SaveAsTex(lineWidth:int, fileName:string) : unit = 
            Pretty.writeDoc lineWidth fileName x.Body


    let empty : LaTeXDocument = RawDoc(Pretty.empty)

    let rawText (source:string) : LaTeXDocument = RawDoc(Pretty.text source)

    let rawDoc (item:Pretty.Doc) : LaTeXDocument = RawDoc(item)



    let liftPP (item:Pretty.Doc) : LaTeXDocument = RawDoc(item)

    let liftOp (op:Pretty.Doc -> Pretty.Doc) (tex:LaTeXDocument) : LaTeXDocument = 
        rawDoc << op <| tex.Body
    
    let liftCat (op:Pretty.Doc -> Pretty.Doc -> Pretty.Doc) (d1:LaTeXDocument) (d2:LaTeXDocument) : LaTeXDocument = 
        rawDoc <| op d1.Body d2.Body

    let liftCats (op:Pretty.Doc list -> Pretty.Doc) (docs:LaTeXDocument list) : LaTeXDocument  = 
        rawDoc << op <| List.map (fun (tex:LaTeXDocument) -> tex.Body) docs

    let commaSpaceSep (items:LaTeXDocument list) : LaTeXDocument = 
        items 
            |> List.map (fun (x:LaTeXDocument) -> x.Body) 
            |> Pretty.punctuate (Pretty.text ", ") 
            |> rawDoc
    

    /// optional params (rendered [])
    let optionsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp Pretty.brackets <| commaSpaceSep items

    /// arguments (rendered {})
    let argumentsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp Pretty.braces <| commaSpaceSep items