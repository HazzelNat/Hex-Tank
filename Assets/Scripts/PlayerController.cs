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
    public Transform objectHit;

    public Ray ray;
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
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        ray = cam.ScreenPointToRay(mousePos);

        if(Input.GetMouseButtonDown(0)){
            if(Physics.Raycast(ray, out hit)){
                objectHit = hit.transform;

                if(objectHit.TryGetComponent<TileScript>(out TileScript hexTile)){
                    hexTile.OnSelected();
                    lastHit = hexTile;
                }
            }
        }
    }
}
