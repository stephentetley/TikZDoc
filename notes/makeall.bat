@echo on

REM example1
latex   --output-directory=./out  example1.tex
dvisvgm --output=./out/example1.svg --bbox=none ./out/example1.dvi


REM example2
latex   --output-directory=./out  example2.tex
dvipdfm -o ./out/example2.pdf  ./out/example2.dvi

REM example3
latex   --output-directory=./out  example3.tex
dvips -o ./out/example3.ps  ./out/example3.dvi

REM geo
latex   --output-directory=./out  geo.tex
dvisvgm --output=./out/geo.svg --bbox=none ./out/geo.dvi
