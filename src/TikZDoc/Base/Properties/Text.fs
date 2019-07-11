// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Text = 

    open TikZDoc.Base

                
    // Text highlighting
    
    let innerSep (dims:Dims) : TikZProperty = 
        rawtext "inner sep" ^=^ dims.ToLaTeX ()
        
    let innerXsep (dims:Dims) : TikZProperty = 
        rawtext "inner xsep" ^=^ dims.ToLaTeX ()

    let innerYsep (dims:Dims) : TikZProperty = 
        rawtext "inner ysep" ^=^ dims.ToLaTeX ()        
    
    let outerSep (dims:Dims) : TikZProperty = 
        rawtext "outer sep" ^=^ dims.ToLaTeX ()
        
    let outerXsep (dims:Dims) : TikZProperty = 
        rawtext "outer xsep" ^=^ dims.ToLaTeX ()

    let outerYsep (dims:Dims) : TikZProperty = 
        rawtext "outer ysep" ^=^ dims.ToLaTeX () 

    let minimumHeight (dims:Dims) : TikZProperty = 
        rawtext "minimum height" ^=^ dims.ToLaTeX () 

    let minimumWidth (dims:Dims) : TikZProperty = 
        rawtext "minimum width" ^=^ dims.ToLaTeX () 

    let minimumSize (dims:Dims) : TikZProperty = 
        rawtext "minimum size" ^=^ dims.ToLaTeX () 
        
     
    // Text attributes
    
    let textWidth (dims:Dims) : TikZProperty = 
        rawtext "text width" ^=^ dims.ToLaTeX ()
        
    type TextPosition =
        | TextJustified
        | TextCentered
        | TextRagged
        | TextBadlyRagged
        | TextBadlyCentered
        | AlignCenter
        | AlignFlushCenter
        | AlignJustify
        | AlignRight
        | AlignFlushRight
        | AlignLeft
        | AlignFlushLeft
        member x.LaTeX 
            with get() = 
                match x with 
                | TextJustified -> rawtext "text justified"
                | TextCentered -> rawtext "text centered"
                | TextRagged -> rawtext "text ragged"
                | TextBadlyRagged -> rawtext "text badly ragged"
                | TextBadlyCentered -> rawtext "text badly centered"
                | AlignCenter -> rawtext "align" ^=^ (rawtext "center")
                | AlignFlushCenter -> rawtext "align" ^=^ (rawtext "flush center")
                | AlignJustify -> rawtext "align" ^=^ (rawtext "justify")
                | AlignRight -> rawtext "align" ^=^ (rawtext "right")
                | AlignFlushRight -> rawtext "align" ^=^ (rawtext "flush right")
                | AlignLeft -> rawtext "align" ^=^ (rawtext "left")
                | AlignFlushLeft -> rawtext "align" ^=^ (rawtext "flush left")

    let textPosition (position:TextPosition) : TikZProperty = 
        position.LaTeX