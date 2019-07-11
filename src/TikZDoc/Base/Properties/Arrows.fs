// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Arrows = 

    open TikZDoc.Base


    // Extremeties (arrow heads)

    /// We cannot directly non-alphanumeric literals in F#
    /// so we use the false option "arrowhead"
    /// "->"; "<-"; "<->"; ">->"; "-to"; "-to reversed"; 
    /// "-o"; "-|"; "-latex"; "-latex reversed";
    /// "-stealth"; "-stealth reversed"
    let arrowhead (ascii:string) : TikZProperty = rawtext ascii

    // arrow.meta
    // Notation arr_ prefix for "-"
    // \usetikzlibrary{arrows.meta}

    let arcBarb : TikZProperty = rawtext "-Arc Barb"

    let bar : TikZProperty = rawtext "-Bar"

    let bracket : TikZProperty = rawtext "-Bracket"

    let hooks : TikZProperty = rawtext "-Hooks"

    let stealth : TikZProperty = rawtext "-Stealth"

    let parenthesis : TikZProperty = rawtext "-Parenthesis"

    let straightBarb : TikZProperty = rawtext "-Straight Barb"

    let teeBarb : TikZProperty = rawtext "-TeeBarb"

    let classicalTikZRightarrow : TikZProperty = 
        rawtext "-Classical TikZ Rightarrow"

    let square : TikZProperty = rawtext "-Square"

    let circle : TikZProperty = rawtext "-Circle"

    let implies : TikZProperty = rawtext "-Implies"

    let rectangle : LaTeX = rawtext "-Rectangle"

    let computerModernRightarrow : TikZProperty = 
        rawtext "-Computer Modern Rightarrow"

    let turnedSquare : TikZProperty = rawtext "-TurnedSquare"

    let diamond : TikZProperty = rawtext "-Diamond"

    let ellipsis : TikZProperty = rawtext "-Ellipsis"

    let kite : TikZProperty = rawtext "-Kite"

    let latex : TikZProperty = rawtext "-Latex"

    let triangle : TikZProperty = rawtext "-Triangle"

    

    let buttCap : TikZProperty = rawtext "-Butt Cap"
    
    let fastRound : TikZProperty = rawtext "-Fast Round"

    let fastTriangle : TikZProperty = rawtext "-Fast Triangle"

    let roundCap : TikZProperty = rawtext "-Round Cap"

    let triangleCap : TikZProperty = rawtext "-Triangle Cap"
