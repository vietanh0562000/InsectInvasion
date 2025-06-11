using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour {
    public int id;

    [SerializeField] Text lvlText;
    [SerializeField] Text priceText;
    public Image turretImage;
    [SerializeField] Text damageText;

    public GameObject equippedObject;

    public int price;
    public int level;

    public string description;

    public GameObject turretPrefab;
    
    public float baseDamage;

    // Update is called once per frame
    void Update() {

    }

    public void SetUpTurret() {
        level = GetLevel();

        if (level >= 1) {
            //Turret unlocked
            turretImage.color = Color.white;
        } else {
            turretImage.color = new Color (1, 1, 1, 0.5f);
        }

        lvlText.text = "Lvl " + level;
        priceText.text = GetPrice() + "";
        damageText.text = "Damage: " + GetDamage();
    }

    public void OnUpgrade() {
        if (CurrencyManager._instance.coins < GetPrice()) {
            MessageHandler._instance.ShowMessage("Not enough coins", 1f, Color.red);

            return;
        }

        if (id == 0) {
            Tutorial._instance.NextTutorial(1);
        }

        CurrencyManager._instance.AddCoins(-GetPrice());

        PlayerPrefs.SetInt("TURRET" + id, level + 1);

        ShopManager._instance.SetTurrets();
    }

    int GetPrice() {
        return (int)(Mathf.Pow(1.15f, level) * price);
    }

    int GetLevel () {
        return PlayerPrefs.GetInt("TURRET" + id);
    }

    public float GetDamage() {
        return (int)(Mathf.Max(Mathf.Pow(1.15f, Mathf.Max(level - 1, 0)) * baseDamage, baseDamage + level - 1) * (1 + GameManager._instance.extraDamagePercent / 100f) * 100) / 100f;
    }
}
