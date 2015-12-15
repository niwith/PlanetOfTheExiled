using UnityEngine;
using System.Collections;

public class cameraHeightControl : MonoBehaviour {

    public float cameraHeight = 20;
    public float difference = 0;
    public float cameraAjustSpeed = 10;
    public float minHeight = 20;
    public float maxHeight = 200;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        cameraHeight += Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime*cameraAjustSpeed*-1000;
        
        if(cameraHeight < minHeight)
        {
            cameraHeight = minHeight;
        }
        else if(cameraHeight > maxHeight)
        {
            cameraHeight = maxHeight;
        }

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit))
        {
            difference = cameraHeight - hit.distance;
        }

        float newHeight = transform.position.y + difference;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
