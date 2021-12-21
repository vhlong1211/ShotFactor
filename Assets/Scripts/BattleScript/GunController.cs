using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    GameObject crosshair;
    GameObject canvas;
    GameObject player;
    GameObject viewCollider;
    RectTransform canvasRectTransform ;
    RectTransform crosshairRectTransform ;
    GameObject fpsCam;
    [SerializeField] private GameObject bulletPrefab;

    //for debug
    //GameObject dboj;

    int aimFilterLayer = 63;
    Vector3 hitPoint ;
    public float fireRate;
    float lastShotTime = 0f;

    //IEnumerator currentCoroutine;
    //bool notShootYet = false;

    // Start is called before the first frame update
    void Start()
    {   
        fpsCam = GameObject.Find("Main Camera");
        canvas = GameObject.Find("Canvas");
        crosshair = GameObject.Find("Crosshair");
        player = GameObject.Find("Player");
        viewCollider = GameObject.Find("ViewCollider");
        //dboj = GameObject.Find("dboj");
        canvasRectTransform = canvas.gameObject.GetComponent<RectTransform> ();
        crosshairRectTransform = crosshair.gameObject.GetComponent<RectTransform> ();
        fireRate = CountGrid.fireRate;
        //Debug.Log(viewCollider.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {   
        //Handle Gun rotation
        Vector3 aimDirection = CalculateShootDirection();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position,aimDirection,out hit,200f,aimFilterLayer)){
            //dboj.transform.position = hit.point;
            hitPoint=hit.point;
            gameObject.transform.LookAt(hit.point);
        }

        //Handle shooting

        // if(currentCoroutine == null){
        //     //Debug.Log("start cococo");
        //     currentCoroutine = ProcessShooting();
        //     StartCoroutine(currentCoroutine);
        // }
        // if(!VisionController.isShooting && !notShootYet){
        //     notShootYet = true;
        //     StartCoroutine(ProcessShooting());
        // }

        if(!VisionController.isShooting){
            lastShotTime = Time.time;
            return;
        }
        while(lastShotTime < Time.time){
            Shoot();
            lastShotTime += 1/fireRate;
        }
    }

    private void Shoot(){
        if(!VisionController.isShooting)    return;
        //Debug.Log("shoot at:"+hitPoint);
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = gameObject.transform.position + gameObject.transform.up*0.08f;
        Vector3 aimDirection = hitPoint - bullet.transform.position;
        bullet.GetComponent<Bullet>().Setup(aimDirection.normalized);
    }

    private Vector3 CalculateShootDirection(){
        Vector3 shootDirection = new Vector3(0,0,0);
        Vector3 cursorPosition;
        float xPercentage = crosshairRectTransform.anchoredPosition.x / canvasRectTransform.rect.width ;
        float yPercentage = crosshairRectTransform.anchoredPosition.y / canvasRectTransform.rect.height ;
        //bebug purpose,all under this is for debug
        //dboj.transform.position = new Vector3(-xPositionOnView,yPositionOnView,viewCollider.transform.position.z);
        Vector3 cursorDirection = -viewCollider.transform.right * viewCollider.transform.localScale.x * player.transform.localScale.x * xPercentage + viewCollider.transform.up *viewCollider.transform.localScale.y * player.transform.localScale.y * yPercentage;
        cursorPosition = viewCollider.transform.position + cursorDirection;
        shootDirection = cursorPosition - fpsCam.transform.position;
        return shootDirection;
    }

    // IEnumerator ProcessShooting(){
    //     //while(true){
    //         //Debug.Log("cororo");
    //         // if(!VisionController.isShooting)   yield return null;
    //         // Shoot();
    //         // yield return new WaitForSeconds((float)1/fireRate);
    //         //asdas

    //     //}   

    //     // yield return new WaitForSeconds((float)1/fireRate);
        
    //     // Shoot();
    //     // if(VisionController.isShooting){
    //     //     StartCoroutine(ProcessShooting());
    //     // }else{
    //     //     notShootYet = false;
    //     //     yield return null;
    //     // }
    // }

}
