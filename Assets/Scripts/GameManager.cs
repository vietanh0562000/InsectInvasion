using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;

    public bool isPaused;

    public Transform dragParent;

    public GameObject[] destroyEffect;

    [Space(12)]
    [Header("Bonuses")]

    public int extraDamagePercent;
    public int extraLives;
    public float atkSpeedBonus;

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void StartGame() {
        Player._instance.lives = 1 + extraLives;
        Player._instance.hpText.text = (1 + extraLives) + "";

        CurrencyManager._instance.coinsInLastRound = 0;

        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        BlockSpawner._instance.StartWave();
        isPaused = false;

        Player._instance.ActivateMenuObjects(false);

        yield return new WaitForSeconds(2f);
    }

    public void ResetBonuses() {
        extraDamagePercent = 0;
        extraLives = 0;
    }
}
