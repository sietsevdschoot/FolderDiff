Set-Location $PSScriptRoot

. ".\Utils.ps1"

function MyFolderDiff {
	param(
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilesNames)
	
	Add-Type -path '.\FolderDiffLib.dll'

	$instance = New-Object FolderDiffLib.FolderDiff

	@($instance.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames
		,{ param($file, $referenceFiles) $referenceFiles | Test-Any { $file.RelativePath -eq $_.RelativePath -and $file.File.LastWriteTime -gt $_.File.LastWriteTime} }
		,{ param($file, $referenceFiles) ($referenceFiles | %{ $_.RelativePath }) -NotContains $file.RelativePath }
	))
}
