using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexGenerate : MonoBehaviour
{
    HexGrid gridInfo;
    Vector3 hexLocation;
    GameObject spawnedTiles;
    public List<GameObject> hexGrids = new List<GameObject>();
    void Start()
    {
        gridInfo = GetComponent<HexGrid>();
        GenerateMap(gridInfo.width, gridInfo.height, gridInfo.size);
    }

    
    void Update()
    {
        
    }

    public void GenerateMap(float width, float height, float hexSize)
    {
        for(int i=0 ; i<width*2 ; i+=2){
            for(int j=0 ; j<height ; j++){
                AddHexTile(i, j, hexSize);
            }
        }
    }

    public void AddHexTile(float x, float z, float hexSize){
        if(hexSize%2 == 1){
            hexSize -= 1;
        }

        if(z%2 == 0){
            x -= hexSize;
            z -= Mathf.RoundToInt((hexSize/2)-1);
            hexLocation = new Vector3(x, 1f, z*1.73f);
            spawnedTiles = Instantiate(gridInfo.hexPrefab, hexLocation, transform.rotation);
            FormatHex(x, z);
        } else if(z%2 == 1){
            x -= hexSize;
            z -= Mathf.RoundToInt((hexSize/2)-1);
            hexLocation = new Vector3(x+1, 1f, z*1.73f);
            spawnedTiles = Instantiate(gridInfo.hexPrefab, hexLocation, transform.rotation);
            FormatHex(x+1, z);
        }
        
        
    }

    private void FormatHex(float x, float z)
    {
        string xCoordinates = x.ToString();                                                         // Change name
        string zCoordinates = z.ToString();
        spawnedTiles.name = string.Format("Tile {0}, {1}", xCoordinates, zCoordinates);
        
        TileScript spawnedTileScript = spawnedTiles.GetComponent<TileScript>();

        spawnedTileScript.xCoordinate = Mathf.RoundToInt(x);
        spawnedTileScript.zCoordinate = Mathf.RoundToInt(z);

        spawnedTiles.transform.parent = transform;                                                  // Add as a child of HexGrid

        hexGrids.Add(spawnedTiles);                                                                 // Add to list of hex
    }
}
