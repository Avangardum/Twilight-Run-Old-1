using UnityEngine;
using TMPro;

namespace TwilightRun
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private string _highScorePrefix;
        [SerializeField] private float _pointsPerUnitPassed;
        [SerializeField] private float _coinsPerPoint;

        private int _score;
        private bool _newRecord = false;

        public int Score
        {
            get => _score;
            private set 
            {
                _score = value;
                if(Score > HighScore)
                {
                    HighScore = Score;
                    _newRecord = true;
                }
            }
        }
        public int HighScore { get; private set; }

        private void Start()
        {
            GameOverController.Instance.GameOverEvent += OnGameOver;
            HighScore = SaveDataManager.Instance.HighScore;
        }

        private void Update()
        {
            Score = (int)(PlayerMovement.Instance.DistancePassed * _pointsPerUnitPassed);
            _scoreText.text = Score.ToString();
            _highScoreText.text = _highScorePrefix + HighScore;
        }

        private void OnGameOver()
        {
            if (_newRecord)
                SaveDataManager.Instance.HighScore = HighScore;
            SaveDataManager.Instance.Coins += (int)(Score * _coinsPerPoint);
        }
    } 
}
