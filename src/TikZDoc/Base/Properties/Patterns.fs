// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Patterns = 

    open TikZDoc.Base

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



