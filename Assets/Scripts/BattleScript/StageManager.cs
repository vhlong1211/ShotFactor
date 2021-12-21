using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{   
    public static StageManager Instance;
    
    public Vector3[] currentLvStage;
    public int[] enemyCountPerStage;
    public int currentPlayerPhase = 0;
    // Start is called before the first frame update
    void Awake()
    {   
        if(Instance == null){
            Instance = this;
        }
        int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        currentLvStage = DataConfig.allLevel[sceneIndex];
        enemyCountPerStage = new int[currentLvStage.Length];
        for(int j=0;j<enemyCountPerStage.Length;j++){
            enemyCountPerStage[j] = 0;
        }
        // for(int j=0;j<currentStage.Length;j++){
        //     Debug.Log(currentStage[j]);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if(enemyCountPerStage[currentPlayerPhase] == 0){
        //     currentPlayerPhase ++;
        // }
    }
}
