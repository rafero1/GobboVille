using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : Agent {

    private GameController gameController;
    private Sequence BTNight;
    private Sequence BTEnemy;
    public List<Transform> patrolPoints;
    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        BTNight = new Sequence();
        BTEnemy = new Sequence();

        foreach (Transform target in patrolPoints)
        {
            WalkToNode node = new WalkToNode(transform, target) { speed = 20f };
            BTNight.addChild(node);
        }
        BTEnemy.addChild(new FindTarget(this, "Invader"));
        BTEnemy.addChild(new WalkToTargetNode(this) { speed = 20f});
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameController.EnemyExists) {
            if (gameController.Night) {
                BTNight.run();
            }
        } else {
            BTEnemy.run();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Invader")
        {
            collision.gameObject.SetActive(false);
            gameController.enemyCounter--;
        }
    }
    
}

public abstract class Agent : MonoBehaviour
{
    public Transform Target;
}