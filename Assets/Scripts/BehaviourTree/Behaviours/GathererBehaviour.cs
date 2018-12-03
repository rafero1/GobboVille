using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererBehaviour : BehaviourAgent {

    private Selector BT;
    public GatheringPoint gatheringPoint;
    public Storage storage;

    // Use this for initialization
    void Start () {
        InitAttributes ();
        FoodStorage foodStorage = GameController.GetGameController ().foodStorage;
        WaterStorage waterStorage = GameController.GetGameController ().waterStorage;
        // Root
        BT = new Selector ();

        // Cumer
        Sequence SequenceEat = new Sequence ();
        SequenceEat.addChild (new IsHungryNode (this))
            .addChild (new WalkToNode (transform, foodStorage.transform))
            .addChild (new EatNode (this, foodStorage));

        // Beber
        Sequence SequenceDrink = new Sequence ();
        SequenceDrink.addChild (new IsThirstyNode (this))
            .addChild (new WalkToNode (transform, waterStorage.transform))
            .addChild (new DrinkNode (this, waterStorage));

        // Coletar
        Sequence SequenceGatherResource = new Sequence ();
        SequenceGatherResource.addChild (new IsStorageFullNode (storage))
            .addChild (new WalkToNode (transform, gatheringPoint.transform))
            .addChild (new WaitNode (2, this))
            .addChild (new WalkToNode (transform, storage.transform))
            .addChild (new StoreResource (storage, this));

        // Wander
        Sequence SequenceWander = new Sequence ();
        SequenceWander.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this))
            .addChild (new WalkToPositionNode (this))
            .addChild (new WaitNode (2, 5, this));

        BT.addChild (SequenceEat)
            .addChild (SequenceDrink)
            .addChild (SequenceGatherResource)
            .addChild (SequenceWander);
    }

    // Update is called once per frame
    void Update () {
        updateAttributes ();
        BT.run ();
    }
}

public class IsThirstyNode : Node {
    BehaviourAgent agent;
    public IsThirstyNode (BehaviourAgent agent) {
        this.agent = agent;
    }

    public NodeStatus run () {
        if (agent.isThirsty ()) return NodeStatus.SUCCESS;
        else return NodeStatus.FAIL;
    }
}

public class DrinkNode : Node {

    BehaviourAgent agent;
    WaterStorage waterStorage;

    public DrinkNode (BehaviourAgent agent, WaterStorage waterStorage) {
        this.agent = agent;
        this.waterStorage = waterStorage;
    }

    public NodeStatus run () {
        if (agent.thirst == 0) {
            return NodeStatus.SUCCESS;
        }

        agent.thirst -= 50;
        waterStorage.decreaseUnit ();

        return NodeStatus.FAIL;

    }
}

public class IsHungryNode : Node {
    BehaviourAgent agent;
    public IsHungryNode (BehaviourAgent agent) {
        this.agent = agent;
    }

    public NodeStatus run () {
        if (agent.isHungry ()) return NodeStatus.SUCCESS;
        else return NodeStatus.FAIL;
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

        return NodeStatus.FAIL;

    }
}

public class IsStorageFullNode : Node {
    Storage storage;
    public IsStorageFullNode (Storage storage) {
        this.storage = storage;
    }

    public NodeStatus run () {
        if (storage.isFull ()) return NodeStatus.FAIL;
        else return NodeStatus.SUCCESS;
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
        storage.addUnits (5);
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