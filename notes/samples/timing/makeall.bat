@echo on

REM example1
latex   --output-directory=./output  example1.tex
dvisvgm --output=./output/example1.svg --bbox=none ./output/example1.dvi


REM example2
latex   --output-directory=./output  example2.tex
dvipdfm -o ./output/example2.pdf  ./output/example2.dvi

REM example3
latex   --output-directory=./output  example3.tex
dvips -o ./output/example3.ps  ./output/example3.dvi

REM geo
latex   --output-directory=./output  geo.tex
dvisvgm --output=./output/geo.svg --bbox=none ./output/geo.dvi

REM timing1
latex   --output-directory="./output"  "timing1.tex"
dvisvgm --output="./output/timing1.svg" --bbox=none "./output/timing1.dvi"

REM timing2
latex --output-directory="./output"  "timing2.tex"
dvips -o "./output/timing2.ps"  "./output/timing2.dvi"
latex   --output-directory="./output"  "timing2a.tex"
dvisvgm --output="./output/timing2a.svg" --bbox=none "./output/timing2a.dvi"
