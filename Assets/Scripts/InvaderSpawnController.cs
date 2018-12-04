using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawnController : MonoBehaviour {
    public GameController gameController;
    public GameObject invaderPrefab;
    public Transform invaderParent;
    public bool spawnLocked = false;
    public int interval = 10;

    // Use this for initialization
    void Start () {
        gameController = GameController.GetGameController ();
    }

    // Update is called once per frame
    void Update () {
        if (gameController.Night) {
            if (!spawnLocked)
                StartCoroutine (Spawning ());
        } else {
            StopCoroutine (Spawning ());
            spawnLocked = false;
        }
    }

    IEnumerator Spawning () {
        spawnLocked = true;
        yield return new WaitForSecondsRealtime (Random.Range ((int) interval / 2, interval));
        SpawnInvader (new Vector3 (transform.position.x, transform.position.y, transform.position.z), invaderParent);
        spawnLocked = false;
    }

    public void SpawnInvader (Vector3 position, Transform parent) {
        GameObject invader = invaderPrefab;
        Instantiate (invader, position, Quaternion.identity, parent);
        gameController.addToList (invader);
        gameController.enemyCounter++;
    }
}