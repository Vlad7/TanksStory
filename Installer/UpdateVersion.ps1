$VerFromAssemblyInfo = get-content SharedAssemblyInfo.cs
$InstallScript = get-content Install\installscriptMemoNoteUnicode.iss
$VerFromAssemblyInfo[26][27]
$i = 27
$verString = ""
do
	{
	$verString = $verString + $VerFromAssemblyInfo[26][$i]
	$i = $i + 1
	}
while($VerFromAssemblyInfo[26][$i] -ne ')')
$verString
$InstallScript[2]
$InstallScript[2] = "#define Version " + $verString
$InstallScript[2]
$InstallScript | set-content Install\installscriptMemoNoteUnicode.iss