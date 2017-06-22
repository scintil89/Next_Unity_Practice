using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    void Update()
    {
        // Rotate the object around its local Y axis at 1 degree per second
        transform.Rotate(Vector3.right * Time.deltaTime * 50);

        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
    }

    void OnGUI()
    {
        Rect rect = new Rect(10, 10, Screen.width * 0.2f, Screen.height * 0.1f);
        if( GUI.Button(rect, "Request Banner"))
        {
            GoogleMobileAdsManager.instance.RequestSmartBanner();
        }

        rect.x += (rect.width + 20);
        if (GUI.Button(rect, "Show Banner"))
        {
            GoogleMobileAdsManager.instance.ShowSmartBanner();
        }

        rect.x += (rect.width + 20);
        if (GUI.Button(rect, "Hide Banner"))
        {
            GoogleMobileAdsManager.instance.HideSmartBanner();
        }

        rect.x = 10;
        rect.y += (rect.height + 20);
        if (GUI.Button(rect, "Request Interstitial"))
        {
            GoogleMobileAdsManager.instance.RequestInterstitial();
        }

        if (GoogleMobileAdsManager.instance.IsReadyInterstial())
        {
            rect.x += (rect.width + 20);
            if (GUI.Button(rect, "Show Interstitial"))
            {
                GoogleMobileAdsManager.instance.ShowInterstitial();
            }
        }
        
        rect.x = 10;
        rect.y += (rect.height + 20);
        if (GUI.Button(rect, "Request Rewarded Video"))
        {
            GoogleMobileAdsManager.instance.RequestRewardVideo();
        }

        if (GoogleMobileAdsManager.instance.IsReadyRewardVideo())
        {
            rect.x += (rect.width + 20);
            if (GUI.Button(rect, "Show Rewarded Video"))
            {
                GoogleMobileAdsManager.instance.ShowRewardVideo();
            }
        }
    }
}
