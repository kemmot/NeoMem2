if ($CurrentNoteForm.RtxtNoteText.WordWrap) {
	[System.Windows.Forms.MessageBox]::Show("This script does not work with word wrap enabled")
} else {
	$lineNumber = $CurrentNoteForm.RtxtNoteText.GetLineFromCharIndex($CurrentNoteForm.RtxtNoteText.SelectionStart)
	$lineText = $CurrentNoteForm.RtxtNoteText.Lines[$lineNumber]
	$underlineText = New-Object -typename System.String '-', $lineText.Length
	$newPosition = $CurrentNoteForm.RtxtNoteText.GetFirstCharIndexOfCurrentLine() + $lineText.Length
	$CurrentNoteForm.Helper.PushSelection($newPosition, 0)
	$CurrentNoteForm.Helper.InsertText([Environment]::NewLine + $underlineText)
	$CurrentNoteForm.Helper.PopSelection()
}