// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Internal


module Syntax = 
    
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
    type WrappedDoc<'a> = 
        | WrappedDoc of Pretty.Doc

        member internal x.Body 
            with get() = match x with | WrappedDoc(doc) -> doc
        
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


    let cast (doc : WrappedDoc<'a>) : WrappedDoc<'x> = 
        let d1 = doc.Body in WrappedDoc(d1)




    /// Lift a Pretty.Doc to a WrappedDoc.
    let liftDoc (item : Pretty.Doc) : WrappedDoc<'a> = WrappedDoc(item)

    /// Note - the answer type can be diffrent to input types.
    let liftOp (op:Pretty.Doc -> Pretty.Doc) (tex : WrappedDoc<'b>) : WrappedDoc<'x> = 
        liftDoc (op <| tex.Body)
    
    /// Note - the answer type can be diffrent to input types.
    let liftCat (op:Pretty.Doc -> Pretty.Doc -> Pretty.Doc) 
                (d1 : WrappedDoc<'a>) 
                (d2 : WrappedDoc<'b>) : WrappedDoc<'x> = 
        liftDoc (op d1.Body d2.Body)

    /// Note - the answer type can be diffrent to input types.
    let liftCats (op : Pretty.Doc list -> Pretty.Doc) 
                 (docs : WrappedDoc<'a> list) : WrappedDoc<'x>  = 
        let ds = List.map (fun (d1 : WrappedDoc<'a>) -> d1.Body) docs
        liftDoc (op ds)
            
    /// Note - the answer type can be different to input types.
    let commaSpaceSep (items : WrappedDoc<'a> list) : WrappedDoc<'x> = 
        let ds = List.map (fun (d1 : WrappedDoc<'a>) -> d1.Body) items 
        liftDoc (Pretty.punctuate (Pretty.text ", ")  ds)

    


