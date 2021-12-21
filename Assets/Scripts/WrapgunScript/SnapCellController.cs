using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCellController : MonoBehaviour
{

    Vector3 oldPosition;
    bool isPlace = false;
    float holdingBullet = 0;
    int type = 0;
    int bulletLeft = 0;

    // Update is called once per frame
    private void Start() {
        oldPosition = gameObject.transform.position;
    }

    void Update()
    {
        // RaycastHit hit;
        // if( Physics.Raycast( gameObject.transform.position,-transform.up, out hit, 10f )){
        //     if(hit.transform.tag !="Cell")  return;
        //     if(Vector3.Distance(gameObject.transform.position,hit.transform.position)<0.2f){
        //         gameObject.transform.position = new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z);
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.gameObject.name);
        if(this.type == 1 ){
            CountPlus1(other.gameObject);
        }else if(this.type == 2){
            CountPlus2(other.gameObject);
        }else if(this.type == 3){
            CountMultiIn(other.gameObject);
        }else if(this.type == 4){
            CountMultiIn(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(this.type == 3 ){
            CountMultiOut(other.gameObject);
        }else if(this.type == 4){
            CountMultiOut(other.gameObject);
        }
    }

    public void Snap(int type){
        if(type ==1){
            if(this.type ==0){this.type = 1;}
            SnapPlus1();
        }else if(type == 2){
            if(this.type ==0){this.type = 2;}
            SnapPlus2();
        }else if(type == 3){
            if(this.type ==0){this.type = 3;}
            SnapMulti2();
        }else if(type == 4){
            if(this.type ==0){this.type = 4;}
            SnapMulti3();
        }
    }

    private void SnapPlus1(){
        //Debug.Log("+1 plus");
        isPlace = false;
        RaycastHit hit;
        if( Physics.Raycast( gameObject.transform.position,-transform.up, out hit, 10f )){
            //if(hit.transform.tag !="Cell")  return;
            if(hit.transform.tag =="Cell" && Vector3.Distance(gameObject.transform.position,hit.transform.position)<1f){
                gameObject.transform.position = new Vector3(hit.transform.position.x,0.3f,hit.transform.position.z);
                isPlace = true;
                //Invoke("StartShooting",0.1f);
                InviBullet.isStart = true;
            }
        }
        //Debug.Log("true or false:" +isPlace);
        if(isPlace == false){
            gameObject.transform.position = oldPosition;
            InviBullet.isStart = true;
        }
    }

    private void SnapPlus2(){
        //Debug.Log("+2 plus");
        isPlace = false;
        RaycastHit hit;
        Vector3 pivotPoint = new Vector3(gameObject.transform.position.x - gameObject.transform.localScale.x/4,gameObject.transform.position.y,gameObject.transform.position.z);
        if( Physics.Raycast( pivotPoint,-transform.up, out hit, 10f )){
            bool isError = false;
            if(hit.transform.tag !="Cell")  {isError = true;}
            if(hit.transform.position.x > 2.24f && hit.transform.position.z < 2.26f)    {isError = true;}
            RaycastHit checkOverlap1;
            RaycastHit checkOverlap2;
            Physics.Raycast( new Vector3(gameObject.transform.position.x + 0.3f,gameObject.transform.position.y,gameObject.transform.position.z),-transform.up, out checkOverlap1, 10f );
            Physics.Raycast( new Vector3(gameObject.transform.position.x - 0.3f,gameObject.transform.position.y,gameObject.transform.position.z),-transform.up, out checkOverlap2, 10f );  
            if(checkOverlap1.transform.tag == "BuffCell" || checkOverlap2.transform.tag == "BuffCell" ) {isError = true;}
            if(Vector3.Distance(pivotPoint,hit.transform.position)<1f && !isError){
                gameObject.transform.position = new Vector3(hit.transform.position.x + gameObject.transform.localScale.x/4,0.3f,hit.transform.position.z);
                isPlace = true;
                //Invoke("StartShooting",0.1f);
                InviBullet.isStart = true;
            }
        }
        if(isPlace == false){
            gameObject.transform.position = oldPosition;
            InviBullet.isStart = true;
        }
    }

    private void SnapMulti2(){
        //Debug.Log("x2 multi");
        isPlace = false;
        RaycastHit hit;
        Vector3 pivotPoint = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z - gameObject.transform.localScale.z/4);
        if( Physics.Raycast( pivotPoint,-transform.up, out hit, 10f )){
            bool isError = false;
            if(hit.transform.tag !="Cell")  {isError = true;}
            if(hit.transform.position.z > 1.54f && hit.transform.position.z < 1.56f )  {isError = true;}
            RaycastHit checkOverlap1;
            RaycastHit checkOverlap2;
            Physics.Raycast( new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z+0.3f),-transform.up, out checkOverlap1, 10f );
            Physics.Raycast( new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z-0.3f),-transform.up, out checkOverlap2, 10f );  
            if(checkOverlap1.transform.tag == "BuffCell" || checkOverlap2.transform.tag == "BuffCell" ) {isError = true;}
            if(Vector3.Distance(gameObject.transform.position,hit.transform.position)<1f && !isError){
                gameObject.transform.position = new Vector3(hit.transform.position.x,0.3f,hit.transform.position.z+ gameObject.transform.localScale.z/4);
                isPlace = true;
                //Invoke("StartShooting",0.1f);
                InviBullet.isStart = true;
            }
        }
        if(isPlace == false){
            gameObject.transform.position = oldPosition;
            InviBullet.isStart = true;
        }
    }

    private void SnapMulti3(){
        //Debug.Log("x3 multi");
        isPlace = false;
        RaycastHit hit;
        if( Physics.Raycast( gameObject.transform.position,-transform.up, out hit, 10f )){
            bool isError = false;
            if(hit.transform.tag !="Cell")  {isError = true;}
            if(hit.transform.position.z > 1.54f && hit.transform.position.z < 1.56f )  {isError = true;}
            if(hit.transform.position.z > -2.06f && hit.transform.position.z < -2.04f)  {isError = true;}
            RaycastHit checkOverlap1;
            RaycastHit checkOverlap2;
            Physics.Raycast( new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z+0.6f),-transform.up, out checkOverlap1, 10f );
            Physics.Raycast( new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z-0.6f),-transform.up, out checkOverlap2, 10f );  
            if(checkOverlap1.transform.tag == "BuffCell" || checkOverlap2.transform.tag == "BuffCell" ) {isError = true;}
            if(Vector3.Distance(gameObject.transform.position,hit.transform.position)<1f && !isError){
                gameObject.transform.position = new Vector3(hit.transform.position.x,0.3f,hit.transform.position.z);
                isPlace = true;
                //Invoke("StartShooting",0.1f);
                InviBullet.isStart = true;
            }
        }
        if(isPlace == false){
            gameObject.transform.position = oldPosition;
            InviBullet.isStart = true;
        }
    }

    private void CountPlus1(GameObject bullet){
        //Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
        bullet.GetComponent<InviBullet>().weight *= 2;
    }

    private void CountPlus2(GameObject bullet){
        //Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
        bullet.GetComponent<InviBullet>().weight *= 3;
    }

    private void CountMultiIn(GameObject bullet){
        //Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
        holdingBullet += bullet.GetComponent<InviBullet>().weight;
    }

    //private void CountMulti3In(GameObject bullet){
        //Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
    //     bullet.GetComponent<InviBullet>().weight *= 2;
    // }

    private void CountMultiOut(GameObject bullet){
        //Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
        bullet.GetComponent<InviBullet>().weight = holdingBullet;
        //holdingBullet = 0;
        bulletLeft++;
        if(this.type == 3){
            if(bulletLeft == 2){
                holdingBullet = 0;
                bulletLeft = 0;
            }
        }else if(this.type == 4){
            if(bulletLeft == 3){
                holdingBullet = 0;
                bulletLeft = 0;
            }
        }
        //Debug.Log(bullet.name + "   :   " + holdingBullet);
    }

    private void StartShooting(){
        InviBullet.isStart = true;
    }
    // private void CountMulti3Out(GameObject bullet){
    //     Debug.Log("Weight:"+bullet.GetComponent<InviBullet>().weight);
    //     bullet.GetComponent<InviBullet>().weight *= 2;
    // }
}
