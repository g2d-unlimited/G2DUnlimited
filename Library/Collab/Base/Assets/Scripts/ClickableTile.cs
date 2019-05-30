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
        map.MoveUnitTo(tileX, tileY);
    }
}
