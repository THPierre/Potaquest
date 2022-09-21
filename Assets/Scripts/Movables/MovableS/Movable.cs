using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Movable : MonoBehaviour
{
    public VisualEffect magicVfx;
    public VisualEffect dustPoof;
    public Animator animator;
    [Space(20)]
    public AudioSource fallSound;
    public AudioSource selectSound;
    [Space(20)]
    public MeshRenderer mesh;
    public Rigidbody rb;
    [Space(20)]
    public TutorialHints tuto;
    private Player player;
    private bool isFalling = false;
    [HideInInspector]
    public bool hasLerped = false;
    private void Start() {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (magicVfx.enabled)
            magicVfx.enabled = false;
    }
    private void Update() {
        if (rb != null && rb.velocity.y < 0)
            isFalling = true;
        else
            isFalling = false;
    }
    public void SetTransition(Vector3 newPos) {
        animator.SetTrigger("Transition");
        StartCoroutine(TriggerTransition(newPos));
    }
    private IEnumerator TriggerTransition(Vector3 newPos) {
        yield return new WaitUntil(() => hasLerped);
        if (hasLerped)
            transform.localPosition = newPos;
        yield return null;
    }
    public void SetSelected(bool enabled){
        if (enabled)
            selectSound.Play();
        animator.SetBool("Selected", enabled);
        player.animator.SetBool("Magic", true);
        player.animator.SetBool("Idle", false);
        magicVfx.enabled = enabled;
        if (!enabled) {
            player.animator.SetBool("Magic", false);
            player.animator.SetBool("Idle", true);
        }
        //This method below is only called on Tutorial scene;
        if (enabled && tuto != null){
            if (tuto != null && tuto.tutoProgress == 4)
                tuto.NextTutoStep(4);
            if (tuto != null && tuto.tutoProgress == 6)
                tuto.NextTutoStep(6);
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (name == "Movable_Grav" && isFalling) {
            if (PlayerPrefs.GetInt("Vibrations") == 1)
                Handheld.Vibrate();
            if (fallSound != null)
                fallSound.Play();
            if (collision.gameObject.CompareTag("Player"))
                player.DeathTransition();
            if (dustPoof != null)
                dustPoof.Play();
        }
    }
}
