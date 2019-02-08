// Copyright (c) Stephen Tetley ,2019
// License: BSD 3 Clause

namespace TikZDoc.Extensions.TikZTiming


[<AutoOpen>]
module Timing = 
    open SLFormat
    open TikZDoc

    type TimingChar = 
        | High
        | Low
        | HighImpedance
        | Undefined
        | DataDouble
        | Unknown
        | Toggle
        | Clock
        | Metastable
        | Glitch
        | Space
        
        /// Full width signals
        member x.Upper
            with get () : char = 
                match x with
                | High -> 'H'
                | Low -> 'L'
                | HighImpedance -> 'Z'
                | Undefined -> 'X'
                | DataDouble -> 'D'
                | Unknown -> 'U'
                | Toggle -> 'T'
                | Clock -> 'C'
                | Metastable -> 'M'
                | Glitch -> 'G'
                | Space -> 'S'

        /// Half width signals
        member x.Lower
            with get () : char = 
                match x with
                | High -> 'h'
                | Low -> 'l'
                | HighImpedance -> 'z'
                | Undefined -> 'x'
                | DataDouble -> 'd'
                | Unknown -> 'u'
                | Toggle -> 't'
                | Clock -> 'c'
                | Metastable -> 'm'
                | Glitch -> 'g'
                | Space -> 's'

    let texttiming (settings:LaTeX list) (characters:TimingChar list) : LaTeX = 
        let chars :string = characters |> List.map (fun x -> x.Upper) |> System.String.Concat
        command "texttiming" settings [raw chars]
