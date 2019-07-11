// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// TODO - more code can moved to separate modules.


[<AutoOpen>]
module Misc = 

    open TikZDoc.Base


    
    
    let huge : TikZProperty = command "Huge" [] []




    let name (nodeName:string) : TikZProperty = 
        keyvalue "name" (rawtext nodeName)
    
    let alias (aliasName:string) : TikZProperty = 
        keyvalue "alias" (rawtext aliasName)

    let nodeContents (contents:LaTeX) : TikZProperty = 
        keyvalue "node contents" contents



        



    
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
                | AnchorNorthWest -> rawtext "north west"
                | AnchorNorth -> rawtext "north"
                | AnchorNorthEast -> rawtext "north east"
                | AnchorText -> rawtext "text"
                | AnchorWest -> rawtext "west"
                | AnchorMidWest -> rawtext "mid west"
                | AnchorBaseWest -> rawtext "base west"
                | AnchorBase -> rawtext "base"
                | AnchorEast -> rawtext "east"
                | AnchorMidEast -> rawtext "mid east"
                | AnchorBaseEast -> rawtext "base east"
                | AnchorMid -> rawtext "mid"
                | AnchorSouthEast -> rawtext "south east"
                | AnchorSouth -> rawtext "south"
                | AnchorSouthWest -> rawtext "south west"
                | AnchorCenter -> rawtext "center"
                | AnchorDegree(x) -> rawtext <| x.ToString()

    let anchor (position:Anchor) : TikZProperty = 
        keyvalue "anchor" position.LaTeX



