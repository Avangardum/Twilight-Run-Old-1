using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TwilightRun
{
    public class ScoreAndGoldManager : SingletonMonoBehaviour<ScoreAndGoldManager>
    {
        [SerializeField] private Button _doubleCoinsButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private TextMeshProUGUI _gameOverScoreText;
        [SerializeField] private TextMeshProUGUI _gameOverHighScoreText;
        [SerializeField] private TextMeshProUGUI _gameOverCoinsGainText;
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
        public int CoinGain => (int)(Score * _coinsPerPoint);

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
            _coinsText.text = SaveDataManager.Instance.Coins.ToString();
        }

        private void OnGameOver()
        {
            if (_newRecord)
                SaveDataManager.Instance.HighScore = HighScore;
            SaveDataManager.Instance.Coins += CoinGain;
            _gameOverScoreText.text = Score.ToString();
            _gameOverHighScoreText.text = HighScore.ToString();
            _gameOverCoinsGainText.text = "+ " + CoinGain;
        }

        public void TryDoubleGoldGain()
        {
            AdManager.Instance.SetReward(DoubleGoldGain);
            AdManager.Instance.PlayRewardedVideoAd();
        }

        private void DoubleGoldGain()
        {
            SaveDataManager.Instance.Coins += CoinGain;
            _gameOverCoinsGainText.text = "+ " + CoinGain * 2;
            _doubleCoinsButton.interactable = false;
        }
    } 
}
