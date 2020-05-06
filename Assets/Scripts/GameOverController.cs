using System;
using UnityEngine;

namespace TwilightRun
{
    public class GameOverController : SingletonMonoBehaviour<GameOverController>
    {
        [SerializeField] private GameObject _gameOverScreen;

        public event Action GameOverEvent;

        public void GameOver()
        {
            _gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            GameOverEvent?.Invoke();
        }
    }
}
