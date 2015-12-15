using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

    public float camMoveSpeed = 10;
    public float camRotateSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //forward
        if (Input.GetKey(keyBindings.camForward))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * camMoveSpeed);
        }
        else if (Input.mousePosition.y >= Screen.height)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * camMoveSpeed);
        }
        //backward
        if (Input.GetKey(keyBindings.camBack))
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * camMoveSpeed);
        }
        else if (Input.mousePosition.y <= 0)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * camMoveSpeed);
        }
        //left
        if (Input.GetKey(keyBindings.camLeft))
        {
            transform.Translate(-Vector3.right * Time.deltaTime * camMoveSpeed);
        }
        else if (Input.mousePosition.x <= 0)
        {
            transform.Translate(-Vector3.right * Time.deltaTime * camMoveSpeed);
        }
        //right
        if (Input.GetKey(keyBindings.camRight))
        {
            transform.Translate(Vector3.right * Time.deltaTime * camMoveSpeed);
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            transform.Translate(Vector3.right * Time.deltaTime * camMoveSpeed);
        }
        //rotate left
        if (Input.GetKey(keyBindings.camRotateLeft))
        {
            transform.Rotate(Vector3.down * Time.deltaTime * camRotateSpeed);
        }
        //rotate right
        if (Input.GetKey(keyBindings.camRotateRight))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * camRotateSpeed);
        }
    }
}
