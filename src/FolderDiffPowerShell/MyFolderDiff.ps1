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
		,{ param($diffFile, $referenceFiles) (File-Exists $referenceFolder $file.RelativePath) -and $diffFile.File.LastWriteTime -gt (Get-File $referenceFolder $diffFile.RelativePath).LastWriteTime }
		,{ param($diffFile, $referenceFiles) -Not (File-Exists $referenceFolder $diffFile.RelativePath) }
	))
}
