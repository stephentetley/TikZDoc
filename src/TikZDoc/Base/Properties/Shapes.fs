// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Shapes = 

    open TikZDoc.Base

   // Geometric Shape nodes
    // \usetikzlibrary{shapes.geometric}
    
    let diamond : TikZProperty = rawtext "diamond"
    
    let ellipse : TikZProperty = rawtext "ellipse"
    
    let trapezium : TikZProperty = rawtext "trapezium"
    
    let semicircle : TikZProperty = rawtext "semicircle"
    
    let star : TikZProperty = rawtext "star"
    
    let regularPolygon : TikZProperty = rawtext "regular polygon"
    
    let isoscelesTriangle : TikZProperty = rawtext "isosceles triangle"
    
    let kite : TikZProperty = rawtext "kite"
    
    let dart : TikZProperty = rawtext "dart"
    
    let circularSector : TikZProperty = rawtext "circular sector"
    
    let cylinder : TikZProperty = rawtext "cylinder"

    // Symbol Shape nodes
    // \usetikzlibrary{shapes.symbols}
    
    let forbiddenSign : TikZProperty = rawtext "forbidden sign"
    
    let magnifyingGlass : TikZProperty = rawtext "magnifying glass"
    
    let cloud : TikZProperty = rawtext "cloud"
    
    let starburst : TikZProperty = rawtext "starburst"
    
    let signal : TikZProperty = rawtext "signal"
    
    let tape : TikZProperty = rawtext "tape"

    // Arrow Shapes nodes
    // \usetikzlibrary{shapes.arrows}
    
    let singleArrow : TikZProperty = rawtext "single arrow"
    
    let doubleArrow : TikZProperty = rawtext "double arrow"
    
    let arrowBox : TikZProperty = rawtext "arrow box"    
    
    // Callout Shapes nodes
    // \usetikzlibrary{shapes.callouts}
    
    let ellipseCallout : TikZProperty = rawtext "ellipse callout"
    
    let rectangleCallout : TikZProperty = rawtext "rectangle callout"
    
    let cloudCallout : TikZProperty = rawtext "cloudCallout"
    
    // Miscellaneous Shapes nodes
    // \usetikzlibrary{shapes.misc}
    
    let crossOut : TikZProperty = rawtext "cross out"
    
    let strikeOut : TikZProperty = rawtext "strike out"
    
    let roundedRectangle : TikZProperty = rawtext "rounded rectangle"
    
    let chamferedRectangle : TikZProperty = rawtext "chamfered rectangle"
    
    // Shapes with Multiple Text Parts
    // \usetikzlibrary{shapes.multipart}
    
    let circleSplit : TikZProperty = rawtext "circle split"
    
    let circleSolidus : TikZProperty = rawtext "circle solidus"
    
    let ellipseSplit : TikZProperty = rawtext "ellipse split"
    
    let rectangleSplit : TikZProperty = rawtext "rectangle split"


