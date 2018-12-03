using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject PrimaryLightSource;
    public float rotationSpeed = 0.1f;
    private List<GameObject> entityList;
    public int enemyCounter;
    private bool daytime = true;
    public bool Day {
        get { return daytime; }
    }
    public bool Night {
        get { return !daytime; }
    }
    public bool EnemyExists {
        get {
            if (enemyCounter > 0) {
                return true;
            } else return false;
        }
    }

    public FoodStorage foodStorage;
    public WaterStorage waterStorage;

    // Use this for initialization
    void Start () {
        entityList = getAllEntities ();
    }

    // Update is called once per frame
    void Update () {
        rotateSun (rotationSpeed);
    }

    void rotateSun (float speed) {
        Transform sun = PrimaryLightSource.GetComponent<Transform> ();
        if (sun.localRotation.eulerAngles.x > 0 && sun.localRotation.eulerAngles.x < 180) {
            daytime = true;
        } else if (sun.localRotation.eulerAngles.x > 180 || sun.localRotation.eulerAngles.x < 0) {
            daytime = false;
        }
        sun.Rotate (speed, 0, 0);
    }

    public GameObject[] getEntitiesByTag (string tag) {
        return GameObject.FindGameObjectsWithTag (tag);
    }

    public List<GameObject> getAllEntities () {
        List<GameObject> rs = new List<GameObject> ();

        foreach (GameObject item in getEntitiesByTag ("Peasant")) {
            rs.Add (item);
        }

        foreach (GameObject item in getEntitiesByTag ("Guard")) {
            rs.Add (item);
        }

        foreach (GameObject item in getEntitiesByTag ("Invader")) {
            rs.Add (item);
        }

        return rs;
    }

    public void addToList (GameObject entity) {
        entityList.Add (entity);
    }

    public void removeFromList (GameObject entity) {
        entityList.Remove (entity);
    }

    public static GameController GetGameController () {
        return GameObject.Find ("GameController").GetComponent<GameController> ();
    }
}