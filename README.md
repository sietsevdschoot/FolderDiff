# Folder Diff

FolderDiff is a **command line tool** to compare contents of two folders.

####Features:
- Use **simple** or complex **file selection criteria**
- Combine file selectors
- Supports **PowerShell** and Windows **Command Prompt**
- Supports extremely long file- and foldernames up to 32.767 characters (Thanks [Delimon.Win32.IO](https://gallery.technet.microsoft.com/DelimonWin32IO-Library-17cc7893) Library!)
- No recompilation required when search criteria changes
- Results can be presented / projected using PowerShell
	
## Installation

1. Fork the repository or download the zip.
2. Extract the zip.
3. Navigate to FolderDiffPowerShell folder

## Usage

<pre>
Usage: .\FolderDiff.ps1 referenceFolder differenceFolder [supportLongFilenames]

	<b>ReferenceFolder</b>: Full path of reference folder
	<b>DifferenceFolder</b>: Full path of difference folder
	<b><i>[SupportLongFilenames]</i></b>: optional boolean to enable long filenames support (default is $false)
</pre>

Default file selection is to look for files:
- That are newer in the differenceFolder
- Or, files that only exists in the differenceFolder.

##### Usage in PowerShell


```PowerShell
.\FolderDiff.ps1 C:\Backup\PC\Development\ C:\Backup\Laptop\Development\ | %{ $_.FullName } 
````

or

```PowerShell
.\FolderDiff.ps1 C:\Backup\PC\Development\ C:\Backup\Laptop\Development\ $true | %{ $_.FullName }
````

##### Usage in Command Prompt (DOS)

```batch
FolderDiff.bat C:\Backup\PC\Development\ C:\Backup\Laptop\Development\
````

### Selecting files

Selecting files can be done by defining one or more file selectors.
The proces of selecting files is done by evaluating all file selectors and take the distinct set of all results.

The signature of a file selector in PowerShell look this:
```PowerShell 
{ param($diffFile, $referenceFiles) $diffFile.File.Extension -eq '.txt' }
```
A **file selector** is basically a delegate that takes a differenceFile and all referenceFiles and returns a boolean 
to indicate if the differenceFile should be included in the results. 

#### Default file-selection.

The default file selectors are defined in `FolderDiffPowerShell\FolderDiff.ps1` in the function `FolderDiff-Search`.<br />

```PowerShell
	@($folderDiff.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames,
		{ param($diffFile, $referenceFiles)
		
			$refFile = (Get-File $referenceFolder $diffFile.RelativePath)
			$diffFileIsNewer = $diffFile.File.LastWriteTime -gt $refFile.LastWriteTime 
			
			$refFile.Exists -and $diffFileIsNewer 
		},
		{ param($diffFile, $referenceFiles) 
			-Not (File-Exists $referenceFolder $diffFile.RelativePath) 
		}
	))
```

These file selectors could also be rewritten in a single file selector:

```PowerShell
	@($folderDiff.DiffFolder($referenceFolder, $differenceFolder, $supportLongFilesNames,
		{ param($diffFile, $referenceFiles)
		
			$refFile = (Get-File $referenceFolder $diffFile.RelativePath)
			$diffFileIsNewer = $diffFile.File.LastWriteTime -gt $refFile.LastWriteTime 
			
			($refFile.Exists -and $diffFileIsNewer) -or (-not $refFile.Exists)
		}
	))
```

These conditions can be changed if necessary. Feel free to do so.

#### Selectable properties

The files are expressed as a type `MyFileInfo` which wraps `FileInfoBase` together with the Relative path.

If we search in a folder `c:\Backup\` and a file is found in `c:\Backup\Pc\Folder\File.txt` <br/>
then the RelativePath is `Pc\Folder\File.txt`


<pre>
| Name        	| Name              	| Type                                     	|
|---------------|-------------------	|-------------------------------------------|
| <b><i>MyFile</i></b>        | <b><i>MyFile.File</i></b>           |                                           |
| RelativePath 	|                   	| string                                   	|
| File         	|                   	| System.IO.Abstractions.FileInfoBase      	|
|              	| Attributes        	| System.IO.FileAttributes                 	|
|              	| CreationTime      	| datetime                                 	|
|              	| CreationTimeUtc   	| datetime                                 	|
|              	| Directory         	| System.IO.Abstractions.DirectoryInfoBase 	|
|              	| DirectoryName     	| string                                   	|
|              	| Exists            	| bool                                     	|
|              	| Extension         	| string                                   	|
|              	| FullName          	| string                                   	|
|              	| IsReadOnly        	| bool                                     	|
|              	| LastAccessTime    	| datetime                                 	|
|              	| LastAccessTimeUtc 	| datetime                                 	|
|              	| LastWriteTime     	| datetime                                 	|
|              	| LastWriteTimeUtc  	| datetime                                 	|
|              	| Length            	| long                                     	|
|              	| Name              	| string                                   	|
</pre>


Some cmdlets are available to help in selecting files
<pre>
 *   [bool] <b>File-Exists</b> $path $relativePath
 * [MyFile] <b>Get-File</b> $path $relativePath
</pre>


##### Examples:

If we need to get the main folders which contain newer or unique files.<br />
We can write: only list the folders which have 6 subdirectories or less.

```PowerShell
.\FolderDiff.ps1 C:\Backup\PC\Development\ C:\Backup\Laptop\Development\ `
  | %{ $_.Directory.FullName } | Get-Unique | ? { ($_.Split("\")).Length -lt 6 }
````

I will update the examples on request.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## History

20-12-2015 - Initial Readme.md

## Credits

- [Delimon.Win32.IO](https://gallery.technet.microsoft.com/DelimonWin32IO-Library-17cc7893) by Johan Delimon

   It is a great library to overcome the windows file system limitation on folder- and filename length.

- [System.IO.Abstractions](https://github.com/tathamoddie/System.IO.Abstractions) by Tatham Oddie 

   Great and essential Nuget package if you want to write testable code which relies on the FileSystem.<br />
   I use this package for all my IO projects. Make sure to also checkout [System.IO.Abstractions.TestingHelpers](https://www.nuget.org/packages/System.IO.Abstractions.TestingHelpers/)



## License

 
TODO: Write license
