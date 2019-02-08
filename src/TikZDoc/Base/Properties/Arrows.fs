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
    let arrowhead (ascii:string) : TikZProperty = raw ascii

    // arrow.meta
    // Notation arr_ prefix for "-"
    // \usetikzlibrary{arrows.meta}

    let arcBarb : TikZProperty = raw "-Arc Barb"

    let bar : TikZProperty = raw "-Bar"

    let bracket : TikZProperty = raw "-Bracket"

    let hooks : TikZProperty = raw "-Hooks"

    let stealth : TikZProperty = raw "-Stealth"

    let parenthesis : TikZProperty = raw "-Parenthesis"

    let straightBarb : TikZProperty = raw "-Straight Barb"

    let teeBarb : TikZProperty = raw "-TeeBarb"

    let classicalTikZRightarrow : TikZProperty = 
        raw "-Classical TikZ Rightarrow"

    let square : TikZProperty = raw "-Square"

    let circle : TikZProperty = raw "-Circle"

    let implies : TikZProperty = raw "-Implies"

    let rectangle : LaTeX = raw "-Rectangle"

    let computerModernRightarrow : TikZProperty = 
        raw "-Computer Modern Rightarrow"

    let turnedSquare : TikZProperty = raw "-TurnedSquare"

    let diamond : TikZProperty = raw "-Diamond"

    let ellipsis : TikZProperty = raw "-Ellipsis"

    let kite : TikZProperty = raw "-Kite"

    let latex : TikZProperty = raw "-Latex"

    let triangle : TikZProperty = raw "-Triangle"