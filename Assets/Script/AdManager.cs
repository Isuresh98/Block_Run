using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;

    public string bannerAdUnitId = "your_banner_ad_unit_id";
    public string interstitialAdUnitId = "your_interstitial_ad_unit_id";
    private Swap_Player Playerscript;
    private void Start()
    {
        Playerscript = GameObject.FindGameObjectWithTag("Player").GetComponent<Swap_Player>();

        MobileAds.Initialize(initStatus => { });

        // Request banner ad
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest bannerRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(bannerRequest);

        // Request interstitial ad
        interstitial = new InterstitialAd(interstitialAdUnitId);
        AdRequest interstitialRequest = new AdRequest.Builder().Build();
        interstitial.LoadAd(interstitialRequest);

        HideBannerAd();
    }
    private void Update()
    {
       
        
        if (Playerscript.IntasitialAds == true)
        {
            Time.timeScale = 0;

            if (Time.timeScale == 0)
            {
                ShowInterstitialAd();
                HideBannerAd();
                // Resume the game
                Time.timeScale = 1;
            }
            
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    public void ShowBannerAd()
    {
        bannerView.Show();
    }

    public void HideBannerAd()
    {
        bannerView.Hide();
    }
}
