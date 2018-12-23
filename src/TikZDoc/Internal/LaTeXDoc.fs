// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Internal.LaTeXDoc


[<AutoOpen>]
module LaTeXDoc = 

    open TikZDoc.Internal
    
    // Single case union 
    [<Struct>]
    type LaTeXDocument = 
        | RawDoc of PrettyPrint.Doc

        member internal x.Body with get() = match x with | RawDoc(doc) -> doc
        
        member x.Render (lineWidth:int) : string = 
            PrettyPrint.render lineWidth x.Body


    let empty : LaTeXDocument = RawDoc(PrettyPrint.empty)

    let rawText (source:string) : LaTeXDocument = RawDoc(PrettyPrint.text source)

    let rawDoc (item:PrettyPrint.Doc) : LaTeXDocument = RawDoc(item)



    let liftPP (item:PrettyPrint.Doc) : LaTeXDocument = RawDoc(item)

    let liftOp (op:PrettyPrint.Doc -> PrettyPrint.Doc) (tex:LaTeXDocument) : LaTeXDocument = 
        rawDoc << op <| tex.Body
    
    let liftCat (op:PrettyPrint.Doc -> PrettyPrint.Doc -> PrettyPrint.Doc) (d1:LaTeXDocument) (d2:LaTeXDocument) : LaTeXDocument = 
        rawDoc <| op d1.Body d2.Body

    let liftCats (op:PrettyPrint.Doc list -> PrettyPrint.Doc) (docs:LaTeXDocument list) : LaTeXDocument  = 
        rawDoc << op <| List.map (fun (tex:LaTeXDocument) -> tex.Body) docs

    let commaSpaceSep (items:LaTeXDocument list) : LaTeXDocument = 
        items 
            |> List.map (fun (x:LaTeXDocument) -> x.Body) 
            |> PrettyPrint.punctuate (PrettyPrint.text ", ") 
            |> rawDoc
    

    /// optional params (rendered [])
    let optionsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp PrettyPrint.brackets <| commaSpaceSep items

    /// arguments (rendered {})
    let argumentsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp PrettyPrint.braces <| commaSpaceSep items