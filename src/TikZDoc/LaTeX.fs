// Copyright (c) Stephen Tetley 2018,2019
// License: BSD 3 Clause

namespace TikZDoc




[<AutoOpen>]
module LaTeX = 

    open System.IO
  
    open SLFormat
    
    open TikZDoc.Internal.Common
    open TikZDoc.Internal.Invoke
    open TikZDoc.Internal.LaTeXDoc

    type LaTeX = LaTeXDocument

    let raw : string -> LaTeX = rawText

    let (^^) : LaTeX -> LaTeX -> LaTeX  = liftCat Pretty.beside

    let (^+^) : LaTeX -> LaTeX -> LaTeX = liftCat Pretty.besideSpace

    let (^@@^) : LaTeX -> LaTeX -> LaTeX = liftCat Pretty.(^@@^)

    let hcat : LaTeX list -> LaTeX = liftCats Pretty.hcat

    let hsep : LaTeX list -> LaTeX = liftCats Pretty.hcatSpace

    let vcat : LaTeX list -> LaTeX = liftCats Pretty.vcat

    let parens : LaTeX -> LaTeX = liftOp Pretty.parens


    let comment (text: string) : LaTeX = 
        let comment1 (s:string) = raw ("% " + s)
        vcat << List.map comment1 <| toLines text 

    let arguments : LaTeX list -> LaTeX = argumentsList 

    let options : LaTeX list -> LaTeX = optionsList   
    
    
    /// <propertyName>=<propertyValue>
    let property (propertyName:string) (propertyValue:LaTeX) : LaTeX = 
        raw propertyName  ^^ raw "=" ^^ propertyValue


    /// \<name>
    let commandZero (name:string) : LaTeX = raw ("\\" + name)

    /// \<name>[<options>]{<arguments>}
    let command (name:string) (options: LaTeX list) (arguments: LaTeX list) : LaTeX = 
        let opts = 
            match options with
            | [] -> empty
            | xs -> optionsList xs
        let args = 
            match arguments with
            | [] -> empty
            | xs -> argumentsList xs
        commandZero name ^^ opts ^^ args

    /// \def\<name><body>
    /// No attempt is made to match TeX syntax for the macro body.
    let def (name:string) (body:LaTeX) : LaTeX = 
        command "def" [] [] ^^ command name [] [] ^^ body

    /// \documentclass[<options>]{<name>}
    let documentclass (options:LaTeX list) (name:string) : LaTeX = 
        command "documentclass" options [raw name]

    /// \usepackage[<options>]{<name>}
    let usepackage (options:LaTeX list) (name:string) : LaTeX = 
        command "usepackage" options [raw name]

    /// \begin[<options>]{<name>}
    /// _Cmd suffix as begin is a keyword
    let beginCmd (options:LaTeX list) (name:string) : LaTeX = 
        command "begin" options [raw name]
    
    /// \end{<name>}
    /// _Cmd suffix as end is a keyword
    let endCmd (name:string) : LaTeX = 
        command "end" [] [raw name]

    let block (options:LaTeX list) (name:string)  (body:LaTeX) : LaTeX =
        beginCmd options name ^@@^ body ^@@^ endCmd name


    // ************************************************************************
    // Output


    type internal LaTeXOutput = 
        | PostScript 
        | PDF 
        | SVG
        
        member x.DocumentProlog 
            with get() : LaTeX = 
                match x with 
                | PostScript -> documentclass [] "article"
                | PDF -> documentclass [] "article" ^@@^ def "pgfsysdriver" (arguments [raw "pgfsys-dvipdfm.def"])
                | SVG -> documentclass [raw "dvisvgm"] "minimal"        


    type LaTeXDocument with
        
        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToSVG(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = SVG.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvisvgm outputDirectory fileName

        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToPS(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = PostScript.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvips outputDirectory fileName

        /// Note - this procedure creates a number of auxiliary files that you 
        /// may want to (manually) delete.
        member x.SaveToPDF(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = PDF.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvipdfm outputDirectory fileName

