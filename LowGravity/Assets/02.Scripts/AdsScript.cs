using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsScript : MonoBehaviour {
    static bool isAdsBannerSet = false;
    BannerView banner = null;
    UnityAdsHelper m_AdsHelper;
    int m_PlayNumbering = 0;

    // Use this for initialization
    void Start () {
        m_AdsHelper = GetComponent<UnityAdsHelper>();

        if (!isAdsBannerSet)
            RequestBanner();

    }

    // Update is called once per frame
    void Update () {
		
	}

    private void RequestBanner()

    {

#if UNITY_ANDROID

        string AdUnitID = "";

#else

        string AdUnitID = "unDefind";

#endif

        banner = new BannerView(AdUnitID, AdSize.Banner, AdPosition.Bottom);



        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request =
            new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("C251A04598CF28276A850C491D2B598B").Build();

        banner.LoadAd(request);
        banner.Show();
        isAdsBannerSet = true;

    }

}
