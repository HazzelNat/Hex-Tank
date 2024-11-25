using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Camera cam;

    TileScript lastHit;
    TankScript tankScript;
    public Transform objectHit;
    [SerializeField] private LayerMask placementLayerMask;
    public Vector3 worldPosition;

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
        mousePos.z = cam.nearClipPlane + 1;

        ray = cam.ScreenPointToRay(mousePos);

        worldPosition = cam.ScreenToWorldPoint(mousePos);
        
        if(Physics.Raycast(ray, out hit, 100, placementLayerMask)){
            objectHit = hit.transform;

            if(Input.GetMouseButtonDown(0)){
                if(objectHit.TryGetComponent<TileScript>(out TileScript hexTile)){
                    hexTile.OnSelected();
                    lastHit = hexTile;
                }
            }
        }
    }
}
