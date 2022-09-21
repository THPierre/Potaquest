using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableManager : MonoBehaviour
{
    private List<Triggerable> triggerableList = new List<Triggerable>();
    private List<Trigger> triggerList = new List<Trigger>();
    private void Start() {
        ListsInit();
        SendThisToLists(this);
    }
    private void ListsInit() {
        triggerableList.AddRange(GetComponentsInChildren<Triggerable>());
        triggerList.AddRange(GetComponentsInChildren<Trigger>());
    }
    private void SendThisToLists(TriggerableManager triggerMgr) {
        for (int i = 0; i < triggerableList.Count; i++)
            triggerableList[i].TriggerableManagerInit(triggerMgr);
        for (int i = 0; i < triggerList.Count; i++)
            triggerList[i].TriggerableManagerInit(triggerMgr);
    }
    public void OpeningMatchingTrigger(int triggerIndex, bool enabled) {
        for (int i = 0; i < triggerableList.Count; i++)
            if (triggerableList[i].triggerableIndex == triggerIndex)
                triggerableList[i].OpenTriggerable(enabled);
    }
}
