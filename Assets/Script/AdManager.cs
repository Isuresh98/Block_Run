using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    // Declare banner and interstitial ad objects
    private BannerView bannerView;
    private InterstitialAd interstitial;

    // Declare ad unit IDs for banner and interstitial ads
    public string bannerAdUnitId = "your_banner_ad_unit_id";
    public string interstitialAdUnitId = "your_interstitial_ad_unit_id";

    // Declare variables for banner ad interval and time since banner ad shown
    public float bannerInterval = 10f; // interval in seconds
    [SerializeField]
    private float timeSinceBannerShown = 0f;
    bool bannerShown = false;

    // Declare variable for player script
    private Swap_Player playerScript;

    private void Start()
    {
        // Get player script
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Swap_Player>();

        // Initialize mobile ads
        MobileAds.Initialize(initStatus => { });

        // Request banner ad
        bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest bannerRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(bannerRequest);

        // Request interstitial ad
        interstitial = new InterstitialAd(interstitialAdUnitId);
        AdRequest interstitialRequest = new AdRequest.Builder().Build();
        interstitial.LoadAd(interstitialRequest);

        // Hide banner ad initially
        HideBannerAd();
    }

    private void Update()
    {
        // Update time since banner ad shown
        timeSinceBannerShown += Time.deltaTime;

        // If banner ad has not been shown and interval has passed, show banner ad
        if (!bannerShown && timeSinceBannerShown >= bannerInterval)
        {
            ShowBannerAd();
            bannerShown = true;
        }

        // If player needs to see an interstitial ad, pause game and show ad
        if (playerScript.IntasitialAds == true)
        {
            // Pause game
            Time.timeScale = 0;

            // If game is paused, show interstitial ad, hide banner ad, and resume game
            if (Time.timeScale == 0)
            {
                ShowInterstitialAd();
                HideBannerAd();
                Time.timeScale = 1; // Resume the game
            }
        }
    }

    // Show interstitial ad if it is loaded
    public void ShowInterstitialAd()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    // Show banner ad and reset time since banner shown
    public void ShowBannerAd()
    {
        bannerView.Show();
        timeSinceBannerShown = 0f;
        bannerShown = false;
    }

    // Hide banner ad
    public void HideBannerAd()
    {
        bannerView.Hide();
    }
}
