$title = 'New journal entry'
$dateString = "{0:yyyy-MM-dd HH:mm}: " -f (get-date)
$header = $dateString + $title
$underlineText = New-Object -typename System.String '=', $header.Length

$text = [Environment]::NewLine + $dateString + $title
$text += [Environment]::NewLine + $underlineText
$text += [Environment]::NewLine + 'Sharepoint issue: '
$text += [Environment]::NewLine + 'Check-in comment: '
$text += [Environment]::NewLine + 'Files modified'
$text += [Environment]::NewLine + 'New files' + [Environment]::NewLine

$CurrentNoteForm.Helper.InsertText($text)