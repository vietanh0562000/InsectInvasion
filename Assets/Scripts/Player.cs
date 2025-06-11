using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public static Player _instance;

    public ItemSlot[] turretSlots;

    Vector2 difference; //for calculating angle

    public SpriteRenderer planetSpriteRenderer;

    public int lives;
    public Text hpText;

    private void Awake() {
        _instance = this;
    }

    // Use this for initialization
    void Start() {
        difference = Vector3.up;
        difference.Normalize();

        hpText.text = "";
    }

    // Update is called once per frame
    void Update() {
        if (GameManager._instance.isPaused) return;

        Rotate();
    }

    public void ActivateMenuObjects(bool b) {
        foreach (ItemSlot iS in turretSlots) {
            iS.GetComponent<Image>().enabled = b;
        }
    }

    //Rotating the shooter object
    void Rotate() {
        if (Input.GetMouseButton(0)) {

            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Block") {
            lives--;

            hpText.text = lives + "";

            if (lives <= 0) {
                LevelManager._instance.LevelCompleted(false);
            }

            collision.GetComponent<Block>().DestroyBlock(true);
        }
    }

    public Turret GetTurretInUsage(int id) {
        foreach (ItemSlot i in turretSlots) {
            if (!i.item) continue;

            if (i.item.GetComponent<Turret>().turretUi.id == id) return i.item.GetComponent<Turret>();
        }

        return null;
    }

    public bool IsTurretInUsage(int id) {
        foreach (ItemSlot i in turretSlots) {
            if (!i.item) continue;

            if (i.item.GetComponent<Turret>().turretUi.id == id) return true;
        }

        return false;
    }

    public void SaveSlots() {
        foreach (ItemSlot i in turretSlots) {
            i.Save();
        }
    }
}