using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrapSpawningScript : MonoBehaviour
{
    public List<Vector3Int> scrapLocations = new List<Vector3Int>();
    public Tilemap scrapTilemap;
    public GameObject scrap;
    
    // Start is called before the first frame update
    void Start()
    {
        ScrapLocations();
        for (int i = 0; i < scrapLocations.Count; i++)
        {
            SpawnScrap(scrapLocations[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnScrap(Vector3Int location)
    {
        Instantiate(scrap, location, Quaternion.identity);
    }

    void ScrapLocations()
    {
        scrapLocations.Add(new Vector3Int(37, -3, 0));
        scrapLocations.Add(new Vector3Int(-3, -20, 0));
        scrapLocations.Add(new Vector3Int(-3, -19, 0));
        scrapLocations.Add(new Vector3Int(24, 39, 0));
        scrapLocations.Add(new Vector3Int(-8, -5, 0));
        scrapLocations.Add(new Vector3Int(-30, -12, 0));
    }
}
