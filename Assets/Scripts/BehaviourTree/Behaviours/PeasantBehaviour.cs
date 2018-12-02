using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantBehaviour : BehaviourAgent {
    private Sequence BTWander;

    // Use this for initialization
    void Start () {
        BTWander = new Sequence ();
        BTWander.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this));
        BTWander.addChild (new WalkToPositionNode (this));
        BTWander.addChild (new WaitNode (2, 5, this));
    }

    // Update is called once per frame,
    void Update () {
        BTWander.run ();
    }
}