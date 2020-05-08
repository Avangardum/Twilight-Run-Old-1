using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace TwilightRun
{
    public class AdManager : SingletonMonoBehaviour<AdManager>, IUnityAdsListener
    {
        private const float _minTimeBetweenVideoAds = 90;
        private const bool _isTestAd = true;
        private const string _playStoreId = "3592578";
        private const string _videoAd = "video";
        private const string _rewardedVideoAd = "rewardedVideo";

        private static float _timeSinceLastVideoAd = 9999;

        private event Action Reward;

        private void Start()
        {
            Advertisement.AddListener(this);
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

        public void PlayRewardedVideoAd()
        {
            if (!Advertisement.IsReady(_rewardedVideoAd)) return;
            Advertisement.Show(_rewardedVideoAd);
        }

        public void SetReward(Action reward)
        {
            Reward = reward;
        }

        public void OnUnityAdsReady(string placementId)
        {
            
        }

        public void OnUnityAdsDidError(string message)
        {
            
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if(placementId == _rewardedVideoAd && showResult == ShowResult.Finished)
            {
                Reward?.Invoke();
                Reward = null;
            }
        }
    } 
}
