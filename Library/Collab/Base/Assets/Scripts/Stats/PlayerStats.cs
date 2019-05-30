using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : CharStats
{
    public Map map;
    public Fight fight;
    [SerializeField]
    int currentTileRow { get; set; }
    [SerializeField]
    int currentTileColumn { get; set; }
    [SerializeField]
    string currentTileMaterial { get; set; }

    [SerializeField]
    public CharStats health;

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
        //map.SpawnPlayer();
        health.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        //map.SpawnPlayer();
        //health.Initialize();
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
    //override public void Die()
    //{
    //    map.SpawnPlayer();
    //    Revive();
    //}

}
