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




