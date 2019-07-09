
all: fast-diagram-svg fast-diagram-pdf fast-diagram-ps

fast-diagram-svg: fast-diagram-svg.tex
	latex   --output-directory="./output"  "fast-diagram-svg.tex"
	dvisvgm --output="./output/fast-diagram-svg.svg" --bbox=none "./output/fast-diagram-svg.dvi"
    
fast-diagram-ps: fast-diagram-pdf.tex
	latex   --output-directory="./output"  "fast-diagram-ps.tex"
	dvips -o "./output/fast-diagram-ps.ps"  "./output/fast-diagram-ps.dvi"
    
    
fast-diagram-pdf: fast-diagram-pdf.tex
	latex   --output-directory="./output"  "fast-diagram-pdf.tex"
	dvipdfm -o "./output/fast-diagram-pdf.pdf"  "./output/fast-diagram-pdf.dvi"
