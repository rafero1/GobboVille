using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantBehaviour : Agent {
    private Sequence BT;

    // Use this for initialization
    void Start () {
        BT = new Sequence ();
        BT.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this));
        BT.addChild (new WalkToPositionNode (this));
        BT.addChild (new WaitNode (this, 2, 5));
    }

    // Update is called once per frame,
    void Update () {
        BT.run ();
    }
}
