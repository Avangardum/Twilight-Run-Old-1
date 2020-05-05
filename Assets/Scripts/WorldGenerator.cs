using UnityEngine;
using System;

namespace TwilightRun
{
    public class WorldGenerator : SingletonMonoBehaviour<WorldGenerator>
    {
        [SerializeField] private float _chunkSize;
        [SerializeField] private float _distanceToEdgeForNewChunk;
        [SerializeField] private float _minObstacleGap;
        [SerializeField] private float _maxObstacleGap;
        [SerializeField] private float _firstSpikePosition;
        [SerializeField] private float _blackAndWhiteObstacleSpikeDistance;

        private GameObject _playerLight;
        private ObstacleCreationMethod[] _obstacleCreationMethods;
        private float _printheadPosition;
        private float _playerPositionForNextChunkGeneration;

        private bool RandomBool => UnityEngine.Random.value > 0.5f;
        private float RandomObstacleGap => UnityEngine.Random.Range(_minObstacleGap, _maxObstacleGap);
        private Spike.SpikeColour RandomSpikeColour
        {
            get
            {
                var values = Enum.GetValues(typeof(Spike.SpikeColour));
                var randomValue = ((int[])values)[UnityEngine.Random.Range(0, values.Length)];
                return (Spike.SpikeColour)randomValue;

            }
        }
        private FloorOrCeiling RandomFloorOrCeiling
        {
            get
            {
                var values = Enum.GetValues(typeof(FloorOrCeiling));
                var randomValue = ((int[])values)[UnityEngine.Random.Range(0, values.Length)];
                return (FloorOrCeiling)randomValue;
            }
        }
        private ObstacleCreationMethod RandomObstacleCreationMethod => _obstacleCreationMethods[UnityEngine.Random.Range(0, _obstacleCreationMethods.Length)];

        /// <summary>
        /// Метод, создающий препятствие
        /// </summary>
        /// <param name="xPosition"></param>
        /// <returns>Координата x конца препятствия</returns>
        private delegate float ObstacleCreationMethod(float xPosition);

        private void GenerateChunk()
        {
            float printHeadPositionToStop = _printheadPosition + _chunkSize;
            while(_printheadPosition < printHeadPositionToStop)
            {
                _printheadPosition = RandomObstacleCreationMethod(_printheadPosition);
                _printheadPosition += RandomObstacleGap;
            }
            _playerPositionForNextChunkGeneration = _printheadPosition - _distanceToEdgeForNewChunk;
        }

        /// <summary>
        /// Создаёт черный/белый шип на полу/потолке и шип с противоположными цветом и позицией немного дальше.
        /// Способ прохождения - разместить каждго персонажа на свой цвет
        /// </summary>
        /// <param name="xPosition"></param>
        /// <returns>Координата x конца препятствия</returns>
        private float CreateBlackAndWhiteObstacle(float xPosition)
        {
            bool whiteFirst = RandomBool;
            bool floorFirst = RandomBool;
            SpikeFactory.Instance.CreateSpike(whiteFirst ? Spike.SpikeColour.White : Spike.SpikeColour.Black,
                floorFirst ? FloorOrCeiling.Floor : FloorOrCeiling.Ceiling, xPosition);
            SpikeFactory.Instance.CreateSpike(whiteFirst ? Spike.SpikeColour.Black : Spike.SpikeColour.White,
                floorFirst ? FloorOrCeiling.Ceiling : FloorOrCeiling.Floor, xPosition + _blackAndWhiteObstacleSpikeDistance);
            return xPosition + _blackAndWhiteObstacleSpikeDistance;
        }

        /// <summary>
        /// Создаёт два шипа с одинаковой x координатой и одинаковым случайным цветом на полу и потолке
        /// Способ прохождения - начать свап перед препятсвием, чтобы преодолеть его в воздухе, без контакта с ним
        /// </summary>
        /// <param name="xPosition"></param>
        /// <returns>Координата x конца препятствия</returns>
        private float CreateDoubleSpikeObstacle(float xPosition)
        {
            var colour = RandomSpikeColour;
            SpikeFactory.Instance.CreateSpike(colour, FloorOrCeiling.Floor, xPosition);
            SpikeFactory.Instance.CreateSpike(colour, FloorOrCeiling.Ceiling, xPosition);
            return xPosition;
        }

        /// <summary>
        /// Создаёт одиночный шип со случайными параметрами
        /// </summary>
        /// <param name="xPosition"></param>
        /// <returns>Координата x конца препятствия</returns>
        private float CreateSingleSpikeObstacle(float xPosition)
        {
            SpikeFactory.Instance.CreateSpike(RandomSpikeColour, RandomFloorOrCeiling, xPosition);
            return xPosition;
        }

        protected override void Awake()
        {
            base.Awake();
            _playerLight = GameObject.FindGameObjectWithTag(TagManager.GetTagName(TagManager.Tag.PlayerLight));
            _obstacleCreationMethods = new ObstacleCreationMethod[] { CreateBlackAndWhiteObstacle, CreateDoubleSpikeObstacle, CreateSingleSpikeObstacle };
        }

        private void Start()
        {
            _printheadPosition = _firstSpikePosition;
            GenerateChunk();
        }

        private void Update()
        {
            if (_playerPositionForNextChunkGeneration != 0 && _playerLight.transform.position.x > _playerPositionForNextChunkGeneration)
                GenerateChunk();
        }
    } 
}
