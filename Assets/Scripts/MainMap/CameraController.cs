using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = -10f;
    public float panBorderThicc = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 20f;
    public float minZ = -5f;
    public float maxZ = -20f;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButton(1))
        {
            pos.x -= Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            pos.y -= Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.z -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, maxZ, minZ);
        pos.y = Mathf.Clamp(pos.y, -5, panLimit.y);

        transform.position = pos;
    }
}