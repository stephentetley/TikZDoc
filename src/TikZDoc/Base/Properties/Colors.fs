// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen


/// Colors and printing effects (opacity, blend mode...)
module Colors = 

    open TikZDoc.Base

     // Basic colors

    let black : TikZProperty = rawtext "black"
    
    let blue : TikZProperty = rawtext "blue"
    
    let brown : TikZProperty = rawtext "brown"
    
    let cyan : TikZProperty = rawtext "cyan"
    
    let darkgray : TikZProperty = rawtext "darkgray"
    
    let gray : TikZProperty = rawtext "gray"
    
    let green : TikZProperty = rawtext "green"
    
    let lightgray : TikZProperty = rawtext "lightgray"
    
    let lime : TikZProperty = rawtext "lime"
    
    let magenta : TikZProperty = rawtext "magenta"
    
    let olive : TikZProperty = rawtext "olive"
    
    let orange : TikZProperty = rawtext "orange"
    
    let pink : TikZProperty = rawtext "pink"
    
    let purple : TikZProperty = rawtext "purple"
    
    let red : TikZProperty = rawtext "red"
    
    let teal : TikZProperty = rawtext "teal"
    
    let violet : TikZProperty = rawtext "violet"
    
    let white : TikZProperty = rawtext "white"
    
    let yellow : TikZProperty = rawtext "yellow"

    // Opacity
    
    let opacity (level:double) : TikZProperty = 
        keyvalue "opacity" (rawtext <| sprintf "%f" level)
        
    let transparent : TikZProperty = rawtext "transparent"
    
    let ultraNearlyTransparent : TikZProperty = rawtext "ultra nearly transparent"
    
    let veryNearlyTransparent : TikZProperty = rawtext "very nearly transparent"
    
    let nearlyTransparent : TikZProperty = rawtext "nearly transparent"
    
    let semitransparent : TikZProperty = rawtext "semitransparent"
    
    let nearlyOpaque : TikZProperty = rawtext "nearly opaque"
    
    let veryNearlyOpaque : TikZProperty = rawtext "very nearly opaque" 
    
    let ultraNearlyOpaque : TikZProperty = rawtext "ultra nearly opaque"
    
    let opaque : TikZProperty = rawtext "opaque"
    
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
                | BlendNormal -> rawtext "normal"
                | BlendMultiply -> rawtext "multiply"
                | BlendScreen -> rawtext "screen"
                | BlendOverlay -> rawtext "overlay"
                | BlendDarken -> rawtext "darken"
                | BlendLighten -> rawtext "lighten"
                | BlendDifference -> rawtext "difference"
                | BlendExclusion -> rawtext "exclusion"
                | BlendHue -> rawtext "hue"
                | BlendSaturation -> rawtext "saturation"
                | BlendColor -> rawtext "color"
                | BlendLuminosity -> rawtext "luminosity"
                
    let blendGroup (blendMode:BlendMode) : TikZProperty = 
        keyvalue "blend mode" blendMode.LaTeX