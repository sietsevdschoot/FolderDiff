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
	
	Add-Type -path '.\FolderDiffLib.dll'
	$folderDiff = [FolderDiffLib.DiffTools.FolderDiffFactory]::Create($supportLongFilesNames);

	. ".\Utils.ps1" $supportLongFilesNames
		
	@($folderDiff.DiffFolder($referenceFolder, $differenceFolder, 
		{ param($diffFile, $referenceFiles) 
			
			$refFile = Get-File (Path-Combine $referenceFolder $diffFile.RelativePath)
			$diffFileIsNewer = $diffFile.File.LastWriteTime -gt $refFile.LastWriteTime 
			
			$refFile.Exists -and $diffFileIsNewer 
		},
		{ param($diffFile, $referenceFiles) 
			-Not (File-Exists (Path-Combine $referenceFolder $diffFile.RelativePath)) 
		}
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

