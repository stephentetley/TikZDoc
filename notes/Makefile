
all: example1 example2

example1: example1.tex
	latex   --output-directory=./out  example1.tex
	dvisvgm --libgs="C:\programs\gs\gs9.15\bin\gsdll32.dll" -o ./out/example1.svg  ./out/example1.dvi

example2: example2.tex
	latex   --output-directory=./out  example2.tex
	dvips -o ./out/example2.ps  ./out/example2.dvi
	dvipdfm -o ./out/example2.pdf  ./out/example2.dvi




