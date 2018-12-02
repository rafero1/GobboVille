using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererBehaviour : BehaviourAgent {
    private Sequence BTWander;
    private Sequence BTGatherResource;
    private Sequence BTEat;

    public bool gathering = false;
    public GatheringPoint gatheringPoint;
    public Storage storage;

    public FoodStorage foodStorage;

    // Use this for initialization
    void Start () {
        InitAttributes ();

        // Cumer
        BTEat = new Sequence ();
        BTEat.addChild (new WalkToNode (transform, foodStorage.transform));
        BTEat.addChild (new EatNode (this, foodStorage));

        // Wander
        BTWander = new Sequence ();
        BTWander.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this));
        BTWander.addChild (new WalkToPositionNode (this));
        BTWander.addChild (new WaitNode (2, 5, this));

        // Coletar
        BTGatherResource = new Sequence ();
        BTGatherResource.addChild (new QueroTrabalhar ());
        BTGatherResource.addChild (new WalkToNode (transform, gatheringPoint.transform));
        BTGatherResource.addChild (new WaitNode (2, this));
        BTGatherResource.addChild (new WalkToNode (transform, storage.transform));
        BTGatherResource.addChild (new StoreResource (storage, this));
    }

    // Update is called once per frame
    void Update () {
        updateAttributes ();
        if (isHungry ()) {
            BTEat.run ();
        } else if (!storage.isFull () || this.gathering) {
            BTGatherResource.run ();
        } else {
            BTWander.run ();
        }
    }
}

public class EatNode : Node {

    BehaviourAgent agent;
    FoodStorage foodStorage;

    public EatNode (BehaviourAgent agent, FoodStorage foodStorage) {
        this.agent = agent;
        this.foodStorage = foodStorage;
    }

    public NodeStatus run () {
        if (agent.hunger == 0) {
            return NodeStatus.SUCCESS;
        }

        agent.hunger -= 50;
        foodStorage.decreaseUnit ();

        return NodeStatus.EATING;

    }
}

public class StoreResource : Node {
    GathererBehaviour gatherer;
    Storage storage;

    public StoreResource (Storage storage, GathererBehaviour gatherer) {
        this.gatherer = gatherer;
        this.storage = storage;
    }

    public NodeStatus run () {
        storage.addUnit ();
        gatherer.hunger = Random.Range (0, 11);

        if (!storage.isFull ()) {
            gatherer.gathering = true;

        } else {
            gatherer.gathering = false;
        }

        return NodeStatus.SUCCESS;

    }
}

public class QueroTrabalhar : Node {
    // 0 significa não quero QueroTrabalhar
    // 1 significa quero trabalhar
    private int quero;

    public QueroTrabalhar () {
        quero = 0;
    }

    public NodeStatus run () {
        quero = Random.Range (0, 2);
        if (quero == 1) {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.FAIL;
    }
}