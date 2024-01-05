echo COMPILE
tasm /z/zi/n main main main
echo COMPOSITION
tlink/v MAIN.OBJ
echo RUNNING
MAIN.exe
