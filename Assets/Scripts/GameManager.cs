using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    public int currentScene = 1;
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }

    }

    public void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void LoadBattleScene(){
        SceneManager.LoadScene(currentScene);    
    }
}
