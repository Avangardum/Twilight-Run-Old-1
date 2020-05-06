using UnityEngine;

namespace TwilightRun
{
    public class Spike : MonoBehaviour
    {
        public enum SpikeColour
        {
            White,
            Black,
            Red
        }

        public SpikeColour Colour;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerLight)))
            {
                if (Colour == SpikeColour.Black || Colour == SpikeColour.Red)
                    KillPlayer();
            }
            else if (collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerDark)))
            {
                if (Colour == SpikeColour.White || Colour == SpikeColour.Red)
                    KillPlayer();
            }
        }

        private void KillPlayer()
        {
            GameOverController.Instance?.GameOver();
            TutorialGameOverController.Instance?.GameOver();
        }
    } 
}
