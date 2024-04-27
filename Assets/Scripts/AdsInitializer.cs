using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _gameId;
    bool _testMode = false;

    //Runs as game starts
    private void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_gameId,_testMode, this);
    }

    //to show video ad
    public void LoadInerstitialAd()
    {
        Advertisement.Load("Interstitial_Android", this);
    }

    //to show ads randoomly
    public void ShowAddByChance()
    {
        int random = Random.Range(1, 5);
        Debug.Log(random.ToString());
        if (random == 4)
        {
            Advertisement.Load("Interstitial_Android", this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialization Complete");
        LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error} and {message} ");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Advertisement.Show(placementId, this);
        Debug.Log("OnUnityAdsAdLoaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Unity Ads loading failed: PlacementID {placementId}, {error} and {message} ");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");
    }

    //when ad shows
    public void OnUnityAdsShowStart(string placementId)
    {
        Time.timeScale = 0;
        Debug.Log("OnUnityAdsShowStart");
    }

    //when user clicks on ad
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick");
    }

    //on video ad showing complete
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete");
        Time.timeScale = 1;
        LoadBannerAd();
    }

    //To load banner ad
    public void LoadBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load("Banner_Android",
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            }) ;
    }

    public void OnBannerLoaded()
    {
        Advertisement.Banner.Show("Banner_Android");
    }

    public void OnBannerError(string message)
    {
        Debug.Log(message);
    }

    //to hide banner
    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }
}
