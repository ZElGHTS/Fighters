#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class PathBuilder : IPreprocessBuildWithReport
{
    public int callbackOrder
    {
        get => 0;
    }
    
    public void OnPreprocessBuild(BuildReport report)
    {
        MasterManager.PopulateNetworkedPrefabs();
    }
}
#endif
