param (
	[boolean] $supportLongFilenames
 )

Set-Location $PSScriptRoot

Add-Type -path '.\FolderDiffLib.dll'
$factory = New-Object FolderDiffLib.FolderDiffToolFactory
$diffTool = $factory.Create($supportLongFilenames)

function File-Exists {
	param(
	[string] $path,
	[string] $fileRelativePath
	)

	$fullName = Join-Path -Path $path -ChildPath $fileRelativePath
	$diffTool.FileExists($fullName)
}

function Get-File {
	param(
	[string] $path,
	[string] $fileRelativePath
	) 

	$fullName = Join-Path -Path $path -ChildPath $fileRelativePath
	$diffTool.GetFile($fullName)
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

