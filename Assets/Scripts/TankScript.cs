using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class TankScript : MonoBehaviour
{
    public enum State{
        Idle,
        Selected,
        ClickToMove,
        Moving,
        ClickToShoot,
        Shooting,
        Waiting
    }

    [Header("Tank Component")]
    public GameObject selectedTank;
    public GameObject bullet;
    public GameObject firePoint;
    float[] allowedRotation = {0, };

    [Header("UI Tank Component")]
    public GameObject tankPanel;
    public Button moveButton;
    public Button shootButton;

    [Header("Tile Component")]
    SelectedTile selectedTileScript;
    public GameObject selectedTile;
    public GameObject selectedMoveTile;
    public GameObject selectedMoveTileParent;
    public GameObject selectedShootTile;
    HexGenerate hexGenerate;

    [Header("Camera Component")]
    [SerializeField] public GameObject cam;
    PlayerController playerController;
    RaycastHit hit;
    Transform objectHit;

    [Header("Checks")]
    public State state;
    public bool alreadyMoved = false;
    public bool alreadyShot = false;
    public bool alreadyHit = false;
    public bool myBullet = false;

    void Start()
    {
        hexGenerate = GetComponent<HexGenerate>();
        playerController = cam.GetComponent<PlayerController>();
    }

    void Update()
    {
        CheckState();

        if(Input.GetKeyDown(KeyCode.A)){
            Time.timeScale = 0f;
        } else if(Input.GetKeyDown(KeyCode.S)){
            Time.timeScale = 1f;
        }
    }
    private void FixedUpdate() {
        StateBehaviour();
    }

    public void CheckState()
    {
        if(selectedTank && state == State.Idle){
            ChangeState(State.Selected);
        } else if(!selectedTank && state == State.Selected){
            ChangeState(State.Idle);
        }
    }

    public void StateBehaviour()
    {
        switch(state){
            case State.Idle:
                Idle();
                break;

            case State.Selected:
                Selected();
                break;

            case State.ClickToMove:
                if(selectedMoveTile != null){
                    ChangeState(State.Moving);
                }

                RotateTank();

                break;

            case State.Moving:
                Move();
                
                break;

            case State.ClickToShoot:
                if(selectedShootTile != null){
                    Debug.Log("Ganti Shoot");
                    ChangeState(State.Shooting);
                }

                RotateTank();

                break;    

            case State.Shooting:
                Shoot();
                break;

            case State.Waiting:
                RestartValue();
                break;    
        }
    }

    public void Idle()
    {
        tankPanel.SetActive(false);
    }

    public void Selected()
    {
        tankPanel.SetActive(true);

        moveButton.onClick.AddListener(OnClickMove);
        
        shootButton.onClick.AddListener(OnClickShoot);
    }

    public void Move(){
        selectedMoveTileParent = selectedMoveTile.transform.parent.gameObject;

        if(selectedMoveTileParent.transform.position.x == selectedTank.transform.position.x && selectedMoveTileParent.transform.position.z == selectedTank.transform.position.z){
            selectedMoveTile = null;
            selectedMoveTileParent = null;
            alreadyMoved = true;
            ChangeState(State.Selected);
            
        } else {
            selectedTank.transform.position = new Vector3(selectedMoveTileParent.transform.position.x, 1, selectedMoveTileParent.transform.position.z);
        }

        if(alreadyMoved){
            moveButton.interactable = false;
        }
    }

    public void Shoot(){
        if(!alreadyShot){
            Instantiate(bullet, firePoint.transform.position, Quaternion.Euler(90f, 0f, 90f));
            
            alreadyShot = true;
        } else {
            if(alreadyHit){
                selectedShootTile.SetActive(false);
                selectedTile.SetActive(false);
                ChangeState(State.Waiting);
            }
        }
    }

    private void OnClickMove()
    {
        ChangeState(State.ClickToMove);
    }

    private void OnClickShoot()
    {
        ChangeState(State.ClickToShoot);
    }

    public void ChangeState(State currentState)
    {
        state = currentState;
    }

    void RotateTank()
    {
        
    }

    public void RestartValue()
    {
        alreadyMoved = false;
        alreadyShot = false;
        alreadyHit = false;
    }
}
