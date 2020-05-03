using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwilightRun
{
    public class SceneLoader : SingletonMonoBehaviour<SceneLoader>
    {
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
            Time.timeScale = 1;
        }

        public void ReloadScene() => LoadScene(SceneManager.GetActiveScene().name);
    } 
}
