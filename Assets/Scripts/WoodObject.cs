using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodObject : MonoBehaviour
{

    public int valueWood;

    // Use this for initialization
    void Start ()
    {
        // o valor máximo que pode ser armazenado é 20.
        valueWood = Random.Range( 0, 6 );

    }

	// Update is called once per frame
	void Update ()
  {
		  //Debug.Log(valueEat + " valor da comida");
	}

}
