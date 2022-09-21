using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerable : MonoBehaviour
{
    private TriggerableManager triggerMgr;
    public int triggerableIndex;
    public MeshRenderer mesh;
    public BoxCollider coll;
    public AudioSource doorSound;
    public void TriggerableManagerInit(TriggerableManager triggerMgr) {
        this.triggerMgr = triggerMgr;
    }
    public void OpenTriggerable(bool enabled) {
        doorSound.Play();
        StartCoroutine(Openingtriggerable(enabled));
    }
    private IEnumerator Openingtriggerable(bool enabled) {
        mesh.enabled = enabled;
        coll.enabled = enabled;
        yield return null;
    }
}
