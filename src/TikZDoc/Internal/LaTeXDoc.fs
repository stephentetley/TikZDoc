// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Internal.LaTeXDoc


[<AutoOpen>]
module LaTeXDoc = 

    open SLPretty
    open TikZDoc.Internal
    
    



    // Single case union 
    [<Struct>]
    type LaTeXDocument = 
        | RawDoc of SLPretty.Doc

        member internal x.Body with get() = match x with | RawDoc(doc) -> doc
        
        member x.Render (lineWidth:int) : string = 
            SLPretty.render lineWidth x.Body

        member x.SaveAsTex(lineWidth:int, fileName:string) : unit = 
            SLPretty.writeDoc lineWidth fileName x.Body


    let empty : LaTeXDocument = RawDoc(SLPretty.empty)

    let rawText (source:string) : LaTeXDocument = RawDoc(SLPretty.text source)

    let rawDoc (item:SLPretty.Doc) : LaTeXDocument = RawDoc(item)



    let liftPP (item:SLPretty.Doc) : LaTeXDocument = RawDoc(item)

    let liftOp (op:SLPretty.Doc -> SLPretty.Doc) (tex:LaTeXDocument) : LaTeXDocument = 
        rawDoc << op <| tex.Body
    
    let liftCat (op:SLPretty.Doc -> SLPretty.Doc -> SLPretty.Doc) (d1:LaTeXDocument) (d2:LaTeXDocument) : LaTeXDocument = 
        rawDoc <| op d1.Body d2.Body

    let liftCats (op:SLPretty.Doc list -> SLPretty.Doc) (docs:LaTeXDocument list) : LaTeXDocument  = 
        rawDoc << op <| List.map (fun (tex:LaTeXDocument) -> tex.Body) docs

    let commaSpaceSep (items:LaTeXDocument list) : LaTeXDocument = 
        items 
            |> List.map (fun (x:LaTeXDocument) -> x.Body) 
            |> SLPretty.punctuate (SLPretty.text ", ") 
            |> rawDoc
    

    /// optional params (rendered [])
    let optionsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp SLPretty.brackets <| commaSpaceSep items

    /// arguments (rendered {})
    let argumentsList (items:LaTeXDocument list) : LaTeXDocument  =
        liftOp SLPretty.braces <| commaSpaceSep items