param (
	[parameter(Position=0)]
	[bool] $longFilenamesSupport = $false, 
	[parameter(Position=1)]
	$fileSystem
)

Set-Location $PSScriptRoot

Add-Type -path '.\FolderDiffLib.dll'
Add-Type -path '.\System.IO.Abstractions.dll'

$fileSystem = [System.IO.Abstractions.IFileSystem] $fileSystem

if (-not $fileSystem)
{
	$fileSystem = [FolderDiffLib.DiffTools.FileSystemFactory]::Create($longFilenamesSupport);
}

$fileSystemHelper = New-Object FolderDiffLib.FileSystemHelper -args $fileSystem

function Combine-Path {
	param(
	[ValidateNotNullOrEmpty()]  
	[string[]] $paths
	)
	$fileSystemHelper.PathCombine($paths);
}

function File-Exists {
	param(
	[string] $path
	)
	(Get-File($path)).File.Exists;
}

function Get-File {
	param(
	[ValidateNotNullOrEmpty()]  
	[string] $path
	) 
	$fileSystemHelper.GetFile($path);
}

function Get-Files {
	param(
	[parameter(Position=0, Mandatory)]
    [ValidateNotNullOrEmpty()]
	[string] $path,
	[parameter(Position=1)]
	[string] $searchPattern = "*.*",
	[switch] $recursive) 
	
	$searchOption = if ($recursive)  { [System.IO.SearchOption]::AllDirectories } else { [System.IO.SearchOption]::TopDirectoryOnly }
	  
	$fileSystemHelper.GetFiles($path, $searchPattern, $searchOption);
}

function Copy-File {
	[CmdletBinding(ConfirmImpact="Medium", SupportsShouldProcess)]
	param(
		[parameter(Position=0, Mandatory)]
		[FolderDiffLib.DiffTools.MyFileInfo] $file,
		[parameter(Position=1, Mandatory)]
	    [ValidateNotNullOrEmpty()]
		[string] $destinationPath,
		[switch] $override,
		[switch] $force
	)
	begin {
		$destinationFullName = Combine-Path $destinationPath, $file.RelativePath
		Write-Verbose ("File '{0}' will be copied to '{1}'" -f $file.File.FullName, $destinationFullName) 
	}
	process {
		if ($PSCmdlet.ShouldProcess("Item: $($file.File.FullName) Destination: $($destinationFullName)", "Copy File")) {
			$fileSystemHelper.CopyFile($file, $destinationPath, $override, $force);
		}
	}
}

function Copy-Files {
	[CmdletBinding(ConfirmImpact="Medium", SupportsShouldProcess)]
	param(
		[parameter(Position=0, Mandatory, ValueFromPipeline)]
		[FolderDiffLib.DiffTools.MyFileInfo[]] $file,
		[parameter(Position=1,Mandatory)]
	    [ValidateNotNullOrEmpty()]
		[string] $destinationPath,
		[switch] $override,
		[switch] $force
	)
	begin {
		Write-Verbose 
	}
	process {
		$fileSystemHelper.CopyFiles($files, $destinationPath, $override, $force);
	
	}


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

