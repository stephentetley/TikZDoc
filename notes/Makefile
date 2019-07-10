
all: example1-pdf example1-ps example1-svg geo timing1 timing2

example1-svg: example1-svg.tex
	latex   --output-directory="./output"  "example1-svg.tex"
	dvisvgm --output="./output/example1-svg.svg" --bbox=none "./output/example1-svg.dvi"

example1-pdf: example1-pdf.tex
	latex   --output-directory="./output"  "example1-pdf.tex"
	dvipdfm -o "./output/example1-pdf.pdf"  "./output/example1-pdf.dvi"

example1-ps: example1-ps.tex
	latex   --output-directory="./output"  "example1-ps.tex"
	dvips -o "./output/example1-ps.ps"  "./output/example1-ps.dvi"

geo: geo.tex
	latex   --output-directory="./output"  "geo.tex"
	dvisvgm --output="./output/geo.svg" --bbox=none "./output/geo.dvi"

