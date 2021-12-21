using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataConfig : MonoBehaviour
{
    public static DataConfig Instance;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
    }

    public static Vector3[] level1Stage=new []{new Vector3(0,0.5f,8.5f),new Vector3(0,0.5f,-1),new Vector3(0,-2.5f,-25f)};

    public static Vector3[][] allLevel = new Vector3[][]{level1Stage};
}
