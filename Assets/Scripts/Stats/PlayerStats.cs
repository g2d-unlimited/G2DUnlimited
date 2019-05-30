using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : CharStats
{
    public Map map;
    public Fight fight;

    //Buves hexagonas
    public int previousTileRow { get; set; }
    public int previousTileColumn { get; set; }

    [SerializeField]
    int currentTileRow { get; set; }
    [SerializeField]
    int currentTileColumn { get; set; }
    [SerializeField]
    string currentTileMaterial { get; set; }

    [SerializeField]
    public CharStats health;




    public List<BaseAttack> abilities = new List<BaseAttack>();

    public int getCurrentTileColumn()
    {
        return currentTileColumn;
    }
    public int getCurrentTileRow()
    {
        return currentTileRow;
    }
    public void setCurrentTileColumn(int column)
    {
        currentTileColumn = column;
    }
    public void setCurrentTileRow(int row)
    {
        currentTileRow= row;
    }
    public int getPreviousTileColumn()
    {
        return previousTileColumn;
    }
    public int getPrevioustTileRow()
    {
        return previousTileRow;
    }
    public void setPreviousTileColumn(int column)
    {
        previousTileColumn = column;
    }
    public void setPreviousTileRow(int row)
    {
        previousTileRow = row;
    }
    public void setCurrentTileMaterial(string material)
    {
        currentTileMaterial = material;
    }
    public string getCurrentTileMaterial()
    {
        return currentTileMaterial;
    }

    public void Awake()
    {
        health.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            health.CurrentValueHP -= 8;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            health.CurrentValueHP += 5;
        }
    }
}
