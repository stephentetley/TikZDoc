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
        beginCmd [] "forest" ^//^ indent body ^//^ endCmd "forest"

    let node (label : GenLaTeX<'a>) (kids : ForestTikZ list) : ForestTikZ = 
        match kids with
        | [] -> braces label
        | _ -> braces (label ^//^ indent (vcat kids))



    let forestDocument (forestLibraries : string list) (body : ForestTikZ) : LaTeX = 
        vcat 
            [ usepackage "forest" 
            // ; useforestlibrary forestLibraries
            ; document [] (forest body)
            ]

        