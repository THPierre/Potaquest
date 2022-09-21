using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Animator animator;
    private TriggerableManager triggerMgr;
    public int triggerIndex;
    private void Update() {
        if (Taunt(500))
            animator.SetTrigger("Taunt");
    }
    private bool Taunt(int maxRange) {
        if (Random.Range(0, maxRange) == Mathf.RoundToInt(maxRange / 2))
            return true;
        else
            return false;
    }
    public void TriggerableManagerInit(TriggerableManager triggerMgr) {
        this.triggerMgr = triggerMgr;
    }
    private void OnTriggerEnter(Collider other) {
        animator.SetTrigger("Crushed");
        if (other.gameObject.CompareTag("Movable") || other.gameObject.name == "Static_Grav")
            triggerMgr.OpeningMatchingTrigger(triggerIndex, false);
    }
    private void OnTriggerExit(Collider other) {
        if (gameObject.CompareTag("Trigger")) {
            animator.SetTrigger("Recover");
            if (other.gameObject.CompareTag("Movable") || other.gameObject.name == "Static_Grav")
                triggerMgr.OpeningMatchingTrigger(triggerIndex, true);
        }
    }
}
