using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathFinding : MonoBehaviour
{
    public GameManager gameMgr;
    [HideInInspector]
    public GameObject selectedObject;
    private GameObject levelWin;
    private Vector3 playerDestination;
    private Vector3 moveDir;
    private Vector3 currentPos;
    private Vector3 newCoordinates;
    private RaycastHit cast;
    [HideInInspector]
    public bool isMoving = false;
    public Player player;
    private float lerpProgress = 0;
    private float lerpMultiplier;
    public SpriteRenderer sprite;
    public GameObject spriteGameObject;
    private bool isDabbing = false;
    private float dabbingTime = 0;
    public void StartPathFinding(Vector3 playerDest, GameObject obj) {
        playerDestination = playerDest;
        selectedObject = obj;
        currentPos = obj.transform.localPosition;
        moveDir = (playerDestination - selectedObject.transform.position).normalized;
        player = selectedObject.GetComponent<Player>();
        if (moveDir.x > 0)
            sprite.flipX = false;
        else if (moveDir.x < 0)
            sprite.flipX = true;
    }
    private void Start() {
        levelWin = GameObject.Find("levelWin");
    }
    private void FixedUpdate() {
        if (isDabbing)
            DabbingTime(3);
        StartingPathFinding();
    }
    private void DabbingTime(float dabTime) {
        dabbingTime += Time.fixedDeltaTime * 10;
        Debug.Log(dabbingTime);
        if (dabbingTime >= dabTime)
            dabbingTime = 3;
        if (dabbingTime == dabTime)
            isDabbing = false;
    }
    private bool PlayerCast(Vector3 startPositive,Vector3 startNegative,Vector3 dirPositive,Vector3 dirNegative,float lenght) {
        if (moveDir.x > 0)
            return Physics.Raycast(selectedObject.transform.localPosition + startPositive, dirPositive,out cast, lenght);
        else if (moveDir.x < 0)
            return Physics.Raycast(selectedObject.transform.localPosition + startNegative, dirNegative, out cast, lenght);
        else
            return false;
    }
    private void PlayerLerping(Vector3 currPos, Vector3 newCoor) {
        selectedObject.transform.localPosition = Vector3.Lerp(currPos, currPos + newCoor, lerpProgress);
    }
    private void PlayerSnap() {
        Vector3 snapPos
         = new Vector3(Mathf.Round(selectedObject.transform.localPosition.x * 2) / 2,
         Mathf.Round(selectedObject.transform.localPosition.y * 2) / 2, 0);
        selectedObject.transform.localPosition = snapPos;
    }
    
    private void StartingPathFinding() {
        if (!isMoving) {//if player is not moving
            if (PlayerCast(Vector3.zero, Vector3.zero, Vector3.down, Vector3.down, 1)) {//BottomCast
                if (PlayerCast(Vector3.zero, Vector3.zero, Vector3.right, Vector3.left, 1)) {//FrontCast
                    if (PlayerCast(Vector3.zero, Vector3.zero, Vector3.up, Vector3.up, 1)) {//UpperCast
                        PlayerStop();
                    } else {
                        if (PlayerCast(new Vector3(0,0.25f,0), new Vector3(0, 0.25f, 0),
                                Vector3.right + Vector3.up, Vector3.left + Vector3.up, 1.25f)) {//UpperRightCast
                            PlayerStop();
                        } else {
                            TriggerClimbAnimation();
                            PlayerLerpingSetup(1, new Vector3(1, 1, 0), new Vector3(-1, 1, 0));//Player Climb Up
                        }
                    }
                }
                else
                {
                    if (PlayerCast(new Vector3(0, 0.25f, 0), new Vector3(0, 0.25f, 0),
                        Vector3.right + Vector3.down, Vector3.left + Vector3.down, 1.25f)){//BottomRightCast
                        if (cast.collider.gameObject.CompareTag("Spikes")) {
                            TriggerJumpAnimation();
                            PlayerLerpingSetup(5, new Vector3(2, 0, 0), new Vector3(-2, 0, 0));//Player Jump Over Spikes
                        } else {
                            TriggerRunAnimation();
                            PlayerLerpingSetup(5, new Vector3(1, 0, 0), new Vector3(-1, 0, 0));//Player Move Forward
                        }
                    } else {
                        if (PlayerCast(new Vector3(1, -1, 0), new Vector3(-1, -1, 0), Vector3.down, Vector3.down, 1)) {//BottomRightTwoCast
                            if (cast.collider.gameObject.CompareTag("Spikes")) {
                                TriggerJumpAnimation();
                                PlayerLerpingSetup(1, new Vector3(2, 0, 0), new Vector3(-2, 0, 0));//Player Jump
                            } else {
                                TriggerMoveDownAnimation();
                                PlayerLerpingSetup(1, new Vector3(1, -1, 0), new Vector3(-1, -1, 0));//player Move Down
                            }
                        } else {
                            if (PlayerCast(new Vector3(1, -1, 0), new Vector3(-1, -1, 0), Vector3.right, Vector3.left, 1))//BottomRightTwoCastHorizontal
                            {
                                if (!PlayerCast(Vector3.zero, Vector3.zero, Vector3.right, Vector3.left, 2))//FrontTwoCast
                                {
                                    TriggerJumpAnimation();
                                    PlayerLerpingSetup(1, new Vector3(2, 0, 0), new Vector3(-2, 0, 0));//Player Jump
                                } else {
                                    if (playerDestination.y < selectedObject.transform.localPosition.y) {
                                        TriggerJumpAnimation();
                                        PlayerLerpingSetup(5, new Vector3(1, 0, 0), new Vector3(-1, 0, 0));//Player Jump One block
                                    }
                                }
                            } else {
                                if (PlayerCast(Vector3.zero, Vector3.zero, Vector3.right, Vector3.left, 2))//FrontCastTwo
                                    PlayerLerpingSetup(5, new Vector3(1, 0, 0), new Vector3(-1, 0, 0));//Player Move Before hitting the front bloc and Fall
                                else {
                                    TriggerJumpAnimation();
                                    PlayerLerpingSetup(1, new Vector3(2, 0, 0), new Vector3(-2, 0, 0));//Player Jump
                                }
                            }
                        }
                    }
                }
            }
            else {//nothing underneath player
                BackToIdleState();
                player.runSound.Stop();
                player.jumpSound.Stop();
                player.EnablePathFinding(false);
            }
        }
        else { //if player is moving
            lerpProgress += Time.fixedDeltaTime * lerpMultiplier;
            if (lerpProgress >= 1)
                lerpProgress = 1;
            PlayerLerping(currentPos, newCoordinates);//player is Lerping
            if (lerpProgress == 1) {
                PlayerSnap();
                RestartPathFinding();
            }
        }
    }

    private void PlayerStop() {
        BackToIdleState();
        player.EnablePathFinding(false);//Player Stop
        player.animator.SetTrigger("Angry");
        player.SetPlayerBlockedAnimation(true);
        player.animator.SetBool("Idle", true);
        player.runSound.Stop();
        player.jumpSound.Stop();
    }

    private void TriggerClimbAnimation() {
        player.animator.SetBool("Jump", false);
        player.animator.SetBool("Run", false);
        player.animator.SetBool("MoveDown", false);
        player.animator.SetBool("Climb", true);
        player.animator.SetBool("Idle", false);
        player.jumpSound.Play();
        player.runSound.Stop();
    }

    private void TriggerRunAnimation() {
        player.SetPlayerBlockedAnimation(false);
        player.runVfx.enabled = true;
        player.animator.SetBool("Jump", false);
        player.animator.SetBool("Run", true);
        player.animator.SetBool("MoveDown", false);
        player.animator.SetBool("Climb", false);
        player.animator.SetBool("Idle", false);
        player.runSound.Play();
        player.jumpSound.Stop();
    }

    private void TriggerMoveDownAnimation() {
        player.SetPlayerBlockedAnimation(false);
        player.runVfx.enabled = false;
        player.animator.SetBool("Jump", false);
        player.animator.SetBool("Run", false);
        player.animator.SetBool("MoveDown", true);
        player.animator.SetBool("Climb", false);
        player.animator.SetBool("Idle", false);
        player.jumpSound.Play();
        player.runSound.Stop();
    }

    private void TriggerJumpAnimation() {
        player.SetPlayerBlockedAnimation(false);
        player.runVfx.enabled = false;
        player.animator.SetBool("Jump", true);
        player.animator.SetBool("Run", false);
        player.animator.SetBool("MoveDown", false);
        player.animator.SetBool("Climb", false);
        player.animator.SetBool("Idle", false);
        player.jumpSound.Play();
        player.runSound.Stop();
    }

    private void PlayerLerpingSetup(float lerpMultiplier, Vector3 newCoordPositive, Vector3 newCoordNegative) {
        this.lerpMultiplier = lerpMultiplier;
        lerpProgress = 0;
        isMoving = true;
        currentPos = selectedObject.transform.localPosition;
        if (moveDir.x > 0)
            newCoordinates = newCoordPositive;
        else if (moveDir.x < 0)
            newCoordinates = newCoordNegative;
        else
            player.EnablePathFinding(false);
    }
    private void BackToIdleState() {
        player.animator.SetBool("Idle", true);
        player.animator.SetBool("Jump", false);
        player.animator.SetBool("Run", false);
        player.animator.SetBool("MoveDown", false);
        player.animator.SetBool("Climb", false);
        player.animator.SetBool("Selected", false);
        player.animator.SetBool("Magic", false);
        player.animator.SetBool("Success", false);
        player.angryCloud.SetActive(false);
        player.selectedCloud.SetActive(false);
    }
    private void RestartPathFinding() {
        if (selectedObject.transform.localPosition.x == playerDestination.x && 
            selectedObject.transform.localPosition.x >= levelWin.transform.localPosition.x) {
            isMoving = false;
            BackToIdleState();
            player.isLevelCompleted = true;
            player.EnablePathFinding(false);
        }
        else
            if (selectedObject.transform.localPosition != playerDestination)
                isMoving = false;
            if (selectedObject.transform.localPosition.x == playerDestination.x) {
                BackToIdleState();
                player.runSound.Stop();
                player.jumpSound.Stop();
                player.EnablePathFinding(false);
            }
    }
}
