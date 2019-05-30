using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public float tileX;
    public float tileY;
    public Map map;

    void OnMouseUp()
    {
        Debug.Log("Click!");
        Debug.Log(tileX + " " + tileY);
        map.MoveUnitTo(tileX, tileY);
    }
}
