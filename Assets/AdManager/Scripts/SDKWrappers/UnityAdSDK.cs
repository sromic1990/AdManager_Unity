

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

namespace AdManager
{
    public class UnityAdSDK : IAdSDK
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
            #if UNITY_ANDROID
            CurrentPlatform = Platforms.Android;
            #elif UNITY_IOS
            CurrentPlatform = Platforms.IOS;
            #endif

            SDK = sdk;
            Advertisement.Initialize(gameIds[0], true);
        }

        public void ShowAds(AdType adType, string zone)
        {
            #if UNITY_EDITOR
            //StartCoroutine(WaitForAd());
            #endif
            
            if(string.Equals(zone, ""))
            {
                zone = null;
            }
            
            ShowOptions options = new ShowOptions
            {
                resultCallback = AdCallbackhandler
            };
            if (Advertisement.IsReady(zone))
                Advertisement.Show(zone, options);
        }

        public void HideAds(AdType adType)
        {
            //throw new NotImplementedException;
        }

        public void BindCallback(Action OnFailed, Action OnSucceeded, Action OnSkipped)
        {
            AdFailed = OnFailed;
            AdSkipped = OnSkipped;
            AdFinished = OnSucceeded;
        }

        //--------------------------OTHER METHODS---------->
        void AdCallbackhandler(ShowResult result)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    if(AdFinished != null)
                    {
                        AdFinished.Invoke();
                    }
                    Debug.Log("Ad Finished. Rewarding player...");
                    break;
                case ShowResult.Skipped:
                    if(AdSkipped != null)
                    {
                        AdSkipped.Invoke();
                    }
                    Debug.Log("Ad skipped. Son, I am dissapointed in you");
                    break;
                case ShowResult.Failed:
                    if(AdFailed != null)
                    {
                        AdFailed.Invoke();
                    }
                    Debug.Log("I swear this has never happened to me before");
                    break;
            }

            AdFinished = null;
            AdSkipped = null;
            AdFailed = null;
        }

        IEnumerator WaitForAd()
        {
            float currentTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            yield return null;

            while (Advertisement.isShowing)
                yield return null;

            Time.timeScale = currentTimeScale;
        }
    }
    
}
