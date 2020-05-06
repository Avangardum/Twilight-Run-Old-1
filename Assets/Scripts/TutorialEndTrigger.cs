using UnityEngine;

namespace TwilightRun
{
    public class TutorialEndTrigger : MonoBehaviour
    {
        private const string _mainMenuName = "Main Menu";

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerDark)) || collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerLight)))
            {
                SaveDataManager.Instance.IsTutorialPassed = true;
                SceneLoader.Instance.LoadScene(_mainMenuName);
            }
        }
    } 
}
