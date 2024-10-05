#!/bin/bash

: # : -------------------------------------------------------------------- ::
: # :	Description                                                        ::
: # : -------------------------------------------------------------------- ::

: # This script can be executed under Windows and Linux. It is one file, but
: # it contains different sections for the execution of different scripts 
: # that are independent of each other.
: # Windows: Use a double-click or execution it in the terminal is sufficient.
: # MINGW/Msys behaves like Windows.
: # Linux: Add the rights to the file "chmod +x NAME" and execute it "./NAME".


: # : -------------------------------------------------------------------- ::
: # :	Windows script section                                             ::
: # : -------------------------------------------------------------------- ::
:<<"::CMDLITERAL"

@ECHO OFF
cls
cd "%~dp0"
::echo Windows
"Chris82111.TerminalEpubComicToCbz.exe" %*
:: End Windows script section
exit /B 0


: # : -------------------------------------------------------------------- ::
: # :	Linux script section                                               ::
: # : -------------------------------------------------------------------- ::
::CMDLITERAL

#echo "Linux, ${SHELL}"
dotnet Chris82111.TerminalEpubComicToCbz.dll $@
# End Linux script section
exit $?


: # : -------------------------------------------------------------------- ::
: # :	EOF                                                                ::
: # : -------------------------------------------------------------------- ::
