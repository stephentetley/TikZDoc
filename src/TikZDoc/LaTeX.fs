// Copyright (c) Stephen Tetley 2018
// License: BSD 3 Clause

namespace TikZDoc




[<AutoOpen>]
module LaTeX = 

    open System.IO

    open TikZDoc.Internal.LaTeXDoc
    open TikZDoc.Internal


    type LaTeX = LaTeXDocument

    let raw : string -> LaTeX = rawText

    let (^^) : LaTeX -> LaTeX -> LaTeX  = liftCat PrettyPrint.beside

    let (^+^) : LaTeX -> LaTeX -> LaTeX = liftCat PrettyPrint.besideSpace

    let (^@@^) : LaTeX -> LaTeX -> LaTeX = liftCat PrettyPrint.below

    let hcat : LaTeX list -> LaTeX = liftCats PrettyPrint.hcat

    let hsep : LaTeX list -> LaTeX = liftCats PrettyPrint.hsep

    let vcat : LaTeX list -> LaTeX = liftCats PrettyPrint.vcat


    let comment (text: string) : LaTeX = 
        let comment1 (s:string) = raw ("% " + s)
        vcat << List.map comment1 <| unlines text 

    let arguments : LaTeX list -> LaTeX = argumentsList 

    let options : LaTeX list -> LaTeX = optionsList         

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

    
    /// > latex "<InputFile>.tex"         
    let private runLatex (shellWorkingDirectory:string) (finalName:string) : unit =
        let texFile = Path.ChangeExtension(finalName, "tex")
        let command = doubleQuote texFile
        shellRun shellWorkingDirectory "latex" command

    /// > dvips -o "<FinalName>" "<RootName>.dvi"
    let private runDvips (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let psFile = finalName
        let command = sprintf "-o %s %s" (doubleQuote psFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvips" command

    /// > dvipdfm -o "<FinalName>" "<RootName>.dvi"
    let private runDvipdfm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let pdfFile = finalName
        let command = sprintf "-o %s %s" (doubleQuote pdfFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvipdfm" command

    /// > dvisvgm --output="<FinalName>" --bbox=none "<RootName>.dvi"
    let private runDvisvgm (shellWorkingDirectory:string) (finalName:string) : unit =
        let dviFile = Path.ChangeExtension(finalName, "dvi")
        let svgFile = finalName
        let command = sprintf "--output=%s --bbox=none %s" (doubleQuote svgFile) (doubleQuote dviFile)
        shellRun shellWorkingDirectory "dvisvgm" command



    type LaTeXDocument with

        member x.SaveToSVG(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = SVG.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvisvgm outputDirectory fileName


        member x.SaveToPS(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = PostScript.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvips outputDirectory fileName


        member x.SaveToPDF(outputDirectory:string, fileName:string) : unit = 
            let doc:LaTeX = PDF.DocumentProlog ^@@^ x
            let tex1 = Path.ChangeExtension(fileName, "tex")
            let texFile = Path.Combine(outputDirectory,tex1)
            doc.SaveAsTex(80,texFile)
            runLatex outputDirectory fileName
            runDvipdfm outputDirectory fileName