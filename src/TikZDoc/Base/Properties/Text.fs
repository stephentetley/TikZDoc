// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Text = 

    open TikZDoc.Base

                
    // Text highlighting
    
    let innerSep (dims:Dims) : TikZProperty = 
        keyvalue "inner sep" dims.LaTeX
        
    let innerXsep (dims:Dims) : TikZProperty = 
        keyvalue "inner xsep" dims.LaTeX

    let innerYsep (dims:Dims) : TikZProperty = 
        keyvalue "inner ysep" dims.LaTeX        
    
    let outerSep (dims:Dims) : TikZProperty = 
        keyvalue "outer sep" dims.LaTeX
        
    let outerXsep (dims:Dims) : TikZProperty = 
        keyvalue "outer xsep" dims.LaTeX

    let outerYsep (dims:Dims) : TikZProperty = 
        keyvalue "outer ysep" dims.LaTeX 

    let minimumHeight (dims:Dims) : TikZProperty = 
        keyvalue "minimum height" dims.LaTeX 

    let minimumWidth (dims:Dims) : TikZProperty = 
        keyvalue "minimum width" dims.LaTeX 

    let minimumSize (dims:Dims) : TikZProperty = 
        keyvalue "minimum size" dims.LaTeX 
        
     
    // Text attributes
    
    let textWidth (dims:Dims) : TikZProperty = 
        keyvalue "text width" dims.LaTeX
        
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
                | TextJustified -> raw "text justified"
                | TextCentered -> raw "text centered"
                | TextRagged -> raw "text ragged"
                | TextBadlyRagged -> raw "text badly ragged"
                | TextBadlyCentered -> raw "text badly centered"
                | AlignCenter -> keyvalue "align" (raw "center")
                | AlignFlushCenter -> keyvalue "align" (raw "flush center")
                | AlignJustify -> keyvalue "align" (raw "justify")
                | AlignRight -> keyvalue "align" (raw "right")
                | AlignFlushRight -> keyvalue "align" (raw "flush right")
                | AlignLeft -> keyvalue "align" (raw "left")
                | AlignFlushLeft -> keyvalue "align" (raw "flush left")

    let textPosition (position:TextPosition) : TikZProperty = 
        position.LaTeX