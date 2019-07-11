// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Path = 

    open TikZDoc.Base

    let roundedCorners : TikZProperty = rawtext "rounded corners"

    /// Parametric version of roundedCorners
    /// i.e. [rounded corners=0.5cm]    
    let roundedCornersDims (dims:Dims) : TikZProperty = 
        rawtext "rounded corners"  ^=^ dims.ToLaTeX ()


    let sharpCorners : TikZProperty = rawtext "sharp corners"
    

    let lineWidth (width:Dims) : TikZProperty = 
        rawtext "line width" ^=^ width.ToLaTeX ()

    let ultraThin : TikZProperty = rawtext "ultra thin"
    
    let veryThin : TikZProperty = rawtext "very thin"

    let thin : TikZProperty = rawtext "thin"

    let semithick : TikZProperty = rawtext "semithick"

    let thick : TikZProperty = rawtext "thick"

    let veryThick : TikZProperty = rawtext "very thick"

    let ultraThick : TikZProperty = rawtext "ultra thick"



    type LineCap = 
        | CapRect | CapButt | CapRound
        member x.LaTeX 
            with get() : TikZProperty = 
                match x with 
                | CapRect -> rawtext "rect"
                | CapButt -> rawtext "butt"
                | CapRound -> rawtext "round"

    let lineCap (cap:LineCap) : TikZProperty = 
        rawtext "line cap" ^=^ cap.LaTeX


    // Lines Junction

    type LineJoin = 
        | JoinRound | JoinBevel | JoinMiter
        member x.LaTeX 
            with get() : TikZProperty= 
                match x with 
                | JoinRound -> rawtext "round"
                | JoinBevel -> rawtext "bevel"
                | JoinMiter -> rawtext "miter"

    let lineJoin (join:LineJoin) : TikZProperty = 
        rawtext "line join" ^=^ join.LaTeX

    
    // Line Styles

    let solid : TikZProperty = rawtext "solid"

    let dotted : TikZProperty = rawtext "dotted"

    let denselyDotted : TikZProperty = rawtext "densely dotted"

    let looselyDotted : TikZProperty = rawtext "loosely dotted"

    let dashed : TikZProperty = rawtext "dashed"

    let denselyDashed : TikZProperty = rawtext "densely dashed"

    let looselyDashed : TikZProperty = rawtext "loosely dashed"

    let dashDot : TikZProperty = rawtext "dash dot"

    let denselyDashDot : TikZProperty = rawtext "densely dash dot"

    let looselyDashDot : TikZProperty = rawtext "loosely dash dot"

    let dashDotDot : TikZProperty = rawtext "dash dot dot"

    let denselyDashDotDot : TikZProperty = rawtext "densely dash dot dot"

    let looselyDashDotDot : TikZProperty = rawtext "loosely dash dot dot"

    let dashPattern (pattern:LaTeX) : TikZProperty = 
        rawtext "dash pattern" ^=^ pattern

    let dashPhase (length:Dims) : TikZProperty = 
        rawtext "dash phase" ^=^ length.ToLaTeX ()

    /// Line style "double"
    /// [double]
    /// _Opt suffix as begin is a double is a standard F# function.
    let doubleLineStyle : TikZProperty = rawtext "double"

    /// Line style "double distance=.3cm"
    let doubleDistance (dist:Dims) : TikZProperty = 
        rawtext "double distance" ^=^ dist.ToLaTeX ()



    