using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tutoHand;
    [SerializeField]
    private GameObject tutoAura;
    [SerializeField]
    private GameObject tutoCol;
    [SerializeField]
    private GridManager gridMgr;
    private List<Tile> tutorialTiles =  new List<Tile>();
    private void Start()
    {
        for (int i = 0; i < gridMgr.grid.Count; i++)
        {
            if (gridMgr.grid[i].isTutorialTile)
                tutorialTiles.Add(gridMgr.grid[i]);
            if (gridMgr.grid[i].isTutorialTile!)
                gridMgr.grid[i].isEnabled = false;
        }
    }
}
