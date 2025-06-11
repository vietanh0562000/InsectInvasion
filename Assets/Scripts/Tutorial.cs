using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public static Tutorial _instance;

    public GameObject t1, t2, t3;

    public int tutorialNum;

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        tutorialNum = PlayerPrefs.GetInt("TUTORIAL", 0);

        if (tutorialNum != 0)
            gameObject.SetActive(false);
        else {
            NextTutorial(0);

            PlayerPrefs.SetInt("TUTORIAL", 1);
        }
    }

    public void NextTutorial(int id) {
        if (tutorialNum != id) return;

        if (id == 0) {
            t1.SetActive(true);
        } else if (id == 1) {
            t1.SetActive(false);
            t2.SetActive(true);
        } else if (id == 2) {
            t2.SetActive(false);
            t3.SetActive(true);
        } else if (id == 3) {
            gameObject.SetActive(false);
        }

        tutorialNum++;
    }
}
