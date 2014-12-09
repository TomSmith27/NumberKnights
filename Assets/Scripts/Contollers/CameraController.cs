using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;


    void Update()
    {
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            return;
        if (Input.mousePosition.x < 20 && !Input.GetMouseButtonDown(0))
        {
            transform.Translate(new Vector3(-1, 0 ,0), Space.World);
        }
        else if (Input.mousePosition.x > Screen.width - 20 && !Input.GetMouseButtonDown(0))
        {

            transform.Translate(new Vector3(1, 0, 0), Space.World);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0,0);

        transform.Translate(-move, Space.World);  
    }


}