using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderBehaviour : BehaviourAgent {
    private Sequence BT;
    private GameController gameController;

    // Use this for initialization
    void Start () {
        InitAttributes ();
        gameController = GameController.GetGameController ();
        BT = new Sequence ();

        BT.addChild (new FindTarget (this, "Peasant"))
            .addChild (new WalkToTargetNode (this));
    }

    // Update is called once per frame
    void Update () {
        BT.run ();
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Peasant") {
            collision.gameObject.SetActive (false);
            gameController.removeFromList (collision.gameObject);
        }
    }

}