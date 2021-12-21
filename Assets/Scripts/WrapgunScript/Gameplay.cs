using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    
    Vector3 oldMousePos = new Vector3(0,0,0);
    //Vector3 checkMouseMove = new Vector3(0,0,0);
    bool isPressing = false;
    GameObject currentCell;
    int type = 0;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0)){
            // checkMouseMove = Input.mousePosition;
            //Debug.Log("mouse down");
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            if( Physics.Raycast( ray, out hit, 100 ) ){
                if(hit.transform.tag != "BuffCell") return;
                currentCell = hit.transform.gameObject;
                if(currentCell.name == "+1Cell"){
                    type = 1;
                } else if(currentCell.name == "+2Cell"){
                    type = 2;
                }else if(currentCell.name == "x2Cell"){
                    type = 3;
                }else if(currentCell.name == "x3Cell"){
                    type = 4;
                }
                isPressing = true;
            }
        }
        
        if(Input.GetMouseButtonUp(0)){
            //Debug.Log("mouse up");
            if(currentCell == null) return;
            oldMousePos = new Vector3(0,0,0);
            //currentCell.transform.position = new Vector3(currentCell.transform.position.x,0.3f,currentCell.transform.position.z);
            currentCell.GetComponent<SnapCellController>().Snap(type);
            isPressing = false;
        }

        if(isPressing){
            if(currentCell==null)   return;
            //Debug.Log(currentCell.name);
            if(oldMousePos.magnitude == 0){
                //Debug.Log("first click");
                oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            Vector3 moveVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - oldMousePos;
            //Debug.Log(moveVector);
            currentCell.transform.position += moveVector;
            currentCell.transform.position = new Vector3(currentCell.transform.position.x,1f,currentCell.transform.position.z);
            oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
