using UnityEngine;

namespace TwilightRun
{
    public class TutorialLauncher : MonoBehaviour
    {
        [SerializeField] private string _tutorialSceneName;

        private void Start()
        {
            if (!SaveDataManager.Instance.TutorialPassed)
                SceneLoader.Instance.LoadScene(_tutorialSceneName);
        }
    } 
}
