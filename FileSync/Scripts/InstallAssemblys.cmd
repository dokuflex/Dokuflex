@echo off

set installdir=%ProgramFiles%\DokuFlex\FileSync
set nfxver=nfx64

if not '%PROCESSOR_ARCHITECTURE%' == 'AMD64' (
    if not '%PROCESSOR_ARCHITEW6432%' == 'AMD64' (
    	rem You are running x86 Windows
        set nfxver=nfx32
    )	
)

if '%nfxver%'=='nfx32' goto nfx32
if '%nfxver%'=='nfx64' goto nfx64

:nfx32
set regasm="%WinDir%\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe" 

:nfx64
set regasm="%WinDir%\Microsoft.NET\Framework64\v4.0.30319\RegAsm.exe" 

echo nfxver=%nfxver%

%regasm% "%installdir%\DokuFlex.FileSyncOverlayInSync.dll" /codebase

%regasm% "%installdir%\DokuFlex.FileSyncOverlaySyncInProgress.dll" /codebase

%regasm% "%installdir%\DokuFlex.FileSyncOverlayErrorConflict.dll" /codebase

pause