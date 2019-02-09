// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// Don't AutoOpen


/// Colors and printing effects (opacity, blend mode...)
module Colors = 

    open TikZDoc.Base

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