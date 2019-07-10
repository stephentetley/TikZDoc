
all: forest-svg forest-pdf forest-ps



forest-svg: forest-svg.tex
	latex   --output-directory="./output"  "forest-svg.tex"
	dvisvgm --output="./output/forest-svg.svg" --bbox=none "./output/forest-svg.dvi"
    
forest-ps: forest-ps.tex
	latex   --output-directory="./output"  "forest-ps.tex"
	dvips -o "./output/forest-ps.ps"  "./output/forest-ps.dvi"
    
    
forest-pdf: forest-pdf.tex
	latex   --output-directory="./output"  "forest-pdf.tex"
	dvipdfm -o "./output/forest-pdf.pdf"  "./output/forest-pdf.dvi"
