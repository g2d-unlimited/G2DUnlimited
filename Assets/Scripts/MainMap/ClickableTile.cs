using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{

    public GameObject hex;
    public Map map;
    public HexFunctions hexf;


    void OnMouseUp()
    {
        try
        {
            map.MovePlayerTo(hex);
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log("You can't move here");
        }
    }
}
