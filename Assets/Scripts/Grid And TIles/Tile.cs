using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Texture2D baseMat;
    public Texture2D selectedMat;
    public Animator animator;
    public MeshRenderer mesh;
    public bool isTutorialTile;
    public bool tutorialTileindex;
    [HideInInspector]
    public bool isSelected;
    private GridManager gridMgr;
    private MovableSelector movableSel;
    public void SelectedMovableSelector(MovableSelector movableSel)
    {
        this.movableSel = movableSel;
    }
    public void GridInit(GridManager gridMgr)
    {
        this.gridMgr = gridMgr;
    }
    private void OnMouseDown()
    {
        TileSelector(true);
        //if (isSelected)//Check for Selected Object Tag
        if (movableSel.gameObject.CompareTag("Player"))
        {
            movableSel.SelectObject(false);
            movableSel.GetComponent<Player>().StartPathFinding(movableSel.gameObject, this.transform.localPosition);
        }
        else
        {
            movableSel.SelectObject(false);
            movableSel.GetComponent<Movable>().SetTransition(this.transform.localPosition);
        }
    }
    public void TileSelectedAnim(bool enabled)
    {
        animator.SetBool("Selected", enabled);
    }
    public void TileTransition()
    {
        animator.SetTrigger("TileTransition");
    }
    public void TileSelector(bool enabled)
    {
        if (enabled)
        {
            gridMgr.DeselectOtherTiles();
            gridMgr.SendMovableToSelectedTile(this);
            mesh.material.mainTexture = selectedMat;
            isSelected = true;
        }
        else
        {
            mesh.material.mainTexture = baseMat;
            isSelected = false;
        }
    }
}
