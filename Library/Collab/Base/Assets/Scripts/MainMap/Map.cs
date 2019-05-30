using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject[] selectedUnit;
    public GameObject hexPrefab;

    public Material[] hexMaterials;
    public GameObject[,] hexes;
    public ClickableTile ct;
    public HexFunctions hexFunction;
    public int count = 0;

    //[SerializeField]
    public Text valueText;

    public PlayerStats[] stats;

    public Camera mainCamera;
    public Text winText;

    public int numRows = 15;
    public int numColumns = 20;

    [SerializeField]
    private int numbOfPlayers;
    private int playerid = 0;

    // Use this for initialization
    void Start()
    {
        GenerateMap();
        SpawnVault();
        SpawnPlayer();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TurnCheck()
    {
        if (playerid - numbOfPlayers + 1 < 0)
        {
            playerid++;
        }
        else
        {
            playerid = 0;
            if(playerid == 0)
            {
                count++;
            }
        }
    }
    public void GenerateMap()
    {
        hexes = new GameObject[numColumns, numRows];
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
                meshRenderer.material = hexMaterials[Random.Range(0, hexMaterials.Length-1)];
                hexes[column, row] = hexGameObject;

            }
        }
    }

    public void HexFunctions(GameObject hex)
    {

    }

    public void SpawnVault()
    {
        int column = Random.Range(0, numColumns);
        int row = Random.Range(0, numRows);
        Destroy(hexes[column, row]);
        Hex hex = new Hex(column, row);
        GameObject hexGameObject = Instantiate(
            hexPrefab,
            hex.Position(),
            Quaternion.identity,
            this.transform);
        hexGameObject.name = "Hex_" + row + "_" + column;
        MeshRenderer meshRenderer = hexGameObject.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = hexMaterials[6];
        hexes[column, row] = hexGameObject;
    }

    public void AvailableTile()
    {
        int cColumn = stats[playerid].getCurrentTileColumn();
        int cRow = stats[playerid].getCurrentTileRow();

        foreach (GameObject hexas in hexes)
        {
            ct = hexas.GetComponent<ClickableTile>();
            ct.hex = null;
            ct.map = null;
        }

        try
        {
            ct = hexes[cColumn + 1, cRow].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn + 1, cRow];
            ct.map = this;
        }
        catch(System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn + 1, cRow]");
        }
        try
        {
            ct = hexes[cColumn, cRow + 1].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn, cRow + 1];
            ct.map = this;
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn, cRow + 1]");
        }
        try
        {
            ct = hexes[cColumn - 1, cRow + 1].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn - 1, cRow + 1];
            ct.map = this;
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn - 1, cRow + 1]");
        }
        try
        {
            ct = hexes[cColumn - 1, cRow].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn - 1, cRow];
            ct.map = this;
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn - 1, cRow]");
        }
        try
        {
            ct = hexes[cColumn, cRow - 1].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn, cRow - 1];
            ct.map = this;
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn, cRow - 1]");
        }
        try
        {
            ct = hexes[cColumn + 1, cRow - 1].GetComponent<ClickableTile>();
            ct.hex = hexes[cColumn + 1, cRow - 1];
            ct.map = this;
        }
        catch (System.IndexOutOfRangeException e)
        {
            Debug.Log("Out of range [cColumn + 1, cRow - 1]");
        }
    }

    public void MovePlayerTo(GameObject hex)
    {
        string temp = hex.name.Replace("Hex_", "");
        CheckWin();
        int row = int.Parse(temp.Split('_').First());
        int column = int.Parse(temp.Split('_').Last());

        stats[playerid].setCurrentTileColumn(column);
        stats[playerid].setCurrentTileRow(row);
        stats[playerid].setCurrentTileMaterial(hex.GetComponentInChildren<Renderer>().
            material.name.Split(' ').First().ToString());
        //mainCamera.transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y - 5, -15);
        selectedUnit[playerid].transform.position = new Vector3(hex.transform.position.x, hex.transform.position.y, 0);
        Debug.Log(hex.transform.position.x + " " + hex.transform.position.y);
        TurnCheck();
        AvailableTile();
        CheckWin();
        HealthRegen();
    }

    public void HealthRegen()
    {
        stats[playerid].health.CurrentValueHP += 10; 
    }
    public void SpawnPlayer()
    {
        GameObject randomHex = hexes[Random.Range(0, numColumns), Random.Range(0, numRows)];
        stats[playerid].map = this;
        MovePlayerTo(randomHex);
    }
    public void CheckWin()
    {
        int nr;
        if(stats[0].getCurrentTileMaterial() == "Vault")
        {
            nr = playerid + 1;
            winText.text = "Player " + 1 + " wins";
        }
        else if(stats[1].getCurrentTileMaterial() == "Vault")
        {
            winText.text = "Player " + 2 + " wins";
        }
        else
        {
            nr = playerid + 1;
            valueText.text = "Player " + nr;
            winText.text = "";
        }
    }

}