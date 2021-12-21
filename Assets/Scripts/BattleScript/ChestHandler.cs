using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHandler : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private GameObject gameOverMenu;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Bullet")   return;
        Debug.Log(StageManager.Instance.currentPlayerPhase+"//"+StageManager.Instance.currentLvStage.Length);
        if(StageManager.Instance.currentPlayerPhase !=StageManager.Instance.currentLvStage.Length-1)    return;
        health--;
        if(health == 0){
            gameOverMenu.SetActive(true);
        }
    }
}
