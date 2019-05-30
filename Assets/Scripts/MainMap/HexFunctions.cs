using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexFunctions : MonoBehaviour
{
    [System.Serializable]
    public class SpawnObjects
    {
        public string name;
        public int rate;
    }

    public List<SpawnObjects> spawnTable = new List<SpawnObjects>();
    public int spawnChance;

    public void SpawnRate()
    {
        int calc_spawnChance = Random.Range(0, 101);
        if(calc_spawnChance > spawnChance)
        {
            Debug.Log("No Spawn. Rate was: " + calc_spawnChance);
            return;
        }
        if (calc_spawnChance <= spawnChance)
        {
            int spawnWeigth = 0;
            for (int i = 0; i < spawnTable.Count; i++)
            {
                spawnWeigth += spawnTable[i].rate;
            }
            Debug.Log("spawnWeigth = " + spawnWeigth);
            int randomValue = Random.Range(0, spawnWeigth);
            for (int i = 0; i < spawnTable.Count; i++)
            {
                if (randomValue <= spawnTable[i].rate)
                {
                    Debug.Log("Spawned: " + spawnTable[i].name + " rate was " + spawnTable[i].rate);
                    return;
                }
                //randomValue -= spawnTable[i].rate;
            }
        }
    }
}
