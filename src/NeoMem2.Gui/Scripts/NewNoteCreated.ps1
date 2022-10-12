# put in this file any actions to execute when a new note is created
# the following variables are available:
#	$CurrentNoteForm
#	$MainForm
#	$Note

$OriginalName = $Note.Name
$NewNamePrefix = "{0:yyyy-MM-dd HH:mm}: " -f (get-date)

if ([string]::IsNullOrEmpty($OriginalName)) {
	$NewName = 'New Note'
} else {
	$NewName = $OriginalName
}

$Note.Name = $NewNamePrefix + $NewName
$CurrentNoteForm.TxtName.SelectionStart = $NewNamePrefix.Length
$CurrentNoteForm.TxtName.SelectionLength = $NewName.Length

$Note.TextFormat = 'Markdown'
$CurrentNoteForm.Switch()
