// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen

module Patterns = 

    open TikZDoc.Base

   // Fillings
    // \usetikzlibrary{patterns}

    let dots : TikZProperty = rawtext "dots"

    let fivepointedStars : TikZProperty = rawtext "fivepointed stars"

    let sixpointedStars : TikZProperty = rawtext "sixpointed stars"

    let grid : TikZProperty = rawtext "grid"

    let horizontalLines : TikZProperty = rawtext "horizontal lines"

    let verticalLines : TikZProperty = rawtext "vertical lines"

    let northEastLines : TikZProperty = rawtext "north east lines"

    let northWestLines : TikZProperty = rawtext "north west lines"

    let crosshatch : TikZProperty = rawtext "crosshatch"

    let crosshatchDots : TikZProperty = rawtext "crosshatch dots"

    let bricks : TikZProperty = rawtext "bricks"

    let checkerboard : TikZProperty = rawtext "checkerboard"

    let patternColor (color:TikZProperty) : TikZProperty = 
        rawtext "pattern color" ^=^ color



