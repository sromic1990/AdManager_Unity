

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

namespace AdManager
{
    public class AdMobAdSDK : IAdSDK
    {
        public bool IsInitialized { get; set; }
        public AdsSDKs SDK { get; set; }
        public List<string> GameIds { get; set; }
        public String AdsIdForPlatform { get; set; }
        public Platforms CurrentPlatform { get; set; }

        public Action AdFailed { get; set; }
        public Action AdSkipped { get; set; }
        public Action AdFinished { get; set; }

        BannerView bannerView;
        InterstitialAd interstitial;

        public void Initialize(AdsSDKs sdk, List<string> gameIds, List<AdIdsPerPlatform> adIdsPerPlatform)
        {
            #if UNITY_ANDROID
            CurrentPlatform = Platforms.Android;
#elif UNITY_IOS
            CurrentPlatform = Platforms.IOS;
#endif

            SDK = sdk;

            MobileAds.Initialize(ReturnAdID(adIdsPerPlatform, CurrentPlatform, AdType.AppId));

            RequestBannerAd(adIdsPerPlatform);
            RequestInterstitialAd(adIdsPerPlatform);
            RequestRewardAd(adIdsPerPlatform);

        }

        public void ShowAds(AdType adType, string zone = "")
        {
            switch(adType)
            {
                case AdType.Banner:
                    bannerView.Show();
                    break;

                case AdType.Interstitial:
                    if(interstitial.IsLoaded())
                    {
                        interstitial.Show();
                    }
                    break;

                case AdType.VideoRewardAd:
                    if(RewardBasedVideoAd.Instance.IsLoaded())
                    {
                        RewardBasedVideoAd.Instance.Show();
                    }
                    break;
            }
        }

        public void HideAds(AdType adType)
        {
            //Since no other type of ads can be hidden, only banner view ads are getting hidden.
            bannerView.Hide();
        }

        public void BindCallback(Action OnFailed, Action OnSucceeded, Action OnSkipped)
        {
            AdFailed = OnFailed;
            AdFinished = OnSucceeded;
            AdSkipped = OnSkipped;
        }

        //--------------------------OTHER METHODS---------->
        void RequestBannerAd(List<AdIdsPerPlatform> adIdsPerPlatform)
        {
            string adUnitId = ReturnAdID(adIdsPerPlatform, CurrentPlatform, AdType.Banner);

            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
            AdRequest request = new AdRequest.Builder().Build();
            bannerView.LoadAd(request);
            //bannerView.OnAdLoaded += (sender, e) => 
            //{
            //    Debug.Log("Showing admob");
            //    //HideAds(AdType.Banner);
            //};
        }

        void RequestInterstitialAd(List<AdIdsPerPlatform> adIdsPerPlatform)
        {
            string adUnitId = ReturnAdID(adIdsPerPlatform, CurrentPlatform, AdType.Interstitial);

            interstitial = new InterstitialAd(adUnitId);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += (object sender, EventArgs e) =>
            {
                RequestInterstitialAd(adIdsPerPlatform);
            };

        }

        void RequestRewardAd(List<AdIdsPerPlatform> adIdsPerPlatform)
        {
            string adUnitId = ReturnAdID(adIdsPerPlatform, CurrentPlatform, AdType.VideoRewardAd);

            Debug.Log(adUnitId);

            RewardBasedVideoAd.Instance.LoadAd(new AdRequest.Builder().Build(), adUnitId);

            RewardBasedVideoAd.Instance.OnAdClosed += (object sender, EventArgs e) =>
            {
                RewardBasedVideoAd.Instance.LoadAd(new AdRequest.Builder().Build(), adUnitId);
            };

            RewardBasedVideoAd.Instance.OnAdRewarded += (object sender, Reward e) =>
            {
                if(AdFinished != null)
                {
                    AdFinished.Invoke();
                }
            };

        }

        private string ReturnAdID(List<AdIdsPerPlatform> adIdsPerPlatform, Platforms platform, AdType adtype)
        {
            string id = "";

            for (int i = 0; i < adIdsPerPlatform.Count; i++)
            {
                if(adIdsPerPlatform[i]._Platform.Equals(platform))
                {
                    if(adIdsPerPlatform[i]._AdType.Equals(adtype))
                    {
                        id = adIdsPerPlatform[i]._AdID;
                    }
                }
            }

            return id;
        }

    }
    
}
