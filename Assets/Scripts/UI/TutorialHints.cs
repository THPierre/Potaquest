using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHints : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject fingerSprite;
    public MovableSelector player;
    public int tutoProgress;
    private void Start() {
        HideSpritesOnStart();
        tutoProgress = 1;
    }
    private void HideSpritesOnStart() {
        if (sprite.enabled == true) {
            sprite.enabled = false;
            fingerSprite.SetActive(false);
        }
    }
    public void NextTutoStep(int tutoStep){
        if (tutoStep == 2){
            transform.position = new Vector3(2.5f, -3.5f, 0);//Player First Waypoint
            fingerSprite.SetActive(false);
            tutoProgress = 3;
        }
        if (tutoStep == 3){
            transform.position = new Vector3(0.5f, 1.5f, -1);//Movable_Grav Select
            tutoProgress = 4;
        }
        if (tutoStep == 4){
            transform.position = new Vector3(3.5f, 0.5f, 0);//Movable_Grav Waypoint
            tutoProgress = 5;
        }
        if (tutoStep == 5){
            transform.position = new Vector3(0.5f, 0.5f, -1);//Movable Select
            tutoProgress = 6;
        }
        if (tutoStep == 6){
            transform.position = new Vector3(4.5f,-2.5f,0);//Movable Waypoint
            tutoProgress = 7;
        }
        if (tutoStep == 7){
            transform.position = new Vector3(2.5f, -3.5f, 0);//Player Second Select
            tutoProgress = 8;
        }
        if (tutoStep == 8){
            transform.position = new Vector3(11.5f,-0.5f,0);//Player Second Waypoint
            tutoProgress = 9;
        }
    }
    private void TutorialSpriteInit() {
        sprite.enabled = true;
        fingerSprite.SetActive(true);
        tutoProgress = 2;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            if (tutoProgress == 1)
                TutorialSpriteInit();
            if (tutoProgress == 3)
                NextTutoStep(3);
            if (tutoProgress == 9)
            {

            }
        }
        if (other.gameObject.name == "Movable") {
            if (tutoProgress == 7)
                NextTutoStep(7);
        }
        if (other.gameObject.name == "Movable_Grav") {
            if (tutoProgress == 5)
                NextTutoStep(5);
        }
    }
}
