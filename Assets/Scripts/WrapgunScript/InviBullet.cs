using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InviBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float weight = 0;
    public Vector3 originalPos;
    public static bool isStart = false;
    public static int finishBulletCount = 0;
    void Start()
    {   
        originalPos = gameObject.transform.position;
        if(gameObject.transform.name == "Sphere"){
            weight = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart){
            StartJourney();
        }
    }

    public void StartJourney(){
        transform.position += Vector3.right * Time.deltaTime * 25f;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "CountGrid"){
            gameObject.transform.position = originalPos;
            finishBulletCount++;
            Invoke("ResetStatBullet",0.1f);
            if(finishBulletCount == 7){
                if(isStart){isStart=false;}
                finishBulletCount = 0;
            }
            
            
        }
    }
    public void ResetStatBullet(){
        // Debug.Log("All Finish");
        // if(isStart){isStart=false;}
        if(gameObject.transform.name == "Sphere"){
            //Debug.Log("Reset root");
            weight = 3;
        }else{
            //Debug.Log("Reset other  :" +gameObject.transform.name);
            weight = 0;
        }
        //finishBulletCount = 0;
    }
}
