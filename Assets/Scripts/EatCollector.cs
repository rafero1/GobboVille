using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatCollector : MonoBehaviour
{

    public List<Transform> patrolPoints;

    private Sequence passear;
    private Sequence coletarComida;
    private Sequence beberAgua;

    public GameObject comida; // lugar onde coleta a comida
    public GameObject reservatorioAgua; // reservatorio água local
    public GameObject eatObject; // waterObject

    private EatObject reservatorioComida;
    private WaterObject reservatorioAgua2;
    //public GameObject rs;

    public int thirst;
    public bool coletando;
    public bool bebendoAgua;

    // Use this for initialization
    void Start ()
    {

        passear = new Sequence();
        coletarComida = new Sequence();
        beberAgua = new Sequence();

        comida = GameObject.Find("EatPlace"); // local onde colhe a comida
        //reservatorioAgua = GameObject.Find("ReservatorioAgua"); // reservatório de comida

        reservatorioComida = eatObject.GetComponent<EatObject>();
        reservatorioAgua2 = reservatorioAgua.GetComponent<WaterObject>();

        thirst = Random.Range( 0, 11 );
        coletando = false;
        bebendoAgua = false;

        ///////////////////////////////////////////////////////////////////////

        AlterandoValorSede alterandoValor = new AlterandoValorSede(this);

        foreach (Transform target in patrolPoints)
        {
            WalkToNode node = new WalkToNode(transform, target)
            {
                speed = 10f
            };
            passear.addChild(node); // nó que faz o coletador patrulhar

        }
        passear.addChild(alterandoValor);
        ///////////////////////////////////////////////////////////////////////

        QueroTrabalhar queroTrabalhar = new QueroTrabalhar();

        WalkToNode nodeAndarParaColetar = new WalkToNode(transform, comida.transform)
        {
          speed = 15f
        };

        Eat eat = new Eat(); // lugar onde pega a comida

        WalkToNode nodeAndarParaReservatorioComida = new WalkToNode(transform, reservatorioComida.transform)
        {
          speed = 15f
        };

        StoreEat storeEat = new StoreEat(this, reservatorioComida);

        coletarComida.addChild(queroTrabalhar)
                     .addChild(nodeAndarParaColetar)
                     .addChild(eat)
                     .addChild(nodeAndarParaReservatorioComida)
                     .addChild(storeEat)
                     .addChild(alterandoValor);

        ///////////////////////////////////////////////////////////////////////

        WalkToNode nodeAndarParaReservatorioAgua = new WalkToNode(transform, reservatorioAgua2.transform)
        {
          speed = 15f
        };

        GoDrinkWater goDrinkWater = new GoDrinkWater(this, reservatorioAgua2);

        beberAgua.addChild(nodeAndarParaReservatorioAgua)
                 .addChild(goDrinkWater);

    }

	// Update is called once per frame
	void Update ()
  {
    Debug.Log(thirst + " valor da sede");

    if ((thirst > 6) && (reservatorioAgua2.valueWater > 0))
    {
        beberAgua.run();
    }
    else if (reservatorioComida.valueEat < 4 || this.coletando) // reservatorioComida
    {
        coletarComida.run();
    }
    else
    {
      passear.run();
    }
  }
}
