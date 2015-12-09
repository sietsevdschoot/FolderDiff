Set-Location $PSScriptRoot

function MyFolderDiff {
	param(
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilesNames)
	
	. ".\Utils.ps1" $supportLongFilesNames

	Add-Type -path '.\FolderDiffLib.dll'
	$instance = New-Object FolderDiffLib.FolderDiff

	@($instance.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames
		,{ param($file, $referenceFiles) (File-Exists $referenceFolder $file.RelativePath) -and $file.File.LastWriteTime -gt (Get-File $referenceFolder $file.RelativePath).LastWriteTime }
		,{ param($file, $referenceFiles) -Not (File-Exists $referenceFolder $file.RelativePath) }
	))
}
