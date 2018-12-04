using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : BehaviourAgent {

    private GameController gameController;
    Sequence SequenceEnemyExists;
    private Selector BT;
    public List<Transform> patrolPoints;
    // Use this for initialization
    void Start () {
        InitAttributes ();
        FoodStorage foodStorage = GameController.GetGameController ().foodStorage;
        WaterStorage waterStorage = GameController.GetGameController ().waterStorage;
        gameController = GameController.GetGameController ();

        // Root
        BT = new Selector ();

        // Inimigo existe
        SequenceEnemyExists = new Sequence ();
        SequenceEnemyExists.addChild (new DoesEnemyExists (gameController))
            .addChild (new FindTarget (this, "Invader"))
            .addChild (new WalkToTargetNode (this) { speed = 20f });

        // Quando é dia
        Selector SelectorDayActivities = new Selector ();
        // Quando é dia: Cumer
        Sequence SequenceEat = new Sequence ();
        SequenceEat.addChild (new IsHungryNode (this))
            .addChild (new WalkToNode (transform, foodStorage.transform))
            .addChild (new EatNode (this, foodStorage));
        // Quando é dia: Beber
        Sequence SequenceDrink = new Sequence ();
        SequenceDrink.addChild (new IsThirstyNode (this))
            .addChild (new WalkToNode (transform, waterStorage.transform))
            .addChild (new DrinkNode (this, waterStorage));
        // Quando é dia: Wander
        Sequence SequenceWander = new Sequence ();
        SequenceWander.addChild (new FindRandomPositionNode (GameObject.Find ("Floor"), this))
            .addChild (new WalkToPositionNode (this))
            .addChild (new WaitNode (2, 5, this));
        // Quando é dia: Selector
        SelectorDayActivities.addChild (SequenceEat)
            .addChild (SequenceDrink)
            .addChild (SequenceWander);
        // Quando é dia: Juntando
        Sequence SequenceDaytime = new Sequence ();
        SequenceDaytime.addChild (new IsDayNode (gameController))
            .addChild (SelectorDayActivities);

        // Quando é noite
        Sequence SequenceNight = new Sequence ();
        foreach (Transform target in patrolPoints) {
            WalkToNode node = new WalkToNode (transform, target) { speed = 10f };
            SequenceNight.addChild (node);
        }

        BT
        .addChild (SequenceEnemyExists)
            .addChild (SequenceDaytime)
          .addChild (SequenceNight)
        ;
    }

    // Update is called once per frame
    void Update () {
        updateAttributes ();
        if (gameController.EnemyExists) SequenceEnemyExists.run();
        else BT.run ();
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Invader") {
            collision.gameObject.SetActive (false);
            gameController.enemyCounter--;
            gameController.removeFromList (collision.gameObject);
        }
    }
}

internal class DoesEnemyExists : Node {
    private GameController gameController;

    public DoesEnemyExists (GameController gameController) {
        this.gameController = gameController;
    }

    public NodeStatus run () {
        if (gameController.EnemyExists) return NodeStatus.SUCCESS;
        else return NodeStatus.FAIL;
    }
}

internal class IsDayNode : Node {
    private GameController gameController;

    public IsDayNode (GameController gameController) {
        this.gameController = gameController;
    }

    public NodeStatus run () {
        if (gameController.Day) return NodeStatus.SUCCESS;
        else return NodeStatus.FAIL;
    }
}