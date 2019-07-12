// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

namespace TikZDoc.Extensions


module Forest =


    open TikZDoc.Base

    
    type ForestTikZPhantom = class end

    /// A specific type for LaTeX, e.g. in the prolog before "\\tikzpicture"
    type ForestTikZ = GenLaTeX<ForestTikZPhantom>


    /// Choices are "edges" and "linguistics"
    let useforestlibrary (libraries : string list) : LaTeX = 
        match libraries with
        | [] -> emptyLaTeX ()
        | _ -> 
            let libs = List.map rawtext libraries
            command "useforestlibrary" None (Some libs)

    
    let forest (body : ForestTikZ) : LaTeX = 
        beginCmd [] "forest" ^!!^ body ^!!^ endCmd "forest"

    let forTree (args : GenLaTeX<'a> list) = 
        rawtext "for tree" ^=^ formatArguments args



    let folderProp : TikZProperty = rawtext "folder"
    let drawProp : TikZProperty = rawtext "draw"

    let growTick (direction : GenLaTeX<'a>) : TikZProperty = 
        rawtext "grow'" ^=^ direction

    let forestNode (label : GenLaTeX<'a>) (kids : ForestTikZ list) : ForestTikZ = 
        match kids with
        | [] -> brackets label
        | _ -> hang 4 (brackets (label ^!!^  (vcat kids)))

    let addNodeArgs (label : GenLaTeX<'x>) 
                    (args : GenLaTeX<'y> list) : GenLaTeX<'a> = 
        label ^^ character ',' ^+^ text "node options" ^=^ formatArguments args

    let forestDocument (forestLibraries : string list) (body : ForestTikZ) : LaTeX = 
        vcat 
            [ usepackage "forest" 
            ; useforestlibrary forestLibraries
            ; document [] (forest body)
            ]

        