using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Managing the dice dragging
/// </summary>
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    Vector3 startPosition;

    [HideInInspector]
    public Transform startParent;

    public bool isUi;
    bool disableDrag;
    TurretUI turretUi;

    private void Start() {
        if (isUi) {
            turretUi = transform.parent.GetComponent<TurretUI>();
        }
    }

    #region IBeginDragHandler implementation

    public void OnBeginDrag(PointerEventData eventData) {
        if (!GameManager._instance.isPaused) return;

        startParent = transform.parent;

        if (!isUi)
            startParent.GetComponent<Image>().color = Color.white;
        else {
            if (Player._instance.IsTurretInUsage(turretUi.id) || turretUi.level < 1) {
                disableDrag = true;
                return;
            } else {
                disableDrag = false;
            }
        }

        itemBeingDragged = gameObject;
        startPosition = transform.position;

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.parent = GameManager._instance.dragParent;


    }

    #endregion

    #region IDragHandler implementation

    public void OnDrag(PointerEventData eventData) {
        if (!GameManager._instance.isPaused || disableDrag) return;

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += Vector3.back * transform.position.z;
    }

    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventData) {
        if (!GameManager._instance.isPaused || disableDrag) return;

        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == GameManager._instance.dragParent) {
            transform.SetParent(startParent);
            transform.position = startPosition;

            if (!isUi) {
                startParent.GetComponent<ItemSlot>().RemoveTurret();
            } else {
                transform.SetAsFirstSibling();
            }
        }
    }

    #endregion
}