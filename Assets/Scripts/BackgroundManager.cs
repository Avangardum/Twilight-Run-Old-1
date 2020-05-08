using UnityEngine;

namespace TwilightRun
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _backgrounds;

        private void SetBackground(int index)
        {
            _backgrounds.ForEach(x => x.SetActive(false));
            _backgrounds[index].SetActive(true);
        }

        private void Start()
        {
            SetBackground(SaveDataManager.Instance.ActiveBackground);
        }
    } 
}
