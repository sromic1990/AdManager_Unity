using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdManager;
using System.Reflection;

public class TestScript : MonoBehaviour 
{
    public void ShowAdMobBanner()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.AdMob, AdType.Banner, null, null, null);
    }

    public void HideAdMobBanner()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.HideAdMobAd();
    }

    public void ShowChartBoostRewardAd()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.Chartboost, AdType.VideoRewardAd, null, null, null);
    }

    public void ShowUnityRewardAd()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.UnityAds, AdType.VideoRewardAd, null, null, null);
    }

    public void ShowUnityInterstitial()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.UnityAds, AdType.Interstitial, null, null, null);
    }

    public void ShowAdMobInterstitial()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.AdMob, AdType.Interstitial, null, null, null);
    }

    public void ShowAdMobRewardAd()
    {
        Debug.Log(MethodBase.GetCurrentMethod().Name);
        AdManagerMain.Instance.ShowAds(AdsSDKs.AdMob, AdType.VideoRewardAd, null, null, null);
    }
}

