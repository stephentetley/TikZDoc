
all: tree1-svg tree1-ps

tree1-svg: tree1-svg.tex
	latex   --output-directory="./output"  "tree1-svg.tex"
	dvisvgm --output="./output/tree1-svg.svg" --bbox=none "./output/tree1-svg.dvi"
    
tree1-ps: tree1-ps.tex
	latex   --output-directory="./output"  "tree1-ps.tex"
	dvips -o "./output/tree1-ps.ps"  "./output/tree1-ps.dvi"
    
    
tree1-pdf: tree1-pdf.tex
	latex   --output-directory="./output"  "tree1-pdf.tex"
	dvipdfm -o "./output/tree1-pdf.pdf"  "./output/tree1-pdf.dvi"
