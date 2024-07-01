using UnityEngine.SceneManagement;

namespace Services.SceneLoading
{
    public class SceneLoadingManager
    {
        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }
}