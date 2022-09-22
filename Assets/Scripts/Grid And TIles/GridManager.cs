using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private MovableSelector movableSelector;
    public MovableManager movableMgr;
    [HideInInspector]
    public List<Tile> grid = new List<Tile>();
    private void Start() {
        grid.AddRange(GetComponentsInChildren<Tile>());
        movableMgr.GridInit(this);
        SendThisToAllTiles();
        DisableTilesAtStartup();
    }
    private void SendThisToAllTiles() {
        for (int i = 0; i < grid.Count; i++)
            grid[i].GridInit(this);
    }
    private void DisableTilesAtStartup() {
        for (int i = 0; i < grid.Count; i++) {
            grid[i].GetComponent<Collider>().enabled = false;
            grid[i].GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
    public void SelectedMovableSelector(MovableSelector movableSel) {
        this.movableSelector = movableSel;
    }
    public void SendMovableToSelectedTile(Tile tileSelected) {
        tileSelected.SelectedMovableSelector(movableSelector);
    }
    public void DeselectOtherTiles() {
        for (int i = 0; i < grid.Count; i++)
            grid[i].TileSelector(false);
    }
    public void GridTransitionAnim() {
        for (int i = 0; i < grid.Count; i++)
            grid[i].TileTransition();
    }
    public void GridEnabler(bool enabled) {
        for (int i = 0; i < grid.Count; i++) {
            grid[i].GetComponent<Collider>().enabled = enabled;
            grid[i].GetComponentInChildren<SpriteRenderer>().enabled = enabled;
        }
    }
}
