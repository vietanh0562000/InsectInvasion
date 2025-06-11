using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ADs : MonoBehaviour {

    public static ADs _instance; //instance for easier usage

    public float timeToshowInterstitial = 300;
    float adTimer = 0; //Displaying interstitial ads

    private void Awake() {
        _instance = this;
    }

    // Use this for initialization
    void Start() {
#if UNITY_IOS
        Advertisement.Initialize("3446382");
#elif UNITY_ANDROID
        Advertisement.Initialize("3446383");
#endif
    }

    // Update is called once per frame
    void Update() {
        adTimer += Time.deltaTime;

        if (adTimer > timeToshowInterstitial) {
            ShowInterstitial();
            adTimer = 0;
        }
    }


    /// <summary>
    /// Showing interstitial AD and sending unity custom event
    /// </summary>
    public void ShowInterstitial() {
        
    }

    public void ShowRewardedGetMoney() {
        
    }


    /// <summary>
    /// Callback after viewing an AD
    /// </summary>
    /// <param name="result"></param>
    private void HandleShowResultAddMoney(ShowResult result) {
        if (result == ShowResult.Finished) {
            Debug.Log("Video completed - Offer a reward to the player");

            CurrencyManager._instance.AddCoins(1000);
            MessageHandler._instance.ShowMessage("Reward Collected", 1f, Color.green);
        }
    }

    public int planetToUnlock = -1;

    public void ShowRewardedGetPlanet1(int id) {
        PlanetUI p = ShopManager._instance.GetPlanetById(id);
        if (p.IsBought()) {
            p.OnClick();
            return;
        }

        planetToUnlock = id;
    }

    private void HandlePlanet(ShowResult result) {
        if (result == ShowResult.Finished) {
            Debug.Log("Video completed - Offer a reward to the player");

            PlayerPrefs.SetInt("PLANET_BOUGHT" + planetToUnlock, 1);

            ShopManager._instance.SetPlanets();

            MessageHandler._instance.ShowMessage("Reward Collected", 1f, Color.green);

        }
    }

    public void ShowRewardedGetMoneyAfterGame() {
 
    }


    /// <summary>
    /// Callback after viewing an AD
    /// </summary>
    /// <param name="result"></param>
    private void HandleShowResultGame(ShowResult result) {
        if (result == ShowResult.Finished) {
            Debug.Log("Video completed - Offer a reward to the player");

            CurrencyManager._instance.AddCoins(CurrencyManager._instance.coinsInLastRound);
            MessageHandler._instance.ShowMessage("Reward Collected", 1f, Color.green);
        }
    }
}