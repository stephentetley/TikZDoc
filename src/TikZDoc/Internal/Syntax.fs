// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Internal


module LaTeXDocument = 
    
    open System.IO

    open SLFormat
    open TikZDoc.Internal
    
    type OutputFormat = 
        | PostScript 
        | PDF 
        | SVG
        
        member x.DocumentProlog 
            with get() : Pretty.Doc = 
                match x with 
                | PostScript -> Pretty.text "\\documentclass{article}"
                | PDF -> Pretty.vcat [ Pretty.text "\documentclass{article}" 
                                     ; Pretty.text "\\def\\pgfsysdriver{pgfsys-dvipdfm.def}" ]
                | SVG -> Pretty.text "\\documentclass[dvisvgm]{minimal}"


    // Single case union with a phantom parameter
    [<Struct>]
    type LaTeXDoc<'a> = 
        | LaTeXDoc of Pretty.Doc

        member internal x.Body 
            with get() = match x with | LaTeXDoc(doc) -> doc
        
        member x.Render (lineWidth:int) : string = 
            Pretty.render lineWidth x.Body

        member x.SaveAsTex(lineWidth:int, fileName:string) : unit = 
            Pretty.writeDoc lineWidth fileName x.Body

        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToPS(outputDirectory:string, fileName:string) : unit = 
            let doc = Pretty.vcat [PostScript.DocumentProlog; x.Body]
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            Pretty.writeDoc 80 fileName doc
            Invoke.runLatex outputDirectory fileName
            Invoke.runDvips outputDirectory fileName

        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToPDF(outputDirectory:string, fileName:string) : unit = 
            let doc = Pretty.vcat [PDF.DocumentProlog; x.Body]
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            Pretty.writeDoc 80 fileName doc
            Invoke.runLatex outputDirectory fileName
            Invoke.runDvipdfm outputDirectory fileName

        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToSVG(outputDirectory:string, fileName:string) : unit = 
            let doc = Pretty.vcat [SVG.DocumentProlog; x.Body]
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            Pretty.writeDoc 80 fileName doc
            Invoke.runLatex outputDirectory fileName
            Invoke.runDvisvgm outputDirectory fileName


    let cast (doc:LaTeXDoc<'a>) : LaTeXDoc<'x> = 
        let d1 = doc.Body in LaTeXDoc(d1)

    let empty : LaTeXDoc<'a> = LaTeXDoc(Pretty.empty)

    let rawText (source:string) : LaTeXDoc<'a> = LaTeXDoc(Pretty.text source)


    /// Lift a Pretty.Doc to a LaTeXDoc.
    let liftDoc (item:Pretty.Doc) : LaTeXDoc<'a> = LaTeXDoc(item)

    /// Note - the answer type can be diffrent to input types.
    let liftOp (op:Pretty.Doc -> Pretty.Doc) (tex:LaTeXDoc<'b>) : LaTeXDoc<'x> = 
        liftDoc (op <| tex.Body)
    
    /// Note - the answer type can be diffrent to input types.
    let liftCat (op:Pretty.Doc -> Pretty.Doc -> Pretty.Doc) 
                (d1:LaTeXDoc<'a>) 
                (d2:LaTeXDoc<'b>) : LaTeXDoc<'x> = 
        liftDoc (op d1.Body d2.Body)

    /// Note - the answer type can be diffrent to input types.
    let liftCats (op:Pretty.Doc list -> Pretty.Doc) 
                 (docs:LaTeXDoc<'a> list) : LaTeXDoc<'x>  = 
        let ds = List.map (fun (tex:LaTeXDoc<'a>) -> tex.Body) docs in liftDoc (op ds)
            
    /// Note - the answer type can be diffrent to input types.
    let commaSpaceSep (items:LaTeXDoc<'a> list) : LaTeXDoc<'x> = 
        let ds = List.map (fun (x:LaTeXDoc<'a>) -> x.Body) items 
        liftDoc (Pretty.punctuate (Pretty.text ", ")  ds)

    

    /// optional params (rendered [])
    /// Note - the answer type can be diffrent to input types.
    let optionsList (items:LaTeXDoc<'a> list) : LaTeXDoc<'x>  =
        liftOp Pretty.brackets <| commaSpaceSep items

    /// arguments (rendered {})
    /// Note - the answer type can be diffrent to input types.
    let argumentsList (items:LaTeXDoc<'a> list) : LaTeXDoc<'x>  =
        liftOp Pretty.braces <| commaSpaceSep items

