using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : Agent {

    private GameController gameController;
    private Sequence BTNight;
    private Sequence BTDay;
    private Sequence BTEnemy;
    public List<Transform> patrolPoints;
    // Use this for initialization
    void Start () {
        gameController = GameObject.Find ("GameController").GetComponent<GameController> ();

        // Behaviour durante a noite
        BTNight = new Sequence ();
        foreach (Transform target in patrolPoints) {
            WalkToNode node = new WalkToNode (transform, target) { speed = 20f };
            BTNight.addChild (node);
        }

        // Behaviour durante o dia
        BTDay = new Sequence ();
        BTDay.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this));
        BTDay.addChild (new WalkToPositionNode (this));
        BTDay.addChild (new WaitNode (this, 2, 5));

        // Behaviour quando inimigo existe
        BTEnemy = new Sequence ();
        BTEnemy.addChild (new FindTarget (this, "Invader"));
        BTEnemy.addChild (new WalkToTargetNode (this) { speed = 20f });

    }

    // Update is called once per frame
    void Update () {
        if (!gameController.EnemyExists) {
            if (gameController.Night) {
                BTNight.run ();
            } else {
                BTDay.run ();
            }
        } else {
            BTEnemy.run ();
        }
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Invader") {
            collision.gameObject.SetActive (false);
            gameController.enemyCounter--;
        }
    }
}

public abstract class Agent : MonoBehaviour {
    public Transform Target;
    public Vector3 targetPosition;
}