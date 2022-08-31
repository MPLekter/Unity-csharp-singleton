using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


//this AdManager is a singleton. 
//it will exist in every scene. 

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;
    public static AdManager Instance;

    private GameOverHandler gameOverHandler;
#if UNITY_ANDROID
    private string gameId = "4908965";
#endif
#if UNITY_IOS
    private string gameId = "4908964"
#endif

    private void Awake()
    {
        // if this already exists, destroy to make sure there is only one.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        //if this is the first one, set the instance to this and put it in DontDestroyOnLoad.
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.AddListener(this); 
            Advertisement.Initialize(gameId, testMode);

        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        Debug.Log($"AdManager here called by {gameOverHandler.name}");
        this.gameOverHandler = gameOverHandler; //a passed gameOver handler from GameOver object/script becomes the cached gameOver here.
        Advertisement.Show("ContinueAfterCrash"); //you can copy this from your dashboard.unity3d.com
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                gameOverHandler.ContinueGame();
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
       Debug.Log($"UnityAdsReady {placementId}");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError($"UnityAdsError {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"UnityAdsStarted {placementId}");
    }

    
}
