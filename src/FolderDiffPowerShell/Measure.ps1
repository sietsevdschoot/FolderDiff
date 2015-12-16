function Measure-Time ([scriptblock] $script) 
{
	$sw = [System.Diagnostics.Stopwatch]::StartNew()

	$script.Invoke()

	Write-Host "Executed in" $sw.Elapsed.TotalSeconds
}