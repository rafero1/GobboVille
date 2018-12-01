using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollector : MonoBehaviour {

    public List<Transform> patrolPoints;

    private Sequence passear;
    private Sequence comer;
    private Sequence coletarAgua;

    public GameObject eat;
    public GameObject water;
    public GameObject waterObject; // waterObject

    WaterObject reservatorioAgua;
    EatObject reservatorioComida;

    public int hungry;
    public bool coletando;

    // Use this for initialization
    void Start () {

        hungry = Random.Range( 0, 11 );
        coletando = false;

        passear = new Sequence();
        comer = new Sequence();
        coletarAgua = new Sequence();

        //eat = GameObject.Find("Eat");
        water = GameObject.FindWithTag("agua");

        reservatorioAgua = waterObject.GetComponent<WaterObject>();
        reservatorioComida = eat.GetComponent<EatObject>();

        ///////////////////////////////////////////////////////////////////////

        foreach (Transform target in patrolPoints)
        {
            WalkToNode node = new WalkToNode(transform, target)
            {
                speed = 10f
            };
            passear.addChild(node); // nó que faz o coletador patrulhar
        }

        ///////////////////////////////////////////////////////////////////////

        WalkToNode nodeAndarParaComer = new WalkToNode(transform, reservatorioComida.transform)
        {
          speed = 15f
        };

        GoEat nodeComer = new GoEat(this, reservatorioComida);

        comer.addChild(nodeAndarParaComer);
        comer.addChild(nodeComer);

        ///////////////////////////////////////////////////////////////////////

        QueroTrabalhar queroTrabalhar = new QueroTrabalhar();

        WalkToNode nodeAndarParaColetar = new WalkToNode(transform, water.transform)
        {
          speed = 20f
        };

        River river = new River();

        WalkToNode nodeAndarParaGuardarAgua = new WalkToNode(transform, reservatorioAgua.transform)
        {
          speed = 20f
        };

        StoreWater storeWater = new StoreWater(this, reservatorioAgua);

        coletarAgua.addChild(queroTrabalhar)
                    .addChild(nodeAndarParaColetar)
                     .addChild(river)
                      .addChild(nodeAndarParaGuardarAgua)
                       .addChild(storeWater);

    }

	// Update is called once per frame
  void Update ()
  {
    //Debug.Log(hungry + " valor da fome");

    if (hungry > 5)
    {
        comer.run();
    }
    else if (reservatorioAgua.valueWater < 5 || this.coletando)
    {
        coletarAgua.run();
    }
    else
    {
        passear.run();
    }
  }
}
