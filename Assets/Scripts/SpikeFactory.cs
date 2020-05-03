using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwilightRun
{
    public class SpikeFactory : SingletonMonoBehaviour<SpikeFactory>
    {
        [SerializeField] private GameObject _spikePrefab;
        [SerializeField] private float _onFloorY;
        [SerializeField] private float _onCeilingY;

        internal GameObject CreateSpike(Spike.SpikeColour colour, FloorOrCeiling floorOrCeiling, float xPosition)
        {
            float yPosition = floorOrCeiling == FloorOrCeiling.Floor ? _onFloorY : _onCeilingY;
            float zRotation = floorOrCeiling == FloorOrCeiling.Floor ? 0 : 180;
            GameObject spike = Instantiate(_spikePrefab, new Vector2(xPosition, yPosition), Quaternion.Euler(0, 0, zRotation));
            spike.GetComponent<Spike>().Colour = colour;
            SpriteRenderer spriteRenderer = spike.GetComponent<SpriteRenderer>();
            switch(colour)
            {
                case Spike.SpikeColour.White:
                    spriteRenderer.color = Color.white;
                    break;
                case Spike.SpikeColour.Black:
                    spriteRenderer.color = Color.black;
                    break;
                case Spike.SpikeColour.Red:
                    spriteRenderer.color = Color.red;
                    break;
            }
            return spike;
        }
    }
}
