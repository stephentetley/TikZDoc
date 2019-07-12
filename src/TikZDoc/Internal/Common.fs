// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc.Internal

module Common = 

    open System

    open SLFormat.Pretty

    /// Splits on Environment.NewLine
    let toLines (source:string) : string list = 
        source.Split(separator=[| Environment.NewLine |], options=StringSplitOptions.None) |> Array.toList

    /// Joins with Environment.NewLine
    let fromLines (source:string list) : string = 
        String.concat Environment.NewLine source

    let doubleQuote (s:string) : string = "\"" + s + "\""

    /// Note - the answer type can be different to input types.
    let commaSpaceSep (items : Doc list) : Doc = 
        punctuate (text ", ")  items




