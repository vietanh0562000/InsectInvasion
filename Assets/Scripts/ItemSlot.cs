using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This script is added to every tile / dice-place on the gamefield
/// </summary>
public class ItemSlot : MonoBehaviour, IDropHandler {

    public float rotation;

    public GameObject item {
        get {
            if (transform.childCount > 0) {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IDropHandler implementation
    public void OnDrop(PointerEventData eventData) {
        if (DragHandler.itemBeingDragged == null) return;

        if (!item) {
            //SLOT IS EMPTY
            PlaceTurret(DragHandler.itemBeingDragged.GetComponent<DragHandler>());

            Tutorial._instance.NextTutorial(2);
        } else {
            //Replace
            RemoveTurret();

            PlaceTurret(DragHandler.itemBeingDragged.GetComponent<DragHandler>());
        }
    }

    void PlaceTurret(DragHandler dr, bool save = true) {
        GameObject g = null;

        if (dr.isUi) {
            if (dr.startParent != null) {
                g = Instantiate(dr.startParent.GetComponent<TurretUI>().turretPrefab);
                g.GetComponent<Turret>().turretUi = dr.startParent.GetComponent<TurretUI>();
            } else {
                g = Instantiate(dr.transform.parent.GetComponent<TurretUI>().turretPrefab);
                g.GetComponent<Turret>().turretUi = dr.transform.parent.GetComponent<TurretUI>();

            }

        } else g = dr.gameObject;

        g.transform.SetParent(transform);
        g.transform.localPosition = Vector3.zero;
        g.transform.localEulerAngles = Vector3.forward * rotation;
        g.transform.localScale = Vector3.one;

        g.GetComponent<Turret>().turretUi.equippedObject.SetActive(true);

        if (save)
            Player._instance.SaveSlots();
    }

    public void RemoveTurret() {
        if (!item) return;

        item.GetComponent<Turret>().turretUi.equippedObject.SetActive(false);

        Destroy(item);

        Player._instance.SaveSlots();
    }
    #endregion

    public void Save() {
        if (transform.childCount < 1)
            PlayerPrefs.SetInt(gameObject.name, -1);
        else {
            PlayerPrefs.SetInt(gameObject.name, item.GetComponent<Turret>().turretUi.id);
        }

    }

    public void Load() {
        int id = PlayerPrefs.GetInt(gameObject.name, -1);
        if (id == -1) return;

        foreach (TurretUI t in ShopManager._instance.turrets) {
            if (t.id == id) {
                PlaceTurret(t.turretImage.GetComponent<DragHandler>(), false);
            }
        }
    }
}