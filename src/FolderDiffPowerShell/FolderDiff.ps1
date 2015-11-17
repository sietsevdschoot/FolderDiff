 param (
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

. ".\CollectionUtils.ps1"

function MyFolderDiff {
	param(
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilesNames)
	
	Add-Type -path '.\FolderDiffLib.dll'

	$instance = New-Object FolderDiffLib.FolderDiff

	@($instance.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames
		,{ param($file, $refFiles) $refFiles | Test-Any { $file.RelativePath -eq $_.RelativePath -and $file.File.LastWriteTime -gt $_.File.LastWriteTime} }
		,{ param($file, $refFiles) ($refFiles | %{ $_.RelativePath }) -NotContains $file.RelativePath }
	))
}

MyFolderDiff $referenceFolder $differenceFolder $supportLongFilenames

