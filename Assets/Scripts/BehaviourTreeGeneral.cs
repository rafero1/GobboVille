using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////// Leaf Nodes ///////////

public class QueroTrabalhar : Node
{
    // 0 significa não quero QueroTrabalhar
    // 1 significa quero trabalhar
    private int quero;

    public QueroTrabalhar()
    {
      quero = 0;
    }

    public NodeStatus run()
    {
        // Máximo valor de fome é 10
        quero = Random.Range( 0, 2 );
        Debug.Log(quero + " valor quero");
        if(quero == 1)
        {
            return NodeStatus.SUCCESS;
        }

        return NodeStatus.FAIL;
    }
}
