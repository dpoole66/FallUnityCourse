using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_FormationControl : MonoBehaviour {

    public GameObject enemyShip;
    public float enemySpeed = 3.0f;
    public float width = 16;
    public float height = 8;
    public float spawnDelay = 1.0f;
    public AudioClip soundEnemySpawn;

    private float xMin;
    private float xMax;
    private bool movingRight = false;
    private bool AllMembersAreDead;



    // Use this for initialization
    void Start() {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0.03f, 0, distance));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0.97f, 0, distance));
        xMin = leftBoundry.x;
        xMax = rightBoundry.x;

        SpawnIntoFormation();

    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Formation updates
    void Update() {
        // Set formation in motion.
        if (movingRight) {
            transform.position += Vector3.right * enemySpeed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * enemySpeed * Time.deltaTime;
        }

        // Keep formation in gamespace.
        float leftFormation = transform.position.x - (0.5f * width);
        float rightFormation = transform.position.x + (0.5f * width);
        if (leftFormation < xMin) {
            movingRight = true;
        } else if (rightFormation > xMax) {
            movingRight = false;
        }

        if (AllMembersDead()) {
            Debug.Log("Empty Formation");
            SpawnIntoFormation();
        }

    }

    // All Members Are Dead in the formation
    bool AllMembersDead() {
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount > 0) {
                return false;
            }
        }
        return true;
    }

    // Spawn consecutive enemies into a Full Formation
    void SpawnIntoFormation() {

        Transform freePosition = NextFreePosition();

        if(freePosition){ 
            GameObject enemies = Instantiate(enemyShip, freePosition.position, Quaternion.identity) as GameObject;
            enemies.transform.parent = freePosition;
            AudioSource.PlayClipAtPoint(soundEnemySpawn, transform.position);
        }

        if(NextFreePosition()){
            Invoke("SpawnIntoFormation", spawnDelay);
        }
        

    }

    // Next Free Position in formation spawning
    Transform NextFreePosition(){
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount == 0) {
                return childPositionGameObject;
            }
        }
        return null;
    }
}