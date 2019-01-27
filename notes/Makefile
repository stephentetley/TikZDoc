
all: example1 example2 example3 geo

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
