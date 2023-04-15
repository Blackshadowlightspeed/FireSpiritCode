using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    void Awake()
    {
        if (Advertisement.isInitialized)
        {
            Debug.Log("Ads Initialized");
            LoadBanner();
        }
        else
        {
            InitializeAds();
        }
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadInterstitialAd()
    {
        Advertisement.Load("Interstitial_Android", this);
        Time.timeScale = 1;
    }

    public void LoadRewardedAd()
    {
        Advertisement.Load("Rewarded_Android", this);
        Time.timeScale = 0;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsLoaded");
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error Showing Ad Unit (placementId): (error.ToString()) - (message)");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsFailure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("UnityAdsStarted");
        Time.timeScale = 0;
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("UnityAdsClicked!");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Show Ad Complete" + showCompletionState);
        if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Debug.Log("Reward Player");
        }
        Time.timeScale = 1;
        Advertisement.Banner.Show("Banner_Android");

    }

    public void LoadBanner()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load("Banner_Android",
            new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = onBannerError
            });
    }

    void OnBannerLoaded()
    {
        Advertisement.Banner.Show("Banner_Android");
    }

    void onBannerError(string message)
    {

    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }
}