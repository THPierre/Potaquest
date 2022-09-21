using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGrav : MonoBehaviour
{
    private Player player;
    public Rigidbody rB;
    public BoxCollider triggerCol;
    public AudioSource fallSound;
    private bool isFalling = false;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        if (rB != null && rB.velocity.y < 0)
            isFalling = true;
        else
            isFalling = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isFalling)
            fallSound.Play();
        if (collision.gameObject.CompareTag("Player") && isFalling)
            player.DeathTransition();
    }
}
