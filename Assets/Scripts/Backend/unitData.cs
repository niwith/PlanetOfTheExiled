using UnityEngine;
using System.Collections;

public class unitData : MonoBehaviour {

    public objectList listScript;
    

    public bool isPlayer = false;
    public bool addToList = true;

    // Use this for initialization
    void Awake()
    {
        listScript = GameObject.FindGameObjectWithTag("objectLister").GetComponent<objectList>();
    }


    void Start () {
        listScript.playerUnits.Add(gameObject);
        if (isPlayer && addToList)
        {
            listScript.playerUnits.Add(gameObject);
        }
    }
	
    void OnDestroy()
    {
        if (isPlayer && addToList)
        {
            listScript.playerUnits.Remove(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        
    }
}
