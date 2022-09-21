using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManager : MonoBehaviour
{
    private List<Static> staticList = new List<Static>();
    public Material[] matIterations;
    private void Start() {
        InitializeStaticsList();
        SetRandomTexture();
    }
    private void SetRandomTexture() {
        for (int i = 0; i < staticList.Count; i++) {
            int textureIndex = Random.Range(0, matIterations.Length);
            staticList[i].mesh.material = matIterations[textureIndex];
        }
    }
    private void InitializeStaticsList() {
        staticList.AddRange(GetComponentsInChildren<Static>());
    }
    public void CheckForUselessColliders() {
        for (int index = 0; index < staticList.Count; index++)
            staticList[index].CheckForUselessCollider();
        for (int index = 0; index < staticList.Count; index++)
            if (staticList[index].isColliderUseful == false)
                staticList[index].EnableCollider(false);
    }
}
