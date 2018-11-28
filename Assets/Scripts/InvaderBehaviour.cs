using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBehaviour : Agent {
    // Achar alvo
    //   Atacar alvo

    private Sequence BT;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        BT = new Sequence();

        BT.addChild(new FindTarget(this, "Peasant"));
        BT.addChild(new WalkToTargetNode(this) { speed = 10f });
    }
	
	// Update is called once per frame
	void Update () {
        BT.run();
	}
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Peasant")
        {
            collision.gameObject.SetActive(false);
        }
    }

}
