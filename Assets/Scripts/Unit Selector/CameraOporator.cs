using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraOporator : MonoBehaviour {

    public Texture2D selectionHighlight = null;
    public static Rect selection = new Rect();
    private Vector3 startClick = -Vector3.one;
    public Color borderColor = new Color(0.8f, 0.8f, 0.95f);
    public float borderWidth = 1f;
    public Camera camera;

    private static Vector3 moveToDestination = Vector3.zero;
    private static List<string> passables = new List<string>() { "Floor" };

	// Update is called once per frame
	void Update () {
        CheckCamera();
        Cleanup();
	}

    private void CheckCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            startClick = -Vector3.one;
        }
        if(Input.GetMouseButton(0))
        {
            selection = new Rect(startClick.x, InvertMouseY(startClick.y), Input.mousePosition.x - startClick.x, InvertMouseY(Input.mousePosition.y) - InvertMouseY(startClick.y));
            if (selection.width < 0)
            {
                selection.x += selection.width;
                selection.width = -selection.width;
            }
            if (selection.height < 0)
            {
                selection.y += selection.height;
                selection.height = -selection.height;
            }
        }

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.SendMessageUpwards("selectByClick", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, selectionHighlight);
        GUI.color = Color.white;
    }

    public void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    private void OnGUI()
    {
        if(startClick != -Vector3.one)
        {
            GUI.color = new Color(0.8f, 0.8f, 0.95f, 0.25f);
            GUI.DrawTexture(selection, selectionHighlight);
            DrawScreenRectBorder(selection, borderWidth, borderColor);
        }
    }

    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }

    private void Cleanup()
    {
        if(!Input.GetMouseButtonUp(1))
        {
            moveToDestination = Vector3.zero;
        }
    }

    public static Vector3 GetDestination()
    {
        if (moveToDestination == Vector3.zero)
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(r, out hit))
            {
                while(!passables.Contains(hit.transform.gameObject.name))
                {
                    if(Physics.Raycast(hit.point, r.direction, out hit))
                    {
                        break;
                    }
                }
            }
            if(hit.transform != null)
            {
                moveToDestination = hit.point;
            }
        }
        return moveToDestination;
    }
}
