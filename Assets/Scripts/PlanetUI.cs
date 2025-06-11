using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlanetType {
    public enum BonusType { Damage, ExtraLife, AttackSpeed }
    public BonusType bonusType;
    public int value;
}

public class PlanetUI : MonoBehaviour {
    public int id;

    [SerializeField] Text lvlText;
    [SerializeField] Text priceText;
    public Image turretImage;

    public int price;

    public PlanetType planet;

    // Start is called before the first frame update
    void Start() {

    }

    public void SetUpPlanet() {
        if (PlayerPrefs.GetInt("CURRENT_PLANET") == id) {
            SetPlanet();
            priceText.transform.parent.gameObject.SetActive(false);
        } else {
            priceText.transform.parent.gameObject.SetActive(true);
        }

        if (IsBought()) {
            //Planet unlocked
            priceText.text = "Choose";
            priceText.transform.localPosition = Vector3.zero;
            priceText.transform.GetComponentInChildren<Image>().enabled = false;
        } else {
            if (price > 0)
                priceText.text = price + "";
        }
    }

    public void OnClick() {
        if (IsBought()) {
            PlayerPrefs.SetInt("CURRENT_PLANET", id);
            ShopManager._instance.SetPlanets();

            return;
        }


        if (CurrencyManager._instance.coins < price) {
            MessageHandler._instance.ShowMessage("Not enough coins", 1f, Color.red);

            return;
        }

        CurrencyManager._instance.AddCoins(-price);

        PlayerPrefs.SetInt("PLANET_BOUGHT" + id, 1);
        PlayerPrefs.SetInt("CURRENT_PLANET", id);

        ShopManager._instance.SetPlanets();
        ShopManager._instance.SetTurrets();
    }

    void SetPlanet() {
        Player._instance.planetSpriteRenderer.sprite = turretImage.sprite;

        GameManager._instance.ResetBonuses();

        if (planet.bonusType == PlanetType.BonusType.Damage) {
            GameManager._instance.extraDamagePercent = planet.value;
        } else {
            GameManager._instance.extraDamagePercent = 0;
        }

        if (planet.bonusType == PlanetType.BonusType.ExtraLife) {
            GameManager._instance.extraLives = planet.value;
        } else {
            GameManager._instance.extraLives = 0;
        }

        if (planet.bonusType == PlanetType.BonusType.AttackSpeed) {
            GameManager._instance.atkSpeedBonus = planet.value;
        } else {
            GameManager._instance.atkSpeedBonus = 0;
        }
    }

    public bool IsBought() {
        return PlayerPrefs.HasKey("PLANET_BOUGHT" + id) || price == 0;
    }
}
