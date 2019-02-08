// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base

/// Generate output for rendering with LaTeX.
/// Alternative implmenetations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module Properties = 

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


    // Fillings
    // \usetikzlibrary{patterns}

    let dots : TikZProperty = raw "dots"

    let fivepointedStars : TikZProperty = raw "fivepointed stars"

    let sixpointedStars : TikZProperty = raw "sixpointed stars"

    let grid : TikZProperty = raw "grid"

    let horizontalLines : TikZProperty = raw "horizontal lines"

    let verticalLines : TikZProperty = raw "vertical lines"

    let northEastLines : TikZProperty = raw "north east lines"

    let northWestLines : TikZProperty = raw "north west lines"

    let crosshatch : TikZProperty = raw "crosshatch"

    let crosshatchDots : TikZProperty = raw "crosshatch dots"

    let bricks : TikZProperty = raw "bricks"

    let checkerboard : TikZProperty = raw "checkerboard"

    let patternColor (color:TikZProperty) : TikZProperty = 
        keyvalue "pattern color" color

    // Extremeties (arrow heads)

    /// We cannot directly non-alphanumeric literals in F#
    /// so we use the false option "arrowhead"
    /// "->"; "<-"; "<->"; ">->"; "-to"; "-to reversed"; 
    /// "-o"; "-|"; "-latex"; "-latex reversed";
    /// "-stealth"; "-stealth reversed"
    let arrowhead (ascii:string) : TikZProperty = raw ascii

    // arrow.meta
    // Notation arr_ prefix for "-"
    // \usetikzlibrary{arrows.meta}

    let arrArcBarb : TikZProperty = raw "-Arc Barb"

    let arrBar : TikZProperty = raw "-Bar"

    let arrBracket : TikZProperty = raw "-Bracket"

    let arrHooks : TikZProperty = raw "-Hooks"

    let arrStealth : TikZProperty = raw "-Stealth"

    let arrParenthesis : TikZProperty = raw "-Parenthesis"

    let arrStraightBarb : TikZProperty = raw "-Straight Barb"

    let arrTeeBarb : TikZProperty = raw "-TeeBarb"

    let arrClassicalTikZRightarrow : TikZProperty = 
        raw "-Classical TikZ Rightarrow"

    let arrSquare : TikZProperty = raw "-Square"

    let arrCircle : TikZProperty = raw "-Circle"

    let arrImplies : TikZProperty = raw "-Implies"

    let arrRectangle : LaTeX = raw "-Rectangle"

    let arrComputerModernRightarrow : TikZProperty = 
        raw "-Computer Modern Rightarrow"

    let arrTurnedSquare : TikZProperty = raw "-TurnedSquare"

    let arrDiamond : TikZProperty = raw "-Diamond"

    let arrEllipsis : TikZProperty = raw "-Ellipsis"

    let arrKite : TikZProperty = raw "-Kite"

    let arrLatex : TikZProperty = raw "-Latex"

    let arrTriangle : TikZProperty = raw "-Triangle"

    
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
        
    // Geometric Shape nodes
    // \usetikzlibrary{shapes.geometric}
    
    let diamond : TikZProperty = raw "diamond"
    
    let ellipse : TikZProperty = raw "ellipse"
    
    let trapezium : TikZProperty = raw "trapezium"
    
    let semicircle : TikZProperty = raw "semicircle"
    
    let star : TikZProperty = raw "star"
    
    let regularPolygon : TikZProperty = raw "regular polygon"
    
    let isoscelesTriangle : TikZProperty = raw "isosceles triangle"
    
    let kite : TikZProperty = raw "kite"
    
    let dart : TikZProperty = raw "dart"
    
    let circularSector : TikZProperty = raw "circular sector"
    
    let cylinder : TikZProperty = raw "cylinder"

    // Symbol Shape nodes
    // \usetikzlibrary{shapes.symbols}
    
    let forbiddenSign : TikZProperty = raw "forbidden sign"
    
    let magnifyingGlass : TikZProperty = raw "magnifying glass"
    
    let cloud : TikZProperty = raw "cloud"
    
    let starburst : TikZProperty = raw "starburst"
    
    let signal : TikZProperty = raw "signal"
    
    let tape : TikZProperty = raw "tape"

    // Arrow Shapes nodes
    // \usetikzlibrary{shapes.arrows}
    
    let singleArrow : TikZProperty = raw "single arrow"
    
    let doubleArrow : TikZProperty = raw "double arrow"
    
    let arrowBox : TikZProperty = raw "arrow box"    
    
    // Callout Shapes nodes
    // \usetikzlibrary{shapes.callouts}
    
    let ellipseCallout : TikZProperty = raw "ellipse callout"
    
    let rectangleCallout : TikZProperty = raw "rectangle callout"
    
    let cloudCallout : TikZProperty = raw "cloudCallout"
    
    // Miscellaneous Shapes nodes
    // \usetikzlibrary{shapes.misc}
    
    let crossOut : TikZProperty = raw "cross out"
    
    let strikeOut : TikZProperty = raw "strike out"
    
    let roundedRectangle : TikZProperty = raw "rounded rectangle"
    
    let chamferedRectangle : TikZProperty = raw "chamfered rectangle"
    
    // Shapes with Multiple Text Parts
    // \usetikzlibrary{shapes.multipart}
    
    let circleSplit : TikZProperty = raw "circle split"
    
    let circleSolidus : TikZProperty = raw "circle solidus"
    
    let ellipseSplit : TikZProperty = raw "ellipse split"
    
    let rectangleSplit : TikZProperty = raw "rectangle split"
    
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



