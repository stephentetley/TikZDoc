// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Base

/// Generate output for rendering with LaTeX.
/// Alternative implmentations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module TikZBase = 

    open TikZDoc.Base

    type TikZPhantom = class end

    type TikZ = GenLaTeX<TikZPhantom>

    type TikZPropertyPhantom = class end

    type TikZProperty = GenLaTeX<TikZPropertyPhantom>


    let usetikzlibrary (arguments:GenLaTeX<'a> list) : LaTeX = 
        command "usetikzlibrary" [] arguments

    let tikzpicture (options:TikZProperty list) (body:TikZ) : LaTeX = 
        environment options "tikzpicture" body

    let draw (options:TikZProperty list) : TikZ = 
        command "draw" options []

    let fill (options:GenLaTeX<'a> list) : TikZ = 
        command "fill" options []

    let filldraw (options:GenLaTeX<'a> list) : TikZ = 
        command "filldraw" options []



    /// Units are clunky because they must be the same type 
    /// (ruling out units-of-measure), but a particular diagram 
    /// is expected to use just one unit so a shim API for 
    /// the diagram could fix the unit in the function signatures.
    type Dims = 
        | PT of double
        | BP of double
        | MM of double
        | CM of double
        | IN of double
        | EX of double
        | EM of double
        member x.LaTeX 
            with get() : TikZProperty = 
                match x with 
                | PT d -> raw <| sprintf "%fpt" d
                | BP d -> raw <| sprintf "%fbp" d
                | MM d -> raw <| sprintf "%fmm" d
                | CM d -> raw <| sprintf "%fcm" d
                | IN d -> raw <| sprintf "%fin" d
                | EX d -> raw <| sprintf "%fex" d
                | EM d -> raw <| sprintf "%fem" d

    // Coordinates

    type Coord = 
        val Units : option<Dims>
        val XPos : decimal
        val YPos : decimal

        new (x:decimal, y:decimal) = 
            { Units = None 
            ; XPos = x
            ; YPos = y }
        
        new (x:decimal, y:decimal, dims:Dims) = 
            { Units = Some dims
            ; XPos = x
            ; YPos = y }
            
        new (x:double, y:double) = 
            { Units = None 
            ; XPos = decimal x
            ; YPos = decimal y }
        
        new (x:double, y:double, dims:Dims) = 
            { Units = Some dims 
            ; XPos = decimal x
            ; YPos = decimal y }

        member x.LaTeX 
            with get() = 
                let sx = raw <| x.XPos.ToString()
                let sy = raw <| x.YPos.ToString()
                match x.Units with 
                | None -> parens (sx ^^ raw "," ^^ sy)
                | Some dims -> parens (sx ^^ dims.LaTeX ^^ raw "," ^^ sy ^^ dims.LaTeX)

        


    // Other 

    let datavisualization (options:LaTeX list) : LaTeX = 
        command "datavisualization" options []
