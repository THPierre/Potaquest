using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    //Public variables
    public PlayerPathFinding playerpath;
    public Animator animator;
    public Collider coll;
    public Rigidbody rb;
    public GameObject cam;
    public GameManager gameMgr;
    public VisualEffect runVfx;
    [Space(20)]
    public GameObject selectedCloud;
    public GameObject angryCloud;
    [Space(20)]
    public Preferences prefs;
    [Space(20)]
    //Player Sounds
    public AudioSource deathSound;
    public AudioSource selectedSound;
    public AudioSource runSound;
    public AudioSource jumpSound;
    public AudioSource successSound;
    //Private variables
    private float deathTimer = 0;
    [HideInInspector]
    public bool isDead = false;
    private SceneLoaderManager sceneLoaderMgr;
    private float dabTime = 0;
    private bool isDabbing = false;
    [HideInInspector]
    public bool isLevelCompleted = false;
    private void Start(){
        sceneLoaderMgr = GameObject.FindWithTag("SceneLoader").GetComponent<SceneLoaderManager>();
    }
    private void Update(){
        if (isDead)
            DeathTimer();
        if (isDabbing)
            Dabbing(3);
    }
    private void Dabbing(int dabTime)
    {
        this.dabTime += Time.deltaTime;
        if (this.dabTime >= dabTime) {
            this.dabTime = dabTime;
            isDabbing = false;
            isLevelCompleted = false;
            PlayerPrefs.SetInt("LastLevel", gameMgr.lastLevel);
            sceneLoaderMgr.ToLoadingScreen(gameMgr.nextScene);
        }
    }
    private void DeathTimer(){
        deathTimer += Time.deltaTime;
        if (deathTimer >= 3)
            deathTimer = 3;
        if (deathTimer == 3) {
            isDead = false;
            sceneLoaderMgr.ToLoadingScreen("RestartScene");
        }
    }
    public void EnablePathFinding(bool enabled){
        playerpath.isMoving = false;
        playerpath.enabled = enabled;
        if (!enabled && isLevelCompleted) { //If the Level is completed, triggers the dab animation of the player, then switches to next Level
            cam.GetComponent<CameraConstraints>().enabled = false;
            cam.GetComponent<Lean.Touch.LeanPinchCamera>().enabled = false;
            cam.GetComponent<Camera>().fieldOfView = 25;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, cam.transform.position.z);
            animator.SetBool("Idle", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Run", false);
            animator.SetBool("MoveDown", false);
            animator.SetBool("Climb", false);
            animator.SetBool("Selected", false);
            animator.SetBool("Magic", false);
            animator.SetBool("Success", true);
            isDabbing = true;
        }
    }
    public void SetSelected(bool enabled){
        if (enabled)
            selectedSound.Play();
        if (enabled == false){
            animator.SetBool("Idle", true);
            animator.SetBool("Selected", false);
        }
        if (enabled == true){
            animator.SetBool("Selected", true);
            animator.SetBool("Idle", false);
        }
        SetSelectedCloudAnim(enabled);
        //This Method below is only called on the tutorial level
    }
    public void SetPlayerBlockedAnimation(bool enabled){
        angryCloud.SetActive(enabled);
    }
    public void SetMagicSelectedAnimation(bool enabled){
        selectedCloud.SetActive(enabled);
        if (enabled)
            animator.SetTrigger("Magic");
    }
    public void SetSelectedCloudAnim(bool enabled){
        selectedCloud.SetActive(enabled);
    }
    public void StartPathFinding(GameObject player, Vector3 newPos){
        playerpath.enabled = true;
        playerpath.StartPathFinding(newPos, player);
    }
    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("Mob") || coll.gameObject.CompareTag("Spikes")) {
            if (prefs.vibrationsPreferences == 0)
                Handheld.Vibrate();
            DeathTransition();
        }
    }
    public void DeathTransition(){
        coll.enabled = false;
        rb.isKinematic = true;
        EnablePathFinding(false);
        isDead = true;
        animator.SetTrigger("Death");
        deathSound.Play();
        deathTimer = 0;
    }
}
