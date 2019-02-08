// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc.Base

/// Generate output for rendering with LaTeX.
/// Alternative implmenetations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module TikZLaTeX = 

    open TikZDoc

    type TikZPhantom = class end

    type TikZ = GenLaTeX<TikZPhantom>

    type TikZPropertyPhantom = class end

    type TikZProperty = GenLaTeX<TikZPropertyPhantom>



    /// Units are clunky because they must be the same type 
    /// (ruling out units-of-measure), but a particular diagram 
    /// is expected to use just one unit so a shim API for 
    /// the diagram could fix the unit in the function signatures.
    type Dims = 
        | PT of double
        | BP of double
        | MM of double
        | CM of double
        | IN of double
        | EX of double
        | EM of double
        member x.LaTeX 
            with get() : TikZProperty = 
                match x with 
                | PT d -> raw <| sprintf "%fpt" d
                | BP d -> raw <| sprintf "%fbp" d
                | MM d -> raw <| sprintf "%fmm" d
                | CM d -> raw <| sprintf "%fcm" d
                | IN d -> raw <| sprintf "%fin" d
                | EX d -> raw <| sprintf "%fex" d
                | EM d -> raw <| sprintf "%fem" d


    let usetikzlibrary (arguments:GenLaTeX<'a> list) : LaTeX = 
        command "usetikzlibrary" [] arguments

    let draw (options:GenLaTeX<'a> list) : TikZ = 
        command "draw" options []

    let fill (options:GenLaTeX<'a> list) : TikZ = 
        command "fill" options []

    let filldraw (options:GenLaTeX<'a> list) : TikZ = 
        command "filldraw" options []


    let roundedCorners : TikZProperty = raw "rounded corners"

    /// Parametric version of roundedCorners
    /// i.e. [rounded corners=0.5cm]    
    let roundedCornersDims (dims:Dims) : TikZProperty = 
        property "rounded corners" dims.LaTeX


    let sharpCorners : TikZProperty = raw "sharp corners"
    

    let lineWidth (width:Dims) : TikZProperty = 
        property "line width" width.LaTeX

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
        property "line cap" cap.LaTeX


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
        property "line join" join.LaTeX

    
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
        property "dash pattern" pattern

    let dashPhase (length:Dims) : TikZProperty = 
        property "dash phase" length.LaTeX

    /// [double]
    /// _Opt suffix as begin is a double is a standard F# function.
    let doubleOpt : LaTeX = raw "double"

    let doubleDistance (dist:Dims) : LaTeX = 
        property "double distance" dist.LaTeX


    // Fillings
    // \usetikzlibrary{patterns}

    let dots : LaTeX = raw "dots"

    let fivepointedStars : LaTeX = raw "fivepointed stars"

    let sixpointedStars : LaTeX = raw "sixpointed stars"

    let grid : LaTeX = raw "grid"

    let horizontalLines : LaTeX = raw "horizontal lines"

    let verticalLines : LaTeX = raw "vertical lines"

    let northEastLines : LaTeX = raw "north east lines"

    let northWestLines : LaTeX = raw "north west lines"

    let crosshatch : LaTeX = raw "crosshatch"

    let crosshatchDots : LaTeX = raw "crosshatch dots"

    let bricks : LaTeX = raw "bricks"

    let checkerboard : LaTeX = raw "checkerboard"

    let patternColor (color:LaTeX) = 
        property "pattern color" color

    // Extremeties (arrow heads)

    /// We cannot directly non-alphanumeric literals in F#
    /// so we use the false option "arrowhead"
    /// "->"; "<-"; "<->"; ">->"; "-to"; "-to reversed"; 
    /// "-o"; "-|"; "-latex"; "-latex reversed";
    /// "-stealth"; "-stealth reversed"
    let arrowhead (ascii:string) : LaTeX = raw ascii

    // arrow.meta
    // Notation arr_ prefix for "-"
    // \usetikzlibrary{arrows.meta}

    let arrArcBarb : LaTeX = raw "-Arc Barb"

    let arrBar : LaTeX = raw "-Bar"

    let arrBracket : LaTeX = raw "-Bracket"

    let arrHooks : LaTeX = raw "-Hooks"

    let arrStealth : LaTeX = raw "-Stealth"

    let arrParenthesis : LaTeX = raw "-Parenthesis"

    let arrStraightBarb : LaTeX = raw "-Straight Barb"

    let arrTeeBarb : LaTeX = raw "-TeeBarb"

    let arrClassicalTikZRightarrow : LaTeX = 
        raw "-Classical TikZ Rightarrow"

    let arrSquare : LaTeX = raw "-Square"

    let arrCircle : LaTeX = raw "-Circle"

    let arrImplies : LaTeX = raw "-Implies"

    let arrRectangle : LaTeX = raw "-Rectangle"

    let arrComputerModernRightarrow : LaTeX = 
        raw "-Computer Modern Rightarrow"

    let arrTurnedSquare : LaTeX = raw "-TurnedSquare"

    let arrDiamond : LaTeX = raw "-Diamond"

    let arrEllipsis : LaTeX = raw "-Ellipsis"

    let arrKite : LaTeX = raw "-Kite"

    let arrLatex : LaTeX = raw "-Latex"

    let arrTriangle : LaTeX = raw "-Triangle"

    
    // Notation: end_ prefix for "-"
    let endButtCap : LaTeX = raw "-Butt Cap"
    
    let endFastRound : LaTeX = raw "-Fast Round"

    let endFastTriangle : LaTeX = raw "-Fast Triangle"

    let endRoundCap : LaTeX = raw "-Round Cap"

    let endTriangleCap : LaTeX = raw "-Triangle Cap"



    let name (nodeName:string) : LaTeX = 
        property "name" (raw nodeName)
    
    let alias (aliasName:string) : LaTeX = 
        property "alias" (raw aliasName)

    let nodeContents (contents:LaTeX) : LaTeX = 
        property "node contents" contents


    // Coordinates

    type Coord = 
        val Units : option<Dims>
        val XPos : decimal
        val YPos : decimal

        new (x:decimal, y:decimal) = 
            { Units = None 
            ; XPos = x
            ; YPos = y }
        
        new (x:decimal, y:decimal, dims:Dims) = 
            { Units = Some dims
            ; XPos = x
            ; YPos = y }
            
        new (x:double, y:double) = 
            { Units = None 
            ; XPos = decimal x
            ; YPos = decimal y }
        
        new (x:double, y:double, dims:Dims) = 
            { Units = Some dims 
            ; XPos = decimal x
            ; YPos = decimal y }

        member x.LaTeX 
            with get() = 
                let sx = raw <| x.XPos.ToString()
                let sy = raw <| x.YPos.ToString()
                match x.Units with 
                | None -> parens (sx ^^ raw "," ^^ sy)
                | Some dims -> parens (sx ^^ dims.LaTeX ^^ raw "," ^^ sy ^^ dims.LaTeX)

        

    // Basic colors

    let black : TikZProperty = raw "black"
    
    let blue : TikZProperty = raw "blue"
    
    let brown : TikZProperty = raw "brown"
    
    let cyan : LaTeX = raw "cyan"
    
    let darkgray : LaTeX = raw "darkgray"
    
    let gray : LaTeX = raw "gray"
    
    let green : LaTeX = raw "green"
    
    let lightgray : LaTeX = raw "lightgray"
    
    let lime : LaTeX = raw "lime"
    
    let magenta : LaTeX = raw "magenta"
    
    let olive : LaTeX = raw "olive"
    
    let orange : LaTeX = raw "orange"
    
    let pink : LaTeX = raw "pink"
    
    let purple : LaTeX = raw "purple"
    
    let red : LaTeX = raw "red"
    
    let teal : LaTeX = raw "teal"
    
    let violet : LaTeX = raw "violet"
    
    let white : LaTeX = raw "white"
    
    let yellow : LaTeX = raw "yellow"

    // Opacity
    
    let opacity (level:double) : TikZProperty = 
        property "opacity" (raw <| sprintf "%f" level)
        
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
                
    let blendGroup (blendMode:BlendMode) : LaTeX = 
        property "blend mode" blendMode.LaTeX
                
    // Text highlighting
    
    let innerSep (dims:Dims) : LaTeX = 
        property "inner sep" dims.LaTeX
        
    let innerXsep (dims:Dims) : LaTeX = 
        property "inner xsep" dims.LaTeX

    let innerYsep (dims:Dims) : LaTeX = 
        property "inner ysep" dims.LaTeX        
    
    let outerSep (dims:Dims) : LaTeX = 
        property "outer sep" dims.LaTeX
        
    let outerXsep (dims:Dims) : LaTeX = 
        property "outer xsep" dims.LaTeX

    let outerYsep (dims:Dims) : LaTeX = 
        property "outer ysep" dims.LaTeX 

    let minimumHeight (dims:Dims) : LaTeX = 
        property "minimum height" dims.LaTeX 

    let minimumWidth (dims:Dims) : LaTeX = 
        property "minimum width" dims.LaTeX 

    let minimumSize (dims:Dims) : LaTeX = 
        property "minimum size" dims.LaTeX 
        
    // Geometric Shape nodes
    // \usetikzlibrary{shapes.geometric}
    
    let diamond : LaTeX = raw "diamond"
    
    let ellipse : LaTeX = raw "ellipse"
    
    let trapezium : LaTeX = raw "trapezium"
    
    let semicircle : LaTeX = raw "semicircle"
    
    let star : LaTeX = raw "star"
    
    let regularPolygon : LaTeX = raw "regular polygon"
    
    let isoscelesTriangle : LaTeX = raw "isosceles triangle"
    
    let kite : LaTeX = raw "kite"
    
    let dart : LaTeX = raw "dart"
    
    let circularSector : LaTeX = raw "circular sector"
    
    let cylinder : LaTeX = raw "cylinder"

    // Symbol Shape nodes
    // \usetikzlibrary{shapes.symbols}
    
    let forbiddenSign : LaTeX = raw "forbidden sign"
    
    let magnifyingGlass : LaTeX = raw "magnifying glass"
    
    let cloud : LaTeX = raw "cloud"
    
    let starburst : LaTeX = raw "starburst"
    
    let signal : LaTeX = raw "signal"
    
    let tape : LaTeX = raw "tape"

    // Arrow Shapes nodes
    // \usetikzlibrary{shapes.arrows}
    
    let singleArrow : LaTeX = raw "single arrow"
    
    let doubleArrow : LaTeX = raw "double arrow"
    
    let arrowBox : LaTeX = raw "arrow box"    
    
    // Callout Shapes nodes
    // \usetikzlibrary{shapes.callouts}
    
    let ellipseCallout : LaTeX = raw "ellipse callout"
    
    let rectangleCallout : LaTeX = raw "rectangle callout"
    
    let cloudCallout : LaTeX = raw "cloudCallout"
    
    // Miscellaneous Shapes nodes
    // \usetikzlibrary{shapes.misc}
    
    let crossOut : LaTeX = raw "cross out"
    
    let strikeOut : LaTeX = raw "strike out"
    
    let roundedRectangle : LaTeX = raw "rounded rectangle"
    
    let chamferedRectangle : LaTeX = raw "chamfered rectangle"
    
    // Shapes with Multiple Text Parts
    // \usetikzlibrary{shapes.multipart}
    
    let circleSplit : LaTeX = raw "circle split"
    
    let circleSolidus : LaTeX = raw "circle solidus"
    
    let ellipseSplit : LaTeX = raw "ellipse split"
    
    let rectangleSplit : LaTeX = raw "rectangle split"
    
    // Text attributes
    
    let textWidth (dims:Dims) : TikZProperty = 
        property "text width" dims.LaTeX
        
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
                | AlignCenter -> property "align" (raw "center")
                | AlignFlushCenter -> property "align" (raw "flush center")
                | AlignJustify -> property "align" (raw "justify")
                | AlignRight -> property "align" (raw "right")
                | AlignFlushRight -> property "align" (raw "flush right")
                | AlignLeft -> property "align" (raw "left")
                | AlignFlushLeft -> property "align" (raw "flush left")

    let textPosition (position:TextPosition) : LaTeX = 
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

    let anchor (position:Anchor) : LaTeX = 
        property "anchor" position.LaTeX


    // Other 

    let datavisualization (options:LaTeX list) : LaTeX = 
        command "datavisualization" options []
