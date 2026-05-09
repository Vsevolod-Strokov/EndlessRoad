using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class StartFromFirstScene
{
    static StartFromFirstScene()
    {
        // Проверяем, есть ли вообще сцены в списке сборки
        if (EditorBuildSettings.scenes.Length > 0)
        {
            // Берем самую первую сцену (индекс 0) из Build Settings
            var scenePath = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

            // Назначаем её стартовой для режима Play в редакторе
            EditorSceneManager.playModeStartScene = sceneAsset;
        }
    }
}
