// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Arrows = 

    open TikZDoc.Base

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


