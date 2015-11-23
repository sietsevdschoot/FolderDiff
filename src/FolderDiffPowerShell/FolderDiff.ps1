param (
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

. ".\32BitHelpers.ps1"
. ".\MyFolderDiff.ps1"

if (-not (Is32BitEnvironment) -and $supportLongFilenames)
{
	RunFolderDiffAs32Bit $referenceFolder  $differenceFolder  $supportLongFilenames
}
else
{
	MyFolderDiff $referenceFolder $differenceFolder $supportLongFilenames 
}


