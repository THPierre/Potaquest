using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    public Rigidbody mobRb;
    private bool isMoving = false;
    private Vector3 startDir;
    private Vector3 rightDir = new Vector3(1, 0, 0);
    private Vector3 leftDir = new Vector3(-1, 0, 0);
    private Vector3 currentPos;
    private bool isWaiting = false;
    private float waitingProgress = 0;
    private LayerMask playerMask;
    private float lerpProgress = 0;
    private bool isFalling;
    private RaycastHit castHit;
    public AudioSource deathSound;
    private void Start()
    {
        RandomDir();
        playerMask = LayerMask.GetMask("Player");
    }
    public void CheckIfMobIsFalling()
    {
        if (mobRb.velocity.y != 0)
            isFalling = true;
        else
            isFalling = false;
    }
    private void RandomDir()
    {
        int start = Random.Range(0, 2);
        if (start == 0)
            startDir = leftDir;
        else
            startDir = rightDir;
    }
    private void FixedUpdate()
    {
        MobPathFinding();
    }
    private void Update()
    {
        CheckIfMobIsFalling();
        if (isWaiting && !isFalling)
            EnnemyStaleMate();
    }
    private bool MobRaycast(Vector3 castStart, Vector3 castDir, float castLenght)
    {
        return Physics.Raycast(castStart, castDir,out castHit, castLenght, ~playerMask);
    }
    private void EnnemyLerping(Vector3 currPos)
    {
        transform.localPosition = Vector3.Lerp(currPos, currPos + startDir, lerpProgress);
    }
    private void EnnemySnap()
    {
        Vector3 snapPos
         = new Vector3(Mathf.Round(transform.localPosition.x * 2) / 2,
         Mathf.Round(transform.localPosition.y * 2) / 2, 0);
        transform.localPosition = snapPos;
    }
    private void MobPathFinding()
    {
        if (!isWaiting)
        {
            if (!isMoving)
            {
                if (MobRaycast(transform.localPosition, Vector3.down, 1))//Bottom Cast
                {
                    if (MobRaycast(transform.localPosition, startDir, 1))//Front Cast
                    {
                        //wait 2 sec, change direction
                        isWaiting = true;
                        waitingProgress = 0;
                        if (startDir == leftDir)
                            startDir = rightDir;
                        else
                            startDir = leftDir;
                    }
                    else
                    {
                        if (!MobRaycast(transform.localPosition + startDir, Vector3.down, 1))//Bottom Right Cast
                        {
                            //Wair 2 sec, change dir
                            isWaiting = true;
                            waitingProgress = 0;
                            if (startDir == leftDir)
                                startDir = rightDir;
                            else
                                startDir = leftDir;
                        }
                        else
                        {
                            if (castHit.collider.gameObject.CompareTag("Spikes"))
                            {
                                //Wair 2 sec, change dir
                                isWaiting = true;
                                waitingProgress = 0;
                                if (startDir == leftDir)
                                    startDir = rightDir;
                                else
                                    startDir = leftDir;
                            }
                            else
                            {
                                //Avancer, continuer
                                isMoving = true;
                                currentPos = transform.localPosition;
                                lerpProgress = 0;
                            }
                        }
                    }
                }
            }
            else//Mob is Moving
            {
                lerpProgress += Time.fixedDeltaTime;
                if (lerpProgress >= 1)
                    lerpProgress = 1;
                EnnemyLerping(currentPos);
                if (lerpProgress == 1)
                {
                    EnnemySnap();
                    isMoving = false;
                }
            }
        }
    }
    private void EnnemyStaleMate()
    {
        waitingProgress += Time.deltaTime;
        if (waitingProgress >= 1)
            waitingProgress = 1;
        if (waitingProgress == 1)
            isWaiting = false;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Spikes"))
        {
            deathSound.Play();
            Destroy(gameObject);
        }
        if (col.gameObject.name =="Static_Grav")
        {
            deathSound.Play();
            Destroy(gameObject);
        }
    }
}
