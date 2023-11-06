using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BannerAd : MonoBehaviour
{
    string bannerAdUnitId = "ccb1c797f386e072"; // Retrieve the ID from your account

    // Start is called before the first frame update
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            
            // AppLovin SDK is initialized, start loading ads
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
            MaxSdk.ShowBanner(bannerAdUnitId);
        };

        MaxSdk.SetSdkKey("p8uUrEw5nMsXpfLit7GWFovDnwfgoOPX8TFVbeu5aMwuKGRV0l1cSZ4AyWzdm_V3mgcTim6ah2ZDZ2NFLZu_Vk");
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }
}