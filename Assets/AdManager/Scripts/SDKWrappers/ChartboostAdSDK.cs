using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ChartboostSDK;

namespace AdManager
{
    public class ChartboostAdSDK : IAdSDK 
    {
        public bool IsInitialized { get; set; }
        public AdsSDKs SDK { get; set; }
        public List<string> GameIds { get; set; }
        public String AdsIdForPlatform { get; set; }
        public Platforms CurrentPlatform { get; set; }

        public Action AdFailed { get; set; }
        public Action AdSkipped { get; set; }
        public Action AdFinished { get; set; }

        public void Initialize(AdsSDKs sdk, List<string> gameIds, List<AdIdsPerPlatform> adIdsPerPlatform)
        {
            SDK = sdk;

            if(Chartboost.isInitialized())
            {
                Debug.Log("Chartboost is initialized");
            }
            else
            {
                Chartboost.CreateWithAppId(gameIds[0], gameIds[1]);
            }
            Chartboost.setAutoCacheAds(true);
            CacheAds();
        }

        public void ShowAds(AdType adType, string zone = "")
        {
            switch(adType)
            {
                case AdType.Interstitial:
                    if(Chartboost.hasInterstitial(CBLocation.Default))
                    {
                        Chartboost.showInterstitial(CBLocation.Default);
                    }
                    break;

                case AdType.VideoRewardAd:
                    if(Chartboost.hasRewardedVideo(CBLocation.Default))
                    {
                        Chartboost.didCompleteRewardedVideo += (arg1, arg2) => {
                            if(AdFinished != null)
                            {
                                AdFinished.Invoke();
                            }
                        };
                        Chartboost.showRewardedVideo(CBLocation.Default);
                    }
                    break;
            }
            CacheAds();
        }

        public void HideAds(AdType adType)
        {
        }

        public void BindCallback(Action OnFailed, Action OnSucceeded, Action OnSkipped)
        {
            AdFailed = OnFailed;
            AdFinished = OnSucceeded;
            AdSkipped = OnSkipped;
        }

        //--------------------------OTHER METHODS---------->
        void CacheAds()
        {
            Chartboost.cacheInterstitial(CBLocation.Default);
            Chartboost.cacheRewardedVideo(CBLocation.Default);
        }
    }
    
}
