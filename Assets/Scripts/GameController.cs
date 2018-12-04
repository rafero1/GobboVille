using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject PrimaryLightSource;
    public float rotationSpeed = 0.1f;
    public List<GameObject> agentList;
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
        agentList = getAllAgents ();
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

    public List<GameObject> getAllAgents () {
        List<GameObject> rs = new List<GameObject> ();

        foreach (GameObject item in GameObject.FindGameObjectsWithTag ("Peasant")) {
            rs.Add (item);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag ("Guard")) {
            rs.Add (item);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag ("Invader")) {
            rs.Add (item);
        }

        return rs;
    }

    public void addToList (GameObject entity) {
        agentList.Add (entity);
    }

    public void removeFromList (GameObject entity) {
        agentList.Remove (entity);
    }

    public GameObject getAgent (int index) {
        return agentList[index];
    }

    public static GameController GetGameController () {
        return GameObject.Find ("GameController").GetComponent<GameController> ();
    }

    public void ResetScene () {
        SceneManager.LoadScene (SceneManager.GetSceneAt (0).name);
    }
}