
all: example1 example2 example3 geo timing1 timing2

example1: example1.tex
	latex   --output-directory="./output"  "example1.tex"
	dvisvgm --output="./output/example1.svg" --bbox=none "./output/example1.dvi"

example2: example2.tex
	latex   --output-directory="./output"  "example2.tex"
	dvipdfm -o "./output/example2.pdf"  "./output/example2.dvi"

example3: example3.tex
	latex   --output-directory="./output"  "example3.tex"
	dvips -o "./output/example3.ps"  "./output/example3.dvi"

geo: geo.tex
	latex   --output-directory="./output"  "geo.tex"
	dvisvgm --output="./output/geo.svg" --bbox=none "./output/geo.dvi"

timing1: timing1.tex
	latex   --output-directory="./output"  "timing1.tex"
	dvisvgm --output="./output/timing1.svg" --bbox=none "./output/timing1.dvi"

timing2: timing2.tex timing2a.tex
	latex --output-directory="./output"  "timing2.tex"
	dvips -o "./output/timing2.ps"  "./output/timing2.dvi"
	latex   --output-directory="./output"  "timing2a.tex"
	dvisvgm --output="./output/timing2a.svg" --bbox=none "./output/timing2a.dvi"
