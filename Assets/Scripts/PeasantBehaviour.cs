using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PeasantBehaviour : Agent {
    private Sequence BT;
    // Use this for initialization
    void Start () {
        BT = new Sequence();
        WalkNode walkNode = new WalkNode(transform);
        BT.addChild(walkNode);
    }

    // Update is called once per frame
    void Update() {
        BT.run();
    }
}
