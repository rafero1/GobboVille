using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour {

    public int valueWater;

    // Use this for initialization
    void Start () {
        // o valor máximo que pode ser armazenado é 15.
        valueWater = Random.Range( 0, 6 );

    }

	// Update is called once per frame
	void Update () {
		  //Debug.Log(valueWater + " valor da água");
	}

}
