using Microsoft.Build.Framework;

namespace TailwindCSS;

internal class TestTask : Microsoft.Build.Utilities.Task, ICancelableTask
{
    private bool canceled = false;

    public string? Arguments { get; set; }

    public string? In { get; set; }

    public string? Out { get; set; }

    public void Cancel()
    {
        canceled = true;
    }

    public override bool Execute()
    {
        var projectFile = BuildEngine.ProjectFileOfTaskNode;

        Log.LogMessage(MessageImportance.High, $"TailwindCSS ProjectFIle: {projectFile}");
        Log.LogMessage(MessageImportance.High, $"TailwindCSS In: {In}");
        Log.LogMessage(MessageImportance.High, $"TailwindCSS Out: {Out}");
        Log.LogMessage(MessageImportance.High, $"TailwindCSS Canceled: {canceled}");

        return true;
    }
}
