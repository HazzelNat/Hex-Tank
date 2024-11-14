using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    Animator anim;
        // 0 for Selected animation

    public GameObject highlightedHex;
    SelectedTile selectedTileScript;
    GameObject hexGrid;
    public GameObject tank;

    TankScript tankScript;
    GameManager gameManager;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        hexGrid = GameObject.FindGameObjectWithTag("Grid");

        Debug.Log(hexGrid.transform);

        selectedTileScript = hexGrid.GetComponent<SelectedTile>();
        tankScript = tank.GetComponent<TankScript>();
    }

    private void Update()
    {
        
    }

    public void OnSelected()
    {
        highlightedHex = gameObject.transform.GetChild(0).gameObject;
        anim.Play(0);
        
        selectedTileScript.UpdateTileHighlight(highlightedHex);
        // selectedTileScript.UpdateTileHighlight(highlightedHex);
        // selectedTileScript.currentSelectedTile = highlightedHex;
    }
}
