param (
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

Add-Type -path '.\FolderDiffLib.dll'

$fileSystem = [FolderDiffLib.DiffTools.FileSystemFactory]::Create($supportLongFilesNames);
$fileSystemHelper = New-Object FolderDiffLib.FileSystemHelper -ArgumentList $fileSystem

function Path-Combine {
	param(
	[string] $dir,
	[string] $file
	)
	$fileSystemHelper.PathCombine($dir, $file);
}

function File-Exists {
	param(
	[string] $path
	)

	$fileSystemHelper.FileExists($path);
}

function Get-File {
	param(
	[string] $path
	) 

	$fileSystemHelper.GetFile($path);
}

function Get-Files {
	param(
	[string] $path,
	[string] $searchPattern) 
	
	$fileSystemHelper.GetFiles($path, $searchPattern, [System.IO.SearchOption]::TopDirectoryOnly);
}

function Get-Files-Recursive {
	param(
	[string] $path,
	[string] $searchPattern)
		 
	$fileSystemHelper.GetFiles($path, $searchPattern);
}

#http://stackoverflow.com/a/22090065
function Test-Any {
    [CmdletBinding()]
    param($EvaluateCondition,
        [Parameter(ValueFromPipeline = $true)] $ObjectToTest)
    begin {
        $any = $false
    }
    process {
        if (-not $any -and (& $EvaluateCondition $ObjectToTest)) {
            $any = $true
        }
    }
    end {
        $any
    }
}

