using UnityEngine;
using System;

namespace TwilightRun
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private float _chunkSize;
        [SerializeField] private float _distanceToEdgeForNewChunk;
        [SerializeField] private float _minSpikeGap;
        [SerializeField] private float _maxSpikeGap;
        [SerializeField] private float _firstSpikePosition;

        private GameObject _playerLight;
        private float _printheadPosition;
        private float _playerPositionForNextChunkGeneration;

        private float _randomSpikeGap => UnityEngine.Random.Range(_minSpikeGap, _maxSpikeGap);
        private Spike.SpikeColour _randomSpikeColour
        {
            get
            {
                var values = Enum.GetValues(typeof(Spike.SpikeColour));
                var randomValue = ((int[])values)[UnityEngine.Random.Range(0, values.Length)];
                return (Spike.SpikeColour)randomValue;

            }
        }
        private FloorOrCeiling _randomFloorOrCeiling
        {
            get
            {
                var values = Enum.GetValues(typeof(FloorOrCeiling));
                var randomValue = ((int[])values)[UnityEngine.Random.Range(0, values.Length)];
                return (FloorOrCeiling)randomValue;
            }
        }

        private void GenerateChunk()
        {
            float printHeadPositionToStop = _printheadPosition + _chunkSize;
            while(_printheadPosition < printHeadPositionToStop)
            {
                SpikeFactory.Instance.CreateSpike(_randomSpikeColour, _randomFloorOrCeiling, _printheadPosition);
                _printheadPosition += _randomSpikeGap;
            }
            _playerPositionForNextChunkGeneration = _printheadPosition - _distanceToEdgeForNewChunk;
        }

        private void Awake()
        {
            _playerLight = GameObject.FindGameObjectWithTag(TagManager.GetTagName(TagManager.Tag.PlayerLight));
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
