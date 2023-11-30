using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles
{
  [MenuItem("Assets/Build AssetBundles")]
  static void BuildAllAssetBundles()
  {
    if (!Directory.Exists(Application.streamingAssetsPath))
    {
      Directory.CreateDirectory(Application.streamingAssetsPath);
    }
    BuildPipeline.BuildAssetBundles(
        Application.streamingAssetsPath,
        BuildAssetBundleOptions.None,
        EditorUserBuildSettings.activeBuildTarget);
  }
}
