param($installPath, $toolsPath, $package, $project)

$contentFiles = "MK6.GameKeeper.Clockwork.exe.config", "quartz_jobs.xml"

Foreach ($contentFile in $contentFiles)
{
	$file = $project.ProjectItems.Item($contentFile)
	$copyToOutput = $file.Properties.Item("CopyToOutputDirectory")
	$copyToOutput.Value = 2
}