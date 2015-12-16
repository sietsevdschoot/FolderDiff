param (
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

. ".\32BitHelpers.ps1"

function FolderDiff-Search { 
	param(
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFilesNames)
	
	. ".\Utils.ps1" $supportLongFilesNames

	Add-Type -path '.\FolderDiffLib.dll'
	$instance = New-Object FolderDiffLib.FolderDiff

	@($instance.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames
		,{ param($diffFile, $referenceFiles) 
			$refFileExists = (File-Exists $referenceFolder $diffFile.RelativePath)
			$diffFileIsNewer = $diffFile.File.LastWriteTime -gt (Get-File $referenceFolder $diffFile.RelativePath).LastWriteTime 
			
			$refFileExists -and $diffFileIsNewer }
		,{ param($diffFile, $referenceFiles) -Not (File-Exists $referenceFolder $diffFile.RelativePath) }
	))
}

function FolderDiff { 
	param(
	[string] $referenceFolder,
	[string] $differenceFolder,
	[boolean] $supportLongFileNames)

	if (-not (Is32BitEnvironment) -and $supportLongFileNames)
	{
		RunAs32Bit-FolderDiff-Search $referenceFolder $differenceFolder $supportLongFileNames
	}
	else
	{
		FolderDiff-Search $referenceFolder $differenceFolder $supportLongFileNames 
	}
}

if ($referenceFolder -and $differenceFolder) 
{
	FolderDiff $referenceFolder $differenceFolder $supportLongFilenames 
}

