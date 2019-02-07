
all: example1 example2 example3 geo timing1 timing2

example1: example1.tex
	latex   --output-directory="./out"  "example1.tex"
	dvisvgm --output="./out/example1.svg" --bbox=none "./out/example1.dvi"

example2: example2.tex
	latex   --output-directory="./out"  "example2.tex"
	dvipdfm -o "./out/example2.pdf"  "./out/example2.dvi"

example3: example3.tex
	latex   --output-directory="./out"  "example3.tex"
	dvips -o "./out/example3.ps"  "./out/example3.dvi"

geo: geo.tex
	latex   --output-directory="./out"  "geo.tex"
	dvisvgm --output="./out/geo.svg" --bbox=none "./out/geo.dvi"

timing1: timing1.tex
	latex   --output-directory="./out"  "timing1.tex"
	dvisvgm --output="./out/timing1.svg" --bbox=none "./out/timing1.dvi"

timing2: timing2.tex timing2a.tex
	latex --output-directory="./out"  "timing2.tex"
	dvips -o "./out/timing2.ps"  "./out/timing2.dvi"
	latex   --output-directory="./out"  "timing2a.tex"
	dvisvgm --output="./out/timing2a.svg" --bbox=none "./out/timing2a.dvi"
