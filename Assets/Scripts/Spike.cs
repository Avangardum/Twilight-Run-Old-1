using UnityEngine;

public class Spike : MonoBehaviour
{
    public enum SpikeColour
    {
        White,
        Black,
        Red
    }

    [SerializeField] private SpikeColour _colour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerLight)))
        {
            if (_colour == SpikeColour.Black || _colour == SpikeColour.Red)
                KillPlayer();
        }
        else if(collision.CompareTag(TagManager.GetTagName(TagManager.Tag.PlayerDark)))
        {
            if (_colour == SpikeColour.White || _colour == SpikeColour.Red)
                KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("KILL");
    }
}
