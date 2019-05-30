using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject selectedUnit;
    public GameObject hexPrefab;
    public Material[] hexMaterials;
    public GameObject[,] hexes;
    //public GameObject current = hexes[0, 0];
    //public int cRow = 0;
    //public int cColumn = 0;

    public int numRows = 15;
    public int numColumns = 20;

    // Use this for initialization
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void GenerateMap()
    {
        hexes = new GameObject[numColumns, numRows];
        int cColumn = 0;
        int cRow = 0;
        int wColumn1 = 0;
        int wRow1 = 1;
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex hex = new Hex(column, row);
                GameObject hexGameObject = Instantiate(
                    hexPrefab,
                    hex.Position(),
                    Quaternion.identity,
                    this.transform);
                hexGameObject.name = "Hex_" + row + "_" + column;
                MeshRenderer meshRenderer = hexGameObject.GetComponentInChildren<MeshRenderer>();
                meshRenderer.material = hexMaterials[Random.Range(0, hexMaterials.Length)];
                hexes[column, row] = hexGameObject;
                //if ((cColumn == column) && (cRow + 1 == row) || (cColumn == column) && (cRow - 1 == row) || (cColumn - 1 == column) && (cRow == row) || (cColumn - 1 == column) && (cRow + 1 == row) || (cColumn + 1 == column) && (cRow == row) || (cColumn + 1 == column) && (cRow - 1 == row))
                //{
                //    cColumn = column;
                //    cRow = row;
                //    Debug.Log(cColumn + "*****" + cRow);
                //    ClickableTile ct = hexes[column, row].GetComponent<ClickableTile>();
                //    ct.tileX = hexes[column, row].transform.position.x;
                //    ct.tileY = hexes[column, row].transform.position.y;
                //    ct.map = this;
                //}
            }
        }

    }

    public void AvailableTile()
    {
        int cColumn = 0;
        int cRow = 0;
        int wColumn1 = 0;
        int wRow1 = 1;
        if ((cColumn == wColumn1) && (cRow + 1 == wRow1))
        {
            cColumn = wColumn1;
            cRow = wRow1;
            Debug.Log(cColumn + "*****" + cRow);
            ClickableTile ct = hexes[wColumn1, wRow1].GetComponent<ClickableTile>();
            ct.tileX = hexes[wColumn1, wRow1].transform.position.x;
            ct.tileY = hexes[wColumn1, wRow1].transform.position.y;
            ct.map = this;
        }
    }

    public void MoveUnitTo(float x, float y)
    {
        selectedUnit.transform.position = new Vector3(x, y, 0);
        Debug.Log(x + " " + y);
    }
}