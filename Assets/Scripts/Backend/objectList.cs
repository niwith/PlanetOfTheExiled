using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class objectList : MonoBehaviour {

	public List<GameObject> playerUnits;
    public List<GameObject> selectedUnits;

    public void Awake()
    {
        playerUnits.Clear();
    }
    
    // Update is called once per frame
	public void Start () {
        
    }

    public void AddUnit(GameObject gameObjectUnit, bool playerUnit)
    {
        playerUnits.Add(gameObjectUnit);
    }
}
