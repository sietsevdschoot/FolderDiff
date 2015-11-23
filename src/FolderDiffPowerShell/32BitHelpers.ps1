function Is32BitEnvironment()
{
	[IntPtr]::Size -eq 4
}

function RunFolderDiffAs32Bit {
	param (
		[string] $referenceFolder,
		[string] $differenceFolder,
		[boolean] $supportLongFilenames
	 )

	$path = (Resolve-Path ./MyFolderDiff.ps1).Path

	Start-Job {
		param($ScriptPath)
		. "$ScriptPath"
		MyFolderDiff $args[0] $args[1] $args[2] 
	} -ArgumentList $path, $referenceFolder, $differenceFolder, $supportLongFilenames -RunAs32 | Wait-Job | Receive-Job 
}
 