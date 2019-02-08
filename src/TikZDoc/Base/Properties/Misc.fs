// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// TODO - more code can moved to separate modules.


[<AutoOpen>]
module Misc = 

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
    
    
    let huge : TikZProperty = command "Huge" [] []



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


 


    
    // Notation: end_ prefix for "-"
    let endButtCap : TikZProperty = raw "-Butt Cap"
    
    let endFastRound : TikZProperty = raw "-Fast Round"

    let endFastTriangle : TikZProperty = raw "-Fast Triangle"

    let endRoundCap : TikZProperty = raw "-Round Cap"

    let endTriangleCap : TikZProperty = raw "-Triangle Cap"



    let name (nodeName:string) : TikZProperty = 
        keyvalue "name" (raw nodeName)
    
    let alias (aliasName:string) : TikZProperty = 
        keyvalue "alias" (raw aliasName)

    let nodeContents (contents:LaTeX) : TikZProperty = 
        keyvalue "node contents" contents



        

    // Basic colors

    let black : TikZProperty = raw "black"
    
    let blue : TikZProperty = raw "blue"
    
    let brown : TikZProperty = raw "brown"
    
    let cyan : TikZProperty = raw "cyan"
    
    let darkgray : TikZProperty = raw "darkgray"
    
    let gray : TikZProperty = raw "gray"
    
    let green : TikZProperty = raw "green"
    
    let lightgray : TikZProperty = raw "lightgray"
    
    let lime : TikZProperty = raw "lime"
    
    let magenta : TikZProperty = raw "magenta"
    
    let olive : TikZProperty = raw "olive"
    
    let orange : TikZProperty = raw "orange"
    
    let pink : TikZProperty = raw "pink"
    
    let purple : TikZProperty = raw "purple"
    
    let red : TikZProperty = raw "red"
    
    let teal : TikZProperty = raw "teal"
    
    let violet : TikZProperty = raw "violet"
    
    let white : TikZProperty = raw "white"
    
    let yellow : TikZProperty = raw "yellow"

    // Opacity
    
    let opacity (level:double) : TikZProperty = 
        keyvalue "opacity" (raw <| sprintf "%f" level)
        
    let transparent : TikZProperty = raw "transparent"
    
    let ultraNearlyTransparent : TikZProperty = raw "ultra nearly transparent"
    
    let veryNearlyTransparent : TikZProperty = raw "very nearly transparent"
    
    let nearlyTransparent : TikZProperty = raw "nearly transparent"
    
    let semitransparent : TikZProperty = raw "semitransparent"
    
    let nearlyOpaque : TikZProperty = raw "nearly opaque"
    
    let veryNearlyOpaque : TikZProperty = raw "very nearly opaque" 
    
    let ultraNearlyOpaque : TikZProperty = raw "ultra nearly opaque"
    
    let opaque : TikZProperty = raw "opaque"
    
    // Blend Mode
    
    type BlendMode =
        | BlendNormal
        | BlendMultiply
        | BlendScreen
        | BlendOverlay
        | BlendDarken
        | BlendLighten
        | BlendDifference
        | BlendExclusion
        | BlendHue
        | BlendSaturation
        | BlendColor
        | BlendLuminosity
        member x.LaTeX 
            with get() = 
                match x with 
                | BlendNormal -> raw "normal"
                | BlendMultiply -> raw "multiply"
                | BlendScreen -> raw "screen"
                | BlendOverlay -> raw "overlay"
                | BlendDarken -> raw "darken"
                | BlendLighten -> raw "lighten"
                | BlendDifference -> raw "difference"
                | BlendExclusion -> raw "exclusion"
                | BlendHue -> raw "hue"
                | BlendSaturation -> raw "saturation"
                | BlendColor -> raw "color"
                | BlendLuminosity -> raw "luminosity"
                
    let blendGroup (blendMode:BlendMode) : TikZProperty = 
        keyvalue "blend mode" blendMode.LaTeX
                
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
    
    // Positions on a node

    type Anchor = 
        | AnchorNorthWest
        | AnchorNorth
        | AnchorNorthEast
        | AnchorText
        | AnchorWest
        | AnchorMidWest
        | AnchorBaseWest
        | AnchorBase
        | AnchorEast
        | AnchorMidEast
        | AnchorBaseEast
        | AnchorMid
        | AnchorSouthEast
        | AnchorSouth
        | AnchorSouthWest
        | AnchorCenter
        | AnchorDegree of int
        member x.LaTeX 
            with get() = 
                match x with 
                | AnchorNorthWest -> raw "north west"
                | AnchorNorth -> raw "north"
                | AnchorNorthEast -> raw "north east"
                | AnchorText -> raw "text"
                | AnchorWest -> raw "west"
                | AnchorMidWest -> raw "mid west"
                | AnchorBaseWest -> raw "base west"
                | AnchorBase -> raw "base"
                | AnchorEast -> raw "east"
                | AnchorMidEast -> raw "mid east"
                | AnchorBaseEast -> raw "base east"
                | AnchorMid -> raw "mid"
                | AnchorSouthEast -> raw "south east"
                | AnchorSouth -> raw "south"
                | AnchorSouthWest -> raw "south west"
                | AnchorCenter -> raw "center"
                | AnchorDegree(x) -> raw <| x.ToString()

    let anchor (position:Anchor) : TikZProperty = 
        keyvalue "anchor" position.LaTeX



