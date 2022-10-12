$Text = New-Object System.Text.StringBuilder
$Text.AppendLine('{border="1"}') | Out-Null
$Text.AppendLine('|Left Align|Centre Align|Right Align|') | Out-Null
$Text.AppendLine('|---------:|:----------:|:----------|') | Out-Null
$Text.AppendLine('|Row 1a    |Row 1b      |Row 1c     |') | Out-Null
$Text.AppendLine('|Row 2a    |Row 2b      |Row 2c     |') | Out-Null
$CurrentNoteForm.Helper.InsertText($Text.ToString())