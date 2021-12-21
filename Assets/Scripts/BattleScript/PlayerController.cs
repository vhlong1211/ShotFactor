using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] private GameObject gameOverMenu;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            gameOverMenu.SetActive(true);
        }
    }

    private void Update() {
        //Debug.Log(StageManager.Instance.enemyCountPerStage[StageManager.Instance.currentPlayerPhase]);
        //Debug.Log("curphase:"+StageManager.Instance.currentPlayerPhase);
        //Debug.Log("length:"+StageManager.Instance.enemyCountPerStage.Length);
        if(StageManager.Instance.enemyCountPerStage[StageManager.Instance.currentPlayerPhase] == 0 ){
            //Debug.Log("Start Moving");
            if(StageManager.Instance.currentPlayerPhase == StageManager.Instance.currentLvStage.Length-1)   return;
            Move(StageManager.Instance.currentLvStage[StageManager.Instance.currentPlayerPhase+1]);
        }
    }

    private void Move(Vector3 target){
        Vector3 aimToNextPoint = target-gameObject.transform.position;
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position,-transform.up,out hit,5f)){
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,hit.point.y + gameObject.transform.localScale.y/2,gameObject.transform.position.z);
        }
        transform.position = Vector3.MoveTowards(transform.position,target,5f*Time.deltaTime);
        if(Vector3.Distance(transform.position,target)<0.1f && StageManager.Instance.currentPlayerPhase < StageManager.Instance.currentLvStage.Length-1){
            //Debug.Log("Reach Target:");
            StageManager.Instance.currentPlayerPhase++;
        }
    }
}
