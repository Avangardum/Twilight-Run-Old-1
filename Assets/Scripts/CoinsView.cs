using UnityEngine;
using TMPro;
using TwilightRun;

public class CoinsView : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = SaveDataManager.Instance.Coins.ToString();
    }
}
