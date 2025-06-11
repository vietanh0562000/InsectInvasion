using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    public TurretUI turretUi;
    public Shooter[] shooters;

    // Start is called before the first frame update
    void Start() {

    }


    float timer = 0;
    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;

        if (timer > 1) {
            timer = 0;
            GetStat();
        }
    }

    void GetStat() {
        foreach (Shooter s in shooters) {
            s.damage = turretUi.GetDamage();
        }
    }
}
