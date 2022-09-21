using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : MonoBehaviour
{
    public MeshRenderer mesh;
    public BoxCollider coll;
    private RaycastHit staticHit;
    [HideInInspector]
    public bool isColliderUseful = true;
    private int raycasts = 0;
    private bool Cast(Vector3 castDir) {
        return Physics.Raycast(transform.localPosition, castDir, out staticHit, 1);
    }
    public void EnableCollider(bool enabled) {
        coll.enabled = enabled;
    }
    public void CheckForUselessCollider() {
        CastAllDirections();
        CheckForRaycastsAmount(raycasts);
    }
    private void CastAllDirections() {
        CastDir(Vector3.up);
        CastDir(Vector3.down);
        CastDir(Vector3.left);
        CastDir(Vector3.right);
    }
    private void CheckForRaycastsAmount(int raycasts) {
        if (raycasts >= 4)
            isColliderUseful = false;
    }
    private void CastDir(Vector3 castDir) {
        if (Cast(castDir)) {
            GameObject castedObj = staticHit.collider.gameObject;
            if (castedObj.CompareTag("Static"))
                raycasts += 1;
            else if (castedObj == null)
                raycasts -= 1;
            else if (castedObj.CompareTag("Trigger") || castedObj.CompareTag("TriggerOne"))
                raycasts -= 1;
        }
    }
}
