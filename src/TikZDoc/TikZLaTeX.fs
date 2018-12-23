// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc

/// Generate output for rendering with LaTeX.
/// Alternative implmenetations for plain TeX or ConTEXt are possible 
/// but unlikely to be realized.


[<AutoOpen>]
module TikZLaTeX = 

    open TikZDoc

    let usetikzlibrary (arguments:LaTeX list) : LaTeX = 
        command "usetikzlibrary" [] arguments

    let draw (options:LaTeX list) : LaTeX = 
        command "draw" options []

    let datavisualization (options:LaTeX list) : LaTeX = 
        command "datavisualization" options []