using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    public readonly int Q; //Kolona
    public readonly int R; //Eile
    public readonly int S; 
    static readonly float WIDTH_MULT = Mathf.Sqrt(3) / 2; // Math hexagonu deliojimui
    float radius = 1f;
    //Konstruktorius hexagonui
    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = (-q + r);
    }
    //Nustato pozicija i kuria deti
    public Vector3 Position()
    {
        
        float horizontal = HexWidth();
        float verical = HexHeight() * 3 / 4;
        return new Vector3(
            HexHorizontalSpacing() * (Q + R / 2f),
            HexVerticalSpacing() * R,
            0);
    }
    public float HexHeight()
    {
        return radius * 2;
    }
    public float HexWidth()
    {
        return WIDTH_MULT * HexHeight();
    }
    public float HexVerticalSpacing()
    {
        return HexHeight()*0.75f;
    }
    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }
    public Vector3 PositionFromCamera(Vector3 cameraPos, float numRows, float numColumns)
    {
        //float mapHeight = numRows * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();

        Vector3 position = Position();
        float hmwfc = (position.x - cameraPos.x) / mapWidth;
        if(Mathf.Abs(hmwfc) <=0.5f)
        {
            return position;
        }
        
        int hmwtf = (int)Mathf.Round(hmwfc);
        position.x -= hmwtf * mapWidth;
        return position;
    }
}
