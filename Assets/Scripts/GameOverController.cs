using UnityEngine;

namespace TwilightRun
{
    public class GameOverController : SingletonMonoBehaviour<GameOverController>
    {
        [SerializeField] private GameObject _gameOverScreen;

        public void GameOver()
        {
            _gameOverScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
