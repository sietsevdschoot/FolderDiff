Set-Location $PSScriptRoot

function Is32BitEnvironment()
{
	[IntPtr]::Size -eq 4
}

function RunAs32Bit-FolderDiff-Search {
	param (
		[string] $referenceFolder,
		[string] $differenceFolder,
		[boolean] $supportLongFilenames
	 )

	$path = (Resolve-Path ./FolderDiff.ps1).Path

	Start-Job {
		param($ScriptPath)
		. "$ScriptPath"

		FolderDiff-Search $args[0] $args[1] $args[2] 
	} -ArgumentList $path, $referenceFolder, $differenceFolder, $supportLongFilenames -RunAs32 | Wait-Job | Receive-Job 
}
