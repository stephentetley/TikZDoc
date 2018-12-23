// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc




[<AutoOpen>]
module TikZ = 

    open TikZDoc

    let draw (options:LaTeX list) : LaTeX = 
        command "draw" options []