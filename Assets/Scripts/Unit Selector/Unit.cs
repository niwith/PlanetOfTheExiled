using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public bool selected = false;
    public NavMeshAgent agent;
    private bool selectedByClick = false;

    public objectList listScript;

    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!selectedByClick)
            {
                Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
                camPos.y = CameraOporator.InvertMouseY(camPos.y);
                selected = CameraOporator.selection.Contains(camPos);
            }
            if (selected)
            {
                gameObject.GetComponentInChildren<Projector>().enabled = true;
            }
            else
            {
                gameObject.GetComponentInChildren<Projector>().enabled = false;
            }
        }

        if (selected && Input.GetMouseButtonUp(1))
        {
            Vector3 destination = CameraOporator.GetDestination();

            if (destination != Vector3.zero)
            {
                agent.SetDestination(destination);
            }
        }
    }

    private void OnMouseDown()
    {
        selectedByClick = true;
        selected = true;
    }

    private void OnMouseUp()
    {
        if (selectedByClick)
        {
            selected = true;
        }
        selectedByClick = false;
    }
}