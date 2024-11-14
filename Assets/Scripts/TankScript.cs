using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class TankScript : MonoBehaviour
{
    public GameObject selectedTank;
    public GameObject selectedTile;
    SelectedTile selectedTileScript;
    public GameObject tankPanel;
    public GameObject selectedMoveTile;
    public GameObject selectedMoveTileParent;
    public GameObject selectedShootTile;
    HexGenerate hexGenerate;

    public Button moveButton;
    public Button shootButton;

    public enum State{
        Idle,
        Selected,
        ClickToMove,
        Moving,
        ClickToShoot,
        Shooting,
        Waiting
    }

    public State state;
    PlayerController playerController;
    bool alreadyMoved = false;

    void Start()
    {
        hexGenerate = GetComponent<HexGenerate>();
    }

    void Update()
    {
        CheckState();
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
                if(selectedMoveTile == null){
                    Debug.Log("Kosong klik");
                } else if(selectedMoveTile != null){
                    ChangeState(State.Moving);
                    Debug.Log("Target Locked");
                }
                break;

            case State.Moving:
                Move();
                
                break;

            case State.Shooting:

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
        // Vector3 target = selectedMoveTile.position;

        selectedMoveTileParent = selectedMoveTile.transform.parent.gameObject;

        if(selectedMoveTileParent.transform.position.x != selectedTank.transform.position.x && selectedMoveTileParent.transform.position.z != selectedTank.transform.position.z){
            selectedTank.transform.position = new Vector3(selectedMoveTileParent.transform.position.x, 1, selectedMoveTileParent.transform.position.z);
        } else if(selectedMoveTileParent.transform.position.x == selectedTank.transform.position.x && selectedMoveTileParent.transform.position.z == selectedTank.transform.position.z){
            selectedMoveTile = null;
            selectedMoveTileParent = null;
            alreadyMoved = true;
            ChangeState(State.Selected);
        }

        if(alreadyMoved){
            moveButton.interactable = false;
        }
    }

    public void Shoot(){

    }

    private void OnClickMove()
    {
        Debug.Log("KLIK JALAN");
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

    public void RestartValue()
    {
        alreadyMoved = false;
    }
}
