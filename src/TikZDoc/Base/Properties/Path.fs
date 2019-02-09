// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Path = 

    open TikZDoc.Base

    let roundedCorners : TikZProperty = raw "rounded corners"

    /// Parametric version of roundedCorners
    /// i.e. [rounded corners=0.5cm]    
    let roundedCornersDims (dims:Dims) : TikZProperty = 
        keyvalue "rounded corners" dims.LaTeX


    let sharpCorners : TikZProperty = raw "sharp corners"
    

    let lineWidth (width:Dims) : TikZProperty = 
        keyvalue "line width" width.LaTeX

    let ultraThin : TikZProperty = raw "ultra thin"
    
    let veryThin : TikZProperty = raw "very thin"

    let thin : TikZProperty = raw "thin"

    let semithick : TikZProperty = raw "semithick"

    let thick : TikZProperty = raw "thick"

    let veryThick : TikZProperty = raw "very thick"

    let ultraThick : TikZProperty = raw "ultra thick"



    type LineCap = 
        | CapRect | CapButt | CapRound
        member x.LaTeX 
            with get() : TikZProperty = 
                match x with 
                | CapRect -> raw "rect"
                | CapButt -> raw "butt"
                | CapRound -> raw "round"

    let lineCap (cap:LineCap) : TikZProperty = 
        keyvalue "line cap" cap.LaTeX


    // Lines Junction

    type LineJoin = 
        | JoinRound | JoinBevel | JoinMiter
        member x.LaTeX 
            with get() : TikZProperty= 
                match x with 
                | JoinRound -> raw "round"
                | JoinBevel -> raw "bevel"
                | JoinMiter -> raw "miter"

    let lineJoin (join:LineJoin) : TikZProperty = 
        keyvalue "line join" join.LaTeX

    
    // Line Styles

    let solid : TikZProperty = raw "solid"

    let dotted : TikZProperty = raw "dotted"

    let denselyDotted : TikZProperty = raw "densely dotted"

    let looselyDotted : TikZProperty = raw "loosely dotted"

    let dashed : TikZProperty = raw "dashed"

    let denselyDashed : TikZProperty = raw "densely dashed"

    let looselyDashed : TikZProperty = raw "loosely dashed"

    let dashDot : TikZProperty = raw "dash dot"

    let denselyDashDot : TikZProperty = raw "densely dash dot"

    let looselyDashDot : TikZProperty = raw "loosely dash dot"

    let dashDotDot : TikZProperty = raw "dash dot dot"

    let denselyDashDotDot : TikZProperty = raw "densely dash dot dot"

    let looselyDashDotDot : TikZProperty = raw "loosely dash dot dot"

    let dashPattern (pattern:LaTeX) : TikZProperty = 
        keyvalue "dash pattern" pattern

    let dashPhase (length:Dims) : TikZProperty = 
        keyvalue "dash phase" length.LaTeX

    /// Line style "double"
    /// [double]
    /// _Opt suffix as begin is a double is a standard F# function.
    let doubleLineStyle : TikZProperty = raw "double"

    /// Line style "double distance=.3cm"
    let doubleDistance (dist:Dims) : TikZProperty = 
        keyvalue "double distance" dist.LaTeX



    