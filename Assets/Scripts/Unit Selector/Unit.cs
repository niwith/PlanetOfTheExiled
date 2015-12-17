using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool selected = false;
    public NavMeshAgent agent;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraOporator.InvertMouseY(camPos.y);
            selected = CameraOporator.selection.Contains(camPos);
            if (selected)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
        }

        if (selected && Input.GetMouseButtonUp(1))
        {
            Vector3 destination = CameraOporator.GetDestination();

            if (destination != Vector3.zero)
            {
                agent.SetDestination(destination);
                print("Navmesh Added");
            }
        }
    }
}