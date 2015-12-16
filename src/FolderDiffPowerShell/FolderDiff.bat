@ECHO OFF

SET CurrentPath=%~dp0
SET ReferenceFolder=%1
SET DifferenceFolder=%2
SET SupportLongFilenames=%3

IF [%3]==[] SET SupportLongFilenames=$False

ECHO.   
PowerShell.exe -NoProfile -ExecutionPolicy ByPass -Command "%CurrentPath%FolderDiff.ps1 %ReferenceFolder% %DifferenceFolder% %SupportLongFilenames%  | ForEach { $_.FullName }"