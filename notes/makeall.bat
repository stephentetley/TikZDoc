@echo on

REM example1
latex   --output-directory=./output  example1-svg.tex
dvisvgm --output=./output/example1-svg.svg --bbox=none ./output/example1-svg.dvi


REM example2
latex   --output-directory=./output  example1-pdf.tex
dvipdfm -o ./output/example1-pdf.pdf  ./output/example1-pdf.dvi

REM example3
latex   --output-directory=./output  example1-ps.tex
dvips -o ./output/example1-ps.ps  ./output/example1-ps.dvi

REM geo
latex   --output-directory=./output  geo.tex
dvisvgm --output=./output/geo.svg --bbox=none ./output/geo.dvi
