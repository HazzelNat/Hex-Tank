using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SelectedTile : MonoBehaviour
{
    [Header("Camera")]
    public PlayerController playerController;
    public GameObject cam;

    [Header("Selected Tile")]
    public GameObject selectedShootTile;
    public GameObject selectedMoveTile;
    public GameObject currentSelectedTile;
    private List<GameObject> hexGrids;

    [Header("Tanks")]
    public List<GameObject> alliedTanks = new List<GameObject>();
    public GameObject selectedTank;
    TankScript tankScript;
    
    [Header("Movement Tiles")]
    [SerializeField] 
    private int movementLength;
    private bool movementCheck;
    public List<GameObject> highlightedMovementTile;

    void Start()
    {
        tankScript = alliedTanks[0].GetComponent<TankScript>();
        hexGrids = GetComponent<HexGenerate>().hexGrids;
        playerController = cam.GetComponent<PlayerController>();
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

        if(tankScript.state == TankScript.State.ClickToShoot){
            HighlightShoot();
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
                    for(int i=0 ; i<highlightedMovementTile.Count ; i++){
                        if(highlightedHex.transform.parent.name == highlightedMovementTile[i].name){
                            movementCheck = true;

                            currentSelectedTile.SetActive(false);
                            currentSelectedTile = selectedMoveTile;
                            currentSelectedTile.SetActive(true);

                            tankScript.selectedMoveTile = selectedMoveTile;
                        }
                    }

                    if(movementCheck){
                        ClearHighlightMovement();
                        movementCheck = false;
                    }

                    break;
                
                case TankScript.State.Moving:
                    
                    break;

                 case TankScript.State.ClickToShoot:

                    selectedShootTile = highlightedHex;

                    selectedShootTile.SetActive(true);
                    tankScript.selectedShootTile = selectedShootTile;

                    break;

                case TankScript.State.Shooting:

                    break;
            }
        }
        
    }

    public void SelectedTank(GameObject tank)
    {
        selectedTank = tank;
        tankScript.selectedTank = tank;
    }

    public void HighlightMovement()
    {
        foreach(GameObject tile in hexGrids){
            float centerGridZ = currentSelectedTile.transform.position.z/1.73f;
            float tileZ = tile.transform.position.z/1.73f;

            if(Mathf.Abs(tileZ - centerGridZ) <= movementLength){
                if(Mathf.Abs(tile.transform.position.x - currentSelectedTile.transform.position.x) < movementLength*2){
                    highlightedMovementTile.Add(tile);
                    tile.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            
            if(tileZ == centerGridZ){
                if(Mathf.Abs(tile.transform.position.x - currentSelectedTile.transform.position.x) == movementLength*2){
                    highlightedMovementTile.Add(tile);
                    tile.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }

    public void ClearHighlightMovement()
    {
        foreach(GameObject tile in highlightedMovementTile){
            tile.transform.GetChild(0).gameObject.SetActive(false);
            highlightedMovementTile = null;
        }
    }

    public void HighlightShoot()
    {
        if(playerController.objectHit.transform.position.z == currentSelectedTile.transform.position.z){
            if(playerController.objectHit.transform.position.x > currentSelectedTile.transform.position.x){
                Debug.Log("Kanan");
            } else {
                Debug.Log("Kiri");
            }
        }

        if(playerController.objectHit.transform.position.z > currentSelectedTile.transform.position.z){
            if(playerController.objectHit.transform.position.x > currentSelectedTile.transform.position.x){
                Debug.Log("Kanan ATAS");
            } else {
                Debug.Log("Kiri ATAS");
            }
        }
    }

    // public void GetShootTile(int index)
    // {
    //     for(int i=)
    // }
}
