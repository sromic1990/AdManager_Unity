

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AdManager
{
    public class AdManagerMain : MonoBehaviour 
    {
        public static AdManagerMain Instance;

        public List<IAdSDK> adSDKs;
        public List<AdsSDKs> AdSdksToInitialize;

        void Start()
        {
            Instance = this;
            adSDKs = new List<IAdSDK>();
            InitializeAdSDKs();
        }

        private void InitializeAdSDKs()
        {
            for (int i = 0; i < AdSdksToInitialize.Count; i++)
            {
                switch (AdSdksToInitialize[i])
                {
                    case AdsSDKs.UnityAds:
                        IAdSDK iad1 = new UnityAdSDK();
                        List<string> gameIds = new List<string>();
                        gameIds.Add("1639111");
                        iad1.Initialize(AdsSDKs.UnityAds, gameIds, new List<AdIdsPerPlatform>());
                        adSDKs.Add(iad1);
                        break;

                    case AdsSDKs.AdMob:
                        IAdSDK iad2 = new AdMobAdSDK();

                        List<AdIdsPerPlatform> adIdsPerPlatform = new List<AdIdsPerPlatform>();

                        AdIdsPerPlatform a0 = new AdIdsPerPlatform();
                        a0._Platform = Platforms.Android;
                        a0._AdType = AdType.AppId;
                        a0._AdID = "ca-app-pub-4775990796577347~3828110784";
                        adIdsPerPlatform.Add(a0);

                        AdIdsPerPlatform a1 = new AdIdsPerPlatform();
                        a1._Platform = Platforms.Android;
                        a1._AdType = AdType.Banner;
                        a1._AdID = "ca-app-pub-4775990796577347/9486921137";
                        adIdsPerPlatform.Add(a1);

                        AdIdsPerPlatform a2 = new AdIdsPerPlatform();
                        a2._Platform = Platforms.Android;
                        a2._AdType = AdType.VideoRewardAd;
                        a2._AdID = "ca-app-pub-4775990796577347/7913204768";
                        adIdsPerPlatform.Add(a2);

                        AdIdsPerPlatform a3 = new AdIdsPerPlatform();
                        a3._Platform = Platforms.Android;
                        a3._AdType = AdType.Interstitial;
                        a3._AdID = "ca-app-pub-4775990796577347/7982267776";
                        adIdsPerPlatform.Add(a3);

                        iad2.Initialize(AdsSDKs.AdMob, new List<string>(), adIdsPerPlatform);
                        adSDKs.Add(iad2);

                        break;

                    case AdsSDKs.Chartboost:
                        IAdSDK iad3 = new ChartboostAdSDK();
                        List<string> appIds = new List<string>();
                        appIds.Add("503b599816ba474c7500002d");
                        appIds.Add("77ad25547a27fb955007d7c442acc9ea746f8236");
                        iad3.Initialize(AdsSDKs.Chartboost, appIds, new List<AdIdsPerPlatform>());
                        adSDKs.Add(iad3);
                        break;

                    case AdsSDKs.AdColony:
                        break;
                }
            }
        }
         
        public void ShowAds(AdsSDKs sdk, AdType type, Action OnFailed, Action OnSucceeded, Action OnSkipped)
        {
            for (int i = 0; i < adSDKs.Count; i++)
            {
                if(adSDKs[i].SDK == sdk)
                {
                    adSDKs[i].ShowAds(type);
                    adSDKs[i].BindCallback(OnFailed, OnSucceeded, OnSkipped);
                    break;
                }
            }
        }

        public void HideAdMobAd()
        {
            for (int i = 0; i < adSDKs.Count; i++)
            {
                if(adSDKs[i].SDK.Equals(AdsSDKs.AdMob))
                {
                    adSDKs[i].HideAds(AdType.Banner);
                    break;
                }
            }
        }

    }
    
}
