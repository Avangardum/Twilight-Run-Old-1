using UnityEngine;
using UnityEngine.Advertisements;

namespace TwilightRun
{
    public class AdManager : SingletonMonoBehaviour<AdManager>
    {
        private const float _minTimeBetweenVideoAds = 90;
        private const bool _isTestAd = true;

        private static float _timeSinceLastVideoAd = 9999;

        private string _playStoreId = "3592578";
        private string _videoAd = "video";

        public void ShowAd()
        {

        }

        private void Start()
        {
            InitializeAdvertisement();
        }

        private void Update()
        {
            _timeSinceLastVideoAd += Time.unscaledDeltaTime;
        }

        private void InitializeAdvertisement()
        {
            Advertisement.Initialize(_playStoreId, _isTestAd);
        }

        public void PlayVideoAd()
        {
            if (!Advertisement.IsReady(_videoAd)) return;
            if (_timeSinceLastVideoAd < _minTimeBetweenVideoAds) return;
            Advertisement.Show(_videoAd);
            _timeSinceLastVideoAd = 0;
        }
    } 
}
