using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterandoValorSedeWoodCollector : Node
{
    WoodCollector woodCollector;

    public AlterandoValorSedeWoodCollector(WoodCollector woodCollector)
    {
        this.woodCollector = woodCollector;
    }

    public NodeStatus run()
    {
        // Máximo valor de fome é 10
        woodCollector.thirst = Random.Range( 0, 11 );

        return NodeStatus.SUCCESS;
    }
}

// agente vai comer
public class GoEatWoodCollector : Node
{

    WoodCollector woodCollector;
    EatObject eatObject;

    public GoEatWoodCollector(WoodCollector woodCollector, EatObject eatObject)
    {
        this.woodCollector = woodCollector;
        this.eatObject = eatObject;
    }

    public NodeStatus run()
    {
        if (woodCollector.hungry == 0)
        {
            return NodeStatus.SUCCESS;
        }

        woodCollector.hungry--;
        eatObject.valueEat--;

        return NodeStatus.EATING;

    }
}

// beber água
public class GoDrinkWaterWoodCollector : Node
{
    WoodCollector woodCollector;
    WaterObject waterObject;

    public GoDrinkWaterWoodCollector(WoodCollector woodCollector, WaterObject waterObject)
    {
        this.woodCollector = woodCollector;
        this.waterObject = waterObject;
    }

    public NodeStatus run()
    {/*
        if (woodCollector.thirst== 0)
        {
            return NodeStatus.SUCCESS;
        }*/

        woodCollector.thirst--;
        waterObject.valueWater--;

        //return NodeStatus.EATING;

        return NodeStatus.SUCCESS;

    }
}

// agente verifica o valor da reserva
// na hora da coleta o coletador começa a sentir fome
public class WoodPoint : Node // adicionaFome
{

    public WoodPoint()
    {

    }

    public NodeStatus run()
    {
        return NodeStatus.SUCCESS;
    }
}

public class StoreWood : Node
{
    WoodCollector woodCollector;
    WoodObject woodObject;

    public StoreWood(WoodCollector woodCollector, WoodObject woodObject)
    {
        this.woodCollector = woodCollector;
        this.woodObject = woodObject;
    }

    public NodeStatus run()
    {
      woodObject.valueWood++;
      woodCollector.thirst = Random.Range( 0, 11 );

      if (woodObject.valueWood < 10)
      {
        woodCollector.coletando = true;

      } else
      {
          woodCollector.coletando = false;
      }

        return NodeStatus.SUCCESS;

    }
}

public class AlterandoValorFomeWoodCollector : Node
{
    WoodCollector woodCollector;

    public AlterandoValorFomeWoodCollector(WoodCollector woodCollector)
    {
        this.woodCollector = woodCollector;
    }

    public NodeStatus run()
    {
        // Máximo valor de fome é 10
        woodCollector.hungry = Random.Range( 0, 11 );

        return NodeStatus.SUCCESS;
    }
}
