using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TwilightRun
{
    public class GarbageCollector : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _distanceFromPlayerForDeletion;
        [SerializeField] private int _clearEveryXFrames;

        private void Clear()
        {
            ClearSpikes();
        }

        private void ClearSpikes()
        {
            FindObjectsOfType<Spike>()
                .Select(x => x.gameObject)
                .Where(x => x.transform.position.x + _distanceFromPlayerForDeletion < _player.position.x)
                .ForEach(Destroy);
        }

        private void Update()
        {
            if (Time.frameCount % _clearEveryXFrames == 0)
                Clear();
        }
    } 
}
