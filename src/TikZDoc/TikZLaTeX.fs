// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc

/// Generate output for rendering with LaTeX.
/// Alternative implmenetations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module TikZLaTeX = 

    open TikZDoc
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
            with get() = 
                match x with 
                | PT d -> raw <| sprintf "%fpt" d
                | BP d -> raw <| sprintf "%fbp" d
                | MM d -> raw <| sprintf "%fmm" d
                | CM d -> raw <| sprintf "%fcm" d
                | IN d -> raw <| sprintf "%fin" d
                | EX d -> raw <| sprintf "%fex" d
                | EM d -> raw <| sprintf "%fem" d


    let usetikzlibrary (arguments:LaTeX list) : LaTeX = 
        command "usetikzlibrary" [] arguments

    let draw (options:LaTeX list) : LaTeX = 
        command "draw" options []

    let fill (options:LaTeX list) : LaTeX = 
        command "fill" options []

    let filldraw (options:LaTeX list) : LaTeX = 
        command "filldraw" options []


    let roundedCorners : LaTeX = raw "rounded corners"

    /// Parametric version of roundedCorners
    /// i.e. [rounded corners=0.5cm]    
    let roundedCornersDims (dims:Dims) : LaTeX = 
        property "rounded corners" dims.LaTeX


    let sharpCorners : LaTeX = raw "sharp corners"
    

    let lineWidth (width:Dims) : LaTeX = 
        property "line width" width.LaTeX

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
        property "line cap" cap.LaTeX


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
        property "line join" join.LaTeX

    
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
        property "dash pattern" pattern

    let dashPhase (length:Dims) : LaTeX = 
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
    let arrowhead (ascii:string) = raw ascii

    // arrow.meta
    // Notation arr_ prefix for "-"
    // \usetikzlibrary{arrows.meta}

    let arrArcBarb = raw "-Arc Barb"

    let arrBar = raw "-Bar"

    let arrBracket = raw "-Bracket"

    let arrHooks = raw "-Hooks"

    let arrStealth = raw "-Stealth"

    let arrParenthesis = raw "-Parenthesis"

    let arrStraightBarb = raw "-Straight Barb"

    let arrTeeBarb = raw "-TeeBarb"

    let arrClassicalTikZRightarrow = raw "-Classical TikZ Rightarrow"

    let arrSquare = raw "-Square"

    let arrCircle = raw "-Circle"

    let arrImplies = raw "-Implies"

    let arrRectangle = raw "-Rectangle"

    let arrComputerModernRightarrow = raw "-Computer Modern Rightarrow"

    let arrTurnedSquare = raw "-TurnedSquare"

    let arrDiamond = raw "-Diamond"

    let arrEllipsis = raw "-Ellipsis"

    let arrKite = raw "-Kite"

    let arrLatex = raw "-Latex"

    let arrTriangle = raw "-Triangle"

    
    // Notation: end_ prefix for "-"
    let endButtCap = "-Butt Cap"
    
    let endFastRound = "-Fast Round"

    let endFastTriangle = "-Fast Triangle"

    let endRoundCap = "-Round Cap"

    let endTriangleCap = "-Triangle Cap"
    // Other 

    let datavisualization (options:LaTeX list) : LaTeX = 
        command "datavisualization" options []
