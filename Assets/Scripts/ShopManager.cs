using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
    public static ShopManager _instance;

    public TurretUI[] turrets;
    public PlanetUI[] planets;

    private void Awake() {
        _instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start() {
        yield return new WaitForEndOfFrame();

        SetPlanets();

        SetTurrets();

        yield return new WaitForSeconds(0.15f);

        foreach (ItemSlot i in Player._instance.turretSlots) {
            i.Load();
        }
    }

    public void SetTurrets() {
        foreach (TurretUI t in turrets) {
            t.SetUpTurret();
        }
    }

    public void SetPlanets() {
        foreach (PlanetUI p in planets) {
            p.SetUpPlanet();
        }
    }

    public PlanetUI GetPlanetById(int id) {
        foreach (PlanetUI p in planets) {
            if (p.id == id)
                return p;
        }

        return null;
    }
}
