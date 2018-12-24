// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc

/// Generate output for rendering with LaTeX.
/// Alternative implmenetations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module TikZLaTeX = 

    open TikZDoc

    let usetikzlibrary (arguments:LaTeX list) : LaTeX = 
        command "usetikzlibrary" [] arguments

    let draw (options:LaTeX list) : LaTeX = 
        command "draw" options []

    let fill (options:LaTeX list) : LaTeX = 
        command "fill" options []

    let filldraw (options:LaTeX list) : LaTeX = 
        command "filldraw" options []

    /// Design note - what to do about parametric version?
    /// i.e. [round corners=0.5cm]
    let roundCorners : LaTeX = raw "round corners"
    
    let sharpCorners : LaTeX = raw "sharp corners"
    
    /// What to do about units e.g mm cm in
    let lineWidth (inPt:double) : LaTeX = 
        property (raw "line width") (raw <| sprintf "%fpt" inPt )

    let ultraThin : LaTeX = raw "ultra thin"
    
    let veryThin : LaTeX = raw "very thin"

    let thin : LaTeX = raw "thin"

    let semithick : LaTeX = raw "semithick"

    let thick : LaTeX = raw "thick"

    let veryThick : LaTeX = raw "very thick"

    let ultraThick : LaTeX = raw "ultra thick"
    
    
    let huge : LaTeX = command "Huge" [] []



    type LineCap = 
        | CapRect | CapButt | CapRound
        member x.LaTeX 
            with get() = 
                match x with 
                | CapRect -> raw "rect"
                | CapButt -> raw "butt"
                | CapRound -> raw "round"

    let lineCap (cap:LineCap) : LaTeX = 
        property (raw "line cap") cap.LaTeX


    // Lines Junction

    type LineJoin = 
        | JoinRound | JoinBevel | JoinMiter
        member x.LaTeX 
            with get() = 
                match x with 
                | JoinRound -> raw "round"
                | JoinBevel -> raw "bevel"
                | JoinMiter -> raw "miter"

    let lineJoin (join:LineJoin) : LaTeX = 
        property (raw "line join") join.LaTeX

    
    // Line Styles

    let solid : LaTeX = raw "solid"

    let dotted : LaTeX = raw "dotted"

    let denselyDotted : LaTeX = raw "densely dotted"

    let looselyDotted : LaTeX = raw "loosely dotted"

    let dashed : LaTeX = raw "dashed"

    let denselyDashed : LaTeX = raw "densely dashed"

    let looselyDashed : LaTeX = raw "loosely dashed"

    let dashDot : LaTeX = raw "dash dot"

    let denselyDashDot : LaTeX = raw "densely dash dot"

    let looselyDashDot : LaTeX = raw "loosely dash dot"

    let dashDotDot : LaTeX = raw "dash dot dot"

    let denselyDashDotDot : LaTeX = raw "densely dash dot dot"

    let looselyDashDotDot : LaTeX = raw "loosely dash dot dot"

    let dashPattern (pattern:LaTeX) : LaTeX = 
        property (raw "dash pattern") pattern

    let dashPhase (inPt:double) : LaTeX = 
        property (raw "dash phase") (raw <| sprintf "%fpt" inPt )


    // Other 

    let datavisualization (options:LaTeX list) : LaTeX = 
        command "datavisualization" options []
