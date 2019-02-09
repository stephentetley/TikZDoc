// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base.Properties

// TODO - more code can moved to separate modules.


[<AutoOpen>]
module Misc = 

    open TikZDoc.Base


    
    
    let huge : TikZProperty = command "Huge" [] []




    let name (nodeName:string) : TikZProperty = 
        keyvalue "name" (raw nodeName)
    
    let alias (aliasName:string) : TikZProperty = 
        keyvalue "alias" (raw aliasName)

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
                | AnchorNorthWest -> raw "north west"
                | AnchorNorth -> raw "north"
                | AnchorNorthEast -> raw "north east"
                | AnchorText -> raw "text"
                | AnchorWest -> raw "west"
                | AnchorMidWest -> raw "mid west"
                | AnchorBaseWest -> raw "base west"
                | AnchorBase -> raw "base"
                | AnchorEast -> raw "east"
                | AnchorMidEast -> raw "mid east"
                | AnchorBaseEast -> raw "base east"
                | AnchorMid -> raw "mid"
                | AnchorSouthEast -> raw "south east"
                | AnchorSouth -> raw "south"
                | AnchorSouthWest -> raw "south west"
                | AnchorCenter -> raw "center"
                | AnchorDegree(x) -> raw <| x.ToString()

    let anchor (position:Anchor) : TikZProperty = 
        keyvalue "anchor" position.LaTeX



