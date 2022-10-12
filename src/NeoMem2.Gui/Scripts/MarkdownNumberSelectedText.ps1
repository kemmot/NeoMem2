$Text = $CurrentNoteForm.Helper.GetSelectedText()
$Lines = $Text.Split([Environment]::NewLine)
$NewText = ''
$Counter = 1
foreach ($Line in $Lines) {
	if ($Line.Length -gt 0) {
		if ($NewText.Length -gt 0) { $NewText += [Environment]::NewLine }
		$Prefix = "$Counter. "
		if (!$Line.StartsWith($Prefix)) { $NewText += $Prefix }
		$NewText += $Line
		$Counter++
	}
}

$CurrentNoteForm.Helper.ReplaceSelectedText($NewText)