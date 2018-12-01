using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCollector : MonoBehaviour {

    public List<Transform> patrolPoints;

    private Sequence passear;
    private Sequence comer;
    private Sequence beberAgua;
    private Sequence coletarMadeira;

    public GameObject eatObject;
    public GameObject woodPlace; // onde coleta a madeira
    public GameObject woodObject; // onde guarda a madeira
    public GameObject waterObject; // waterObject

    WaterObject reservatorioAgua;
    EatObject reservatorioComida;
    WoodObject reservatorioMadeira;

    public int hungry;
    public int thirst;
    public bool coletando;

    // Use this for initialization
    void Start () {

        hungry = Random.Range( 0, 11 );
        thirst = Random.Range( 0, 11 );
        coletando = false;

        passear = new Sequence();
        comer = new Sequence();
        beberAgua = new Sequence();
        coletarMadeira = new Sequence();

        woodPlace = GameObject.Find("WoodPlace");

        reservatorioAgua = waterObject.GetComponent<WaterObject>();
        reservatorioComida = eatObject.GetComponent<EatObject>();
        reservatorioMadeira = woodObject.GetComponent<WoodObject>();

        ///////////////////////////////////////////////////////////////////////

        // sequencia de andar
        AlterandoValorSedeWoodCollector alterandoValorSedeWoodCollector = new AlterandoValorSedeWoodCollector(this);

        foreach (Transform target in patrolPoints)
        {
            WalkToNode node = new WalkToNode(transform, target)
            {
                speed = 10f
            };
            passear.addChild(node); // nó que faz o coletador patrulhar
        }

        passear.addChild(alterandoValorSedeWoodCollector);

        ///////////////////////////////////////////////////////////////////////

        // sequencia de ir comer
        WalkToNode nodeAndarParaComer = new WalkToNode(transform, reservatorioComida.transform)
        {
          speed = 15f
        };

        GoEatWoodCollector nodeComer = new GoEatWoodCollector(this, reservatorioComida);

        comer.addChild(nodeAndarParaComer)
             .addChild(nodeComer);

        ///////////////////////////////////////////////////////////////////////

        // sequencia para trabalhar
        QueroTrabalhar queroTrabalhar = new QueroTrabalhar();

        WalkToNode nodeAndarParaColetar = new WalkToNode(transform, woodPlace.transform)
        {
          speed = 20f
        };

        WoodPoint woodPoint = new WoodPoint();

        WalkToNode nodeAndarParaGuardarMadeira = new WalkToNode(transform, reservatorioMadeira.transform)
        {
          speed = 20f
        };

        StoreWood storeWood = new StoreWood(this, reservatorioMadeira);
        AlterandoValorFomeWoodCollector alterandoValorFomeWoodCollector = new AlterandoValorFomeWoodCollector(this);

        coletarMadeira.addChild(queroTrabalhar)
                      .addChild(nodeAndarParaColetar)
                      .addChild(woodPoint)
                      .addChild(nodeAndarParaGuardarMadeira)
                      .addChild(storeWood)
                      .addChild(alterandoValorFomeWoodCollector);

       /////////////////////////////////////////////////////////////////////////
       // sequencia beber agua

       WalkToNode nodeWalkToRiver = new WalkToNode(transform, reservatorioAgua.transform)
       {
         speed = 15f
       };

       GoDrinkWaterWoodCollector nodeBeberAgua = new GoDrinkWaterWoodCollector(this, reservatorioAgua);

       beberAgua.addChild(nodeWalkToRiver)
                .addChild(nodeBeberAgua);

    }

	// Update is called once per frame
	void Update ()
  {

    if (hungry > 5)
    {
        comer.run();
    }
    if (thirst > 5)
    {
        beberAgua.run();
    }
    else if (reservatorioMadeira.valueWood < 3 || this.coletando)
    {
        coletarMadeira.run();
    }
    else
    {
      passear.run();
    }

  }
}
