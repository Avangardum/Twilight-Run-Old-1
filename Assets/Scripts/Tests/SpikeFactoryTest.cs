using UnityEngine;

namespace TwilightRun.Tests
{
    public class SpikeFactoryTest : MonoBehaviour
    {
        void Start()
        {
            SpikeFactory.Instance.CreateSpike(Spike.SpikeColour.White, FloorOrCeiling.Floor, -3);
            SpikeFactory.Instance.CreateSpike(Spike.SpikeColour.Black, FloorOrCeiling.Ceiling, 0);
            SpikeFactory.Instance.CreateSpike(Spike.SpikeColour.Red, FloorOrCeiling.Floor, 3);
        }
    }
}
