using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovableSelector : MonoBehaviour
{
    [HideInInspector]
    public GridManager gridMgr;
    [HideInInspector]
    public bool isSelected;
    private Player player;
    public void PlayerInit(Player player) {
        this.player = player;
    }
    public void GridInit(GridManager gridMgr) {
        this.gridMgr = gridMgr;
    }
    public void SelectObject(bool enabled) {
        gridMgr.GridTransitionAnim();
        gridMgr.DeselectOtherTiles();
        if (gameObject.CompareTag("Player"))
            gameObject.GetComponent<Player>().SetSelected(enabled);
        else {
            gameObject.GetComponent<Movable>().SetSelected(enabled);
            player.SetMagicSelectedAnimation(enabled);
        }
        SelectionParameters(enabled);
    }
    private void SelectionParameters(bool enabled) {
        if (enabled)
            gridMgr.SelectedMovableSelector(this);
        gridMgr.GridEnabler(enabled);
        isSelected = enabled;
    }
}
