$Text = $CurrentNoteForm.Helper.GetSelectedText()
$Lines = $Text.Split([Environment]::NewLine)
$NewText = ''
foreach ($Line in $Lines) {
	if ($Line.Length -gt 0) {
		if ($NewText.Length -gt 0) { $NewText += [Environment]::NewLine }
		$Prefix = '* '
		if (!$Line.StartsWith($Prefix)) { $NewText += $Prefix }
		$NewText += $Line
	}
}

$CurrentNoteForm.Helper.ReplaceSelectedText($NewText)