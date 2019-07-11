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
        command "usetikzlibrary" None (Some arguments)

    let tikzpicture (options: TikZProperty list) (body:TikZ) : LaTeX = 
        environment (itemsToOption options) "tikzpicture" body

    
    let draw (options : TikZProperty list) : TikZ = 
        command "draw" (itemsToOption options) None

    let fill (options : TikZProperty list) : TikZ = 
        command "fill" (itemsToOption options)  None

    let filldraw (options: TikZProperty list) : TikZ = 
        command "filldraw" (itemsToOption options)  None



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
                | PT d -> rawtext <| sprintf "%fpt" d
                | BP d -> rawtext <| sprintf "%fbp" d
                | MM d -> rawtext <| sprintf "%fmm" d
                | CM d -> rawtext <| sprintf "%fcm" d
                | IN d -> rawtext <| sprintf "%fin" d
                | EX d -> rawtext <| sprintf "%fex" d
                | EM d -> rawtext <| sprintf "%fem" d

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
            with get()  = 
                let sx = rawtext <| x.XPos.ToString()
                let sy = rawtext <| x.YPos.ToString()
                match x.Units with 
                | None -> parens (sx ^^ rawtext "," ^^ sy)
                | Some dims -> parens (sx ^^ dims.LaTeX ^^ rawtext "," ^^ sy ^^ dims.LaTeX)

        


    // Other 

    let datavisualization (options : option<LaTeX list>) : LaTeX = 
        command "datavisualization" options None
