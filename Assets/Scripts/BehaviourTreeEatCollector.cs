using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////// Leaf Nodes ///////////

public class AlterandoValorSede : Node
{
    EatCollector eatCollector;

    public AlterandoValorSede(EatCollector eatCollector)
    {
        this.eatCollector = eatCollector;
    }

    public NodeStatus run()
    {
        // Máximo valor de fome é 10
        eatCollector.thirst = Random.Range( 0, 11 );

        return NodeStatus.SUCCESS;
    }
}

public class GoDrinkWater : Node
{
    EatCollector eatCollector;
    WaterObject waterObject;

    public GoDrinkWater(EatCollector eatCollector, WaterObject waterObject)
    {
        this.eatCollector = eatCollector;
        this.waterObject = waterObject;
    }

    public NodeStatus run()
    {
        if (eatCollector.thirst == 0)
        {
            eatCollector.bebendoAgua = false;
        } else
        {
            eatCollector.thirst--;
            waterObject.valueWater--;
            eatCollector.bebendoAgua = true;
        }

        return NodeStatus.SUCCESS;
    }
}

// agente vai comer
public class Eat : Node
{
    public Eat()
    {

    }

    public NodeStatus run()
    {
        return NodeStatus.SUCCESS;
    }
}

public class StoreEat : Node
{
    EatCollector eatCollector;
    EatObject eatObject;

    public StoreEat(EatCollector eatCollector, EatObject eatObject)
    {
        this.eatCollector = eatCollector;
        this.eatObject = eatObject;
    }

    public NodeStatus run()
    {
        eatObject.valueEat++;

        if (eatObject.valueEat < 15)
        {
          eatCollector.coletando = true;

        } else
        {
            eatCollector.coletando = false;
        }

        return NodeStatus.SUCCESS;
    }
}
