$Text = New-Object System.Text.StringBuilder
$Text.AppendLine('{border="1"}') | Out-Null
$Text.AppendLine('|Name|Type|Size|Range|Byte|Bit Offset|Description|') | Out-Null
$Text.AppendLine('|----|----|----|-----|----|----------|-----------|') | Out-Null
$Text.AppendLine('|    |    |    |     |    |          |           |') | Out-Null
$Text.AppendLine('|    |    |    |     |    |          |           |') | Out-Null
$Text.AppendLine('|    |    |    |     |    |          |           |') | Out-Null
$CurrentNoteForm.Helper.InsertText($Text.ToString())