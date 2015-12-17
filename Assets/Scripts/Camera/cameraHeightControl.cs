using UnityEngine;
using System.Collections;

public class cameraHeightControl : MonoBehaviour {

    public float cameraHeight = 20;
    public float difference = 0;
    public float cameraAjustSpeed = 10;
    public float minHeight = 20;
    public float maxHeight = 200;

    public LayerMask layerMask = 1 << 8;
	
	// Update is called once per frame
	void Update () {
        //adjust by scroll wheel
        cameraHeight += Input.GetAxis("Mouse ScrollWheel")*Time.deltaTime*cameraAjustSpeed*-1000;
        //adjust by button
        if(Input.GetKey(keyBindings.camUp))
        {
            cameraHeight += Time.deltaTime * -cameraAjustSpeed * 100;
        }
        if (Input.GetKey(keyBindings.camDown))
        {
            cameraHeight += Time.deltaTime * cameraAjustSpeed * 100;
        }
        //check for max and min
        if (cameraHeight < minHeight)
        {
            cameraHeight = minHeight;
        }
        else if(cameraHeight > maxHeight)
        {
            cameraHeight = maxHeight;
        }
        //find difference between set cam height and actual height
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit, layerMask.value))
        {
            difference = cameraHeight - hit.distance;
        }
        //change camera height
        float newHeight = transform.position.y + difference;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
