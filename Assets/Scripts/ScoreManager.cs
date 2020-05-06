using UnityEngine;
using TMPro;

namespace TwilightRun
{
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private string _highScorePrefix;
        [SerializeField] private float _pointsForUnitPassed;

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
            GameOverController.Instance.GameOverEvent += UpdateHighScore;
            HighScore = SaveDataManager.Instance.HighScore;
        }

        private void Update()
        {
            Score = (int)(PlayerMovement.Instance.DistancePassed * _pointsForUnitPassed);
            _scoreText.text = Score.ToString();
            _highScoreText.text = _highScorePrefix + HighScore;
        }

        private void UpdateHighScore()
        {
            if (!_newRecord)
                return;
            SaveDataManager.Instance.HighScore = HighScore;
        }
    } 
}
