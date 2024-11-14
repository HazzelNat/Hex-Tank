using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Camera cam;

    TileScript lastHit;
    TankScript tankScript;
    MovableTile movableTile;
    void Start()
    {
        cam = GetComponent<Camera>();
        tankScript = GetComponent<TankScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
    }

    public void CheckClick()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            RaycastHit hit;
            Vector3 mousePos = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(mousePos);
            
            if(Physics.Raycast(ray, out hit)){
                Transform objectHit = hit.transform;

                if(objectHit.TryGetComponent<TileScript>(out TileScript hexTile)){
                    hexTile.OnSelected();
                    lastHit = hexTile;
                }
            }
        }
    }
}
