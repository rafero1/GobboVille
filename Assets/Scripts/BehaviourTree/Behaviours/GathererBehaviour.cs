using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GathererBehaviour : BehaviourAgent {
    private Sequence BTWander;
    private Sequence BTGetResource;
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

        // Coletar água
        BTGetResource = new Sequence ();
        BTGetResource.addChild (new QueroTrabalhar ());
        BTGetResource.addChild (new WalkToNode (transform, gatheringPoint.transform));
        BTGetResource.addChild (new River ());
        BTGetResource.addChild (new WalkToNode (transform, storage.transform));
        BTGetResource.addChild (new StoreResource (storage, this));
    }

    // Update is called once per frame
    void Update () {
        if (isHungry) {
            BTEat.run ();
        } else if (storage.isFull () || this.gathering) {
            BTGetResource.run ();
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

        agent.hunger--;
        foodStorage.decreaseUnit ();

        return NodeStatus.EATING;

    }
}

// agente verifica o valor da reserva
// na hora da coleta o coletador começa a sentir fome
public class River : Node // adicionaFome
{

    public River () {

    }

    public NodeStatus run () {
        return NodeStatus.SUCCESS;
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
        // Máximo valor de fome é 10
        quero = Random.Range (0, 2);
        Debug.Log (quero + " valor quero");
        if (quero == 1) {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.FAIL;
    }
}