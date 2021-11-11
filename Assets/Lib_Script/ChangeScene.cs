#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor {

    [MenuItem("Open Scene/Loading #1")]
    public static void OpenLoading()
    {
        OpenScene("Loading");
    }

    [MenuItem("Open Scene/GamePlay #2")]
    public static void OpenGamePlay()
    {
        OpenScene("GamePlay");
    }

    [MenuItem("Open Scene/Demo #2")]
    public static void OpenDemo()
    {
        OpenScene("Linh Scene");
    }
    private static void OpenScene (string sceneName) {
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ()) {
			EditorSceneManager.OpenScene ("Assets/_Linh_Folder/Scenes/" + sceneName + ".unity");
		}
	}
}
#endif