// Copyright (c) Stephen Tetley 2019
// License: BSD 3 Clause

namespace TikZDoc.Extensions


module Forest =


    open TikZDoc.Base

    
    type ForestTikZPhantom = class end

    /// A specific type for LaTeX, e.g. in the prolog before "\\tikzpicture"
    type ForestTikZ = GenLaTeX<ForestTikZPhantom>

    let node (label : GenLaTeX<'a>) (kids : ForestTikZ list) : ForestTikZ = 
        match kids with
        | [] -> braces label
        | _ -> braces (label ^//^ vcat kids)

        