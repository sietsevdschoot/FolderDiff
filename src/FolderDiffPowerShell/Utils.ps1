param (
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

Add-Type -path '.\FolderDiffLib.dll'
$factory = New-Object FolderDiffLib.FolderDiffToolFactory
$diffTool = $factory.Create($supportLongFilenames)

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

function File-Exists {
	param(
	[string] $path,
	[string] $fileRelativePath
	)

	$fileInReferenceFolder = Join-Path -Path $path -ChildPath $fileRelativePath
	$diffTool.FileExists($fileInReferenceFolder)
}

function Get-File {
	param(
	[string] $path,
	[string] $fileRelativePath
	)

	$fileInReferenceFolder = Join-Path -Path $path -ChildPath $fileRelativePath
	$diffTool.GetFile($fileInReferenceFolder)
}
