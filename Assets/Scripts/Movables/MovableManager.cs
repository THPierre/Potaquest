using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableManager : MonoBehaviour
{
    private RaycastHit fingerRaycastHit;
    private List<GameObject> movablesObj = new List<GameObject>();
    private GridManager gridMgr;
    [HideInInspector]
    public Player player;
    private void Start() {
        MovablesListInit();
        IsolatePlayer();
        PlayerInit(player);
    }
    private void MovablesListInit() {
        movablesObj.AddRange(GameObject.FindGameObjectsWithTag("Movable"));
        movablesObj.AddRange(GameObject.FindGameObjectsWithTag("MovableGrav"));
        movablesObj.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }
    private void IsolatePlayer() {
        for (int i = 0; i < movablesObj.Count; i++)
            if (movablesObj[i].CompareTag("Player"))
                player = movablesObj[i].GetComponent<Player>();
    }
    private void PlayerInit(Player player) {
        for (int i = 0; i < movablesObj.Count; i++)
            movablesObj[i].GetComponent<MovableSelector>().PlayerInit(player);
    }
    public void GridInit(GridManager gridMgr) {
        this.gridMgr = gridMgr;
        for (int i = 0; i < movablesObj.Count; i++)
            movablesObj[i].GetComponent<MovableSelector>().GridInit(gridMgr);
    }
    private bool FingerRaycast(Vector3 castStart, Vector3 castDir, float lenght) {
        castStart.z = GameObject.Find("Main Camera").transform.position.z;
        return Physics.Raycast(castStart, castDir, out fingerRaycastHit, lenght);
    }
    public void SelectCastedObject(Vector3 fingerPos) {
        if (FingerRaycast(fingerPos, Vector3.forward, 10))
            HighlightCastedObject(fingerRaycastHit.collider.gameObject);
    }
    public void DeselectObjects(Vector3 fingerPos) {
        if (FingerRaycast(fingerPos, Vector3.forward,10))
            if (fingerRaycastHit.collider.gameObject.CompareTag("Static"))
                DisableAllMovables();
    }
    public void HighlightCastedObject(GameObject castedObj) {
        if (castedObj != null) {
            MovableSelector movableSel;
            movableSel = castedObj.GetComponent<MovableSelector>();
            DisableAllMovables();
            if (movableSel != null)
                movableSel.SelectObject(true);
        }
    }
    private void DisableAllMovables() {
        for (int i = 0; i < movablesObj.Count; i++)
            movablesObj[i].GetComponent<MovableSelector>().SelectObject(false);
    }
}
