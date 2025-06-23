using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public static LevelManager _instance;


    public int currentLevel;

    [SerializeField]
    GameObject[] hideInGame;

    [SerializeField] GameObject winPanel, loosePanel, levelCompletedWindow;
    [SerializeField] Text coinsEarnedText;


    [Header("Level Bar")]
    [SerializeField] Image fillImage;
    [SerializeField] Text levelText;
    
    public Transform player;


    private void Awake() {
        _instance = this;
    }

    // Use this for initialization
    void Start() {
        currentLevel = PlayerPrefs.GetInt("CURRENT_LEVEL", 1);
        levelText.text = "LVL " + currentLevel;
        fillImage.fillAmount = 0;

        StartCoroutine(LerpPos(Player._instance.transform, Vector3.up * 1.8f, 0.5f));
        StartCoroutine(LerpSize(Player._instance.transform, Vector3.one * 2f, 0.5f));

        levelCompletedWindow.SetActive(false);
    }

    public void LevelCompleted(bool isWin) {
        if (isWin) {
            currentLevel++;
            PlayerPrefs.SetInt("CURRENT_LEVEL", currentLevel);

            MessageHandler._instance.ShowMessage("Level Completed ;)", 2f, Color.green);
        } else {
            MessageHandler._instance.ShowMessage("Level Failed :(", 2f, Color.red);

            if (BlockSpawner._instance.coroutine != null)
                StopCoroutine(BlockSpawner._instance.coroutine);

            foreach (Block b in BlockSpawner._instance.blocks)
                Destroy(b.gameObject);
        }

        coinsEarnedText.text = "+" + CurrencyManager.GetSuffix((int)CurrencyManager._instance.coinsInLastRound);

        winPanel.SetActive(isWin);
        loosePanel.SetActive(!isWin);

        levelCompletedWindow.SetActive(true);

        fillImage.fillAmount = 0;

        GameManager._instance.isPaused = true;
        Player._instance.transform.localEulerAngles = Vector3.zero;

        ActivateArray(hideInGame, true);

        StartCoroutine(LerpPos(Player._instance.transform, Vector3.up * 1.8f, 0.5f));
        StartCoroutine(LerpSize(Player._instance.transform, Vector3.one * 2f, 0.5f));

        Player._instance.ActivateMenuObjects(true);
        Player._instance.hpText.text = "";

        levelText.text = "LVL " + currentLevel;

    }

    public void SetFill(float f) {
        fillImage.fillAmount = f;
    }

    public void OnClickStartLevel() {
        ActivateArray(hideInGame, false);
        fillImage.fillAmount = 0;

        GameManager._instance.StartGame();

        StartCoroutine(LerpPos(Player._instance.transform, Vector3.zero, 0.5f));
        StartCoroutine(LerpSize(Player._instance.transform, Vector3.one, 0.5f));

        Tutorial._instance.NextTutorial(3);
    }

    void ActivateArray(GameObject[] array, bool b) {
        foreach (GameObject g in array) {
            g.SetActive(b);
        }
    }

    public IEnumerator LerpPos(Transform tr, Vector3 endPos, float time) {
        float t = 0;
        Vector3 startPos = tr.position;

        while (t < time) {
            t += Time.deltaTime;
            tr.position = Vector3.Lerp(startPos, endPos, t / time);

            yield return null;
        }

        tr.position = endPos;
    }

    public IEnumerator LerpSize(Transform tr, Vector3 endScale, float time) {
        float t = 0;
        Vector3 startScale = tr.localScale;

        while (t < time) {
            t += Time.deltaTime;
            tr.localScale = Vector3.Lerp(startScale, endScale, t / time);

            yield return null;
        }

        tr.localScale = endScale;
    }
}
