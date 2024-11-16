using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectedTile : MonoBehaviour
{
    public List<GameObject> alliedTanks = new List<GameObject>();

    public GameObject selectedShootTile;
    public GameObject selectedMoveTile;
    public GameObject currentSelectedTile;
    public GameObject tankPanel;
    public GameObject selectedTank;
    TankScript tankScript;
    HexGenerate hexGenerate;
    public List<GameObject> hexGrids;

    void Start()
    {
        tankScript = alliedTanks[0].GetComponent<TankScript>();
        hexGrids = GetComponent<HexGenerate>().hexGrids;
    }

    void Update()
    {
        if(currentSelectedTile && tankScript.state == TankScript.State.Idle || tankScript.state == TankScript.State.Selected){
            for(int i=0 ; i<alliedTanks.Count ; i++){
                if(currentSelectedTile.transform.position.x == alliedTanks[i].transform.position.x && currentSelectedTile.transform.position.z == alliedTanks[i].transform.position.z){
                    SelectedTank(alliedTanks[i]);
                } else {
                    SelectedTank(null);
                }
            }
        }

        if(tankScript.state == TankScript.State.ClickToMove){
            HighlightNeighbours(currentSelectedTile);
        }
    }

    public void UpdateTileHighlight(GameObject highlightedHex)
    {
        if(selectedTank == null){
            if(currentSelectedTile == null){
                currentSelectedTile = highlightedHex;
                currentSelectedTile.SetActive(true);
            }

            if(currentSelectedTile.activeSelf == true){
                currentSelectedTile.SetActive(true);
            }

            if(currentSelectedTile != highlightedHex){
                currentSelectedTile.SetActive(false);
                currentSelectedTile = highlightedHex;
                currentSelectedTile.SetActive(true);
            }

        } else {
            switch(tankScript.state){
                case TankScript.State.Selected:
                    currentSelectedTile.SetActive(false);
                    currentSelectedTile = highlightedHex;
                    currentSelectedTile.SetActive(true);
                    break;
                
                case TankScript.State.ClickToMove:
                    selectedMoveTile = highlightedHex;

                    currentSelectedTile.SetActive(false);
                    currentSelectedTile = selectedMoveTile;
                    currentSelectedTile.SetActive(true);

                    tankScript.selectedMoveTile = selectedMoveTile;

                    break;
                
                case TankScript.State.Moving:
                    
                    break;

                 case TankScript.State.ClickToShoot:
                    selectedShootTile = highlightedHex;

                    selectedShootTile.SetActive(true);
                    tankScript.selectedShootTile = selectedShootTile;

                    break;

                case TankScript.State.Shooting:
                    Debug.Log("Still Shooting");
                    break;
            }
        }
        
    }

    public void SelectedTank(GameObject tank)
    {
        selectedTank = tank;
        tankScript.selectedTank = tank;
    }

    

    public void HighlightNeighbours(GameObject centerGrid)
    {
        
    }
}
