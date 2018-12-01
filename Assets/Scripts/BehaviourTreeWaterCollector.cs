using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// agente vai comer
public class GoEat : Node
{

    WaterCollector waterCollector;
    EatObject eatObject;

    public GoEat(WaterCollector waterCollector, EatObject eatObject)
    {
        this.waterCollector = waterCollector;
        this.eatObject = eatObject;
    }

    public NodeStatus run()
    {
        if (waterCollector.hungry == 0)
        {
            return NodeStatus.SUCCESS;
        }

        waterCollector.hungry--;
        eatObject.valueEat--;

        return NodeStatus.EATING;

    }
}

// agente verifica o valor da reserva
// na hora da coleta o coletador começa a sentir fome
public class River : Node // adicionaFome
{

    public River()
    {

    }

    public NodeStatus run()
    {
        return NodeStatus.SUCCESS;
    }
}

public class StoreWater : Node
{
    WaterCollector waterCollector;
    WaterObject waterObject;


    public StoreWater(WaterCollector waterCollector, WaterObject waterObject)
    {
        this.waterCollector = waterCollector;
        this.waterObject = waterObject;
    }

    public NodeStatus run()
    {
      waterObject.valueWater++;
      waterCollector.hungry = Random.Range( 0, 11 );

      if (waterObject.valueWater < 15)
      {
        waterCollector.coletando = true;

      } else
      {
          waterCollector.coletando = false;
      }

        return NodeStatus.SUCCESS;

    }
}
