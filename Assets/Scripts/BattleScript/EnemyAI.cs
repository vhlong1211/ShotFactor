using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{   
    //public CharacterController moveController;
    public GameObject player;
    [SerializeField] public GameObject self;
    public Rigidbody rbody;


    //For anim
    public Animator anim;
    public bool isHitAnim;
    public bool isRunAnim = false;
    public bool isDeathAnim = false;

    //Material
    [SerializeField] public Material x1Mat;
    [SerializeField] public Material x2Mat;
    [SerializeField] public Material x4Mat;
    [SerializeField] public Material x8Mat;
    [SerializeField] public Material x16Mat;
    [SerializeField] public Material x32Mat;
    [SerializeField] public Material x64Mat;
    public Material thisMaterial;
    [SerializeField] public Text txtHealth;

    Vector3 moveVector;
    bool isFalling = false;
    public int health;
    public int level;
    public int phase = -1;
    //GameObject dboj;
    public Action handleOutOfHP;

    // Start is called before the first frame update
    public void Start() {
        //dboj = GameObject.Find("dboj");
        player = GameObject.Find("Player");
        rbody = GetComponent<Rigidbody>();
        thisMaterial = GetComponentInChildren<Renderer>().material;
        handleOutOfHP = KageBunshinNoJutsu;
        GetLevelFromMaterial();
        SetupStatistic(this.level,this.phase);
        StageManager.Instance.enemyCountPerStage[this.phase]++;
        //Debug.Log("phase:"+this.phase);
    }

    // Update is called once per frame
    void Update()
    {   
        Move();
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag != "Bullet")   return;
        anim.SetBool("isHit",true);
        isHitAnim = true ;
        Invoke("SetBackRunAnim",1f);
        //Debug.Log("trigger");
        rbody.AddForce(-moveVector.normalized  * 100f);
        this.health--;
        txtHealth.text = "" + this.health;
        if(this.health == 0){
            handleOutOfHP();
        }
        if(this.health <= 0){
            txtHealth.text = "";
        }
    }

    private void SetBackRunAnim(){
        if(isHitAnim == true){
            anim.SetBool("isHit",false);
            isHitAnim = false;
        }     
    }

    public void Die(){
        //Debug.Log("Call die");
        StageManager.Instance.enemyCountPerStage[this.phase]--;
        //Destroy(gameObject);
        anim.SetBool("isDeath",true);
        StartCoroutine(ProcessDeath());
    }

    public void KageBunshinNoJutsu(){
        Debug.Log("Call bunshinnojutsu");
        StageManager.Instance.enemyCountPerStage[this.phase]--;
        // Destroy(gameObject);
        GameObject firstClone = Instantiate(self);
        firstClone.transform.position = new Vector3(firstClone.transform.position.x+firstClone.transform.right.x*(float)0.1,firstClone.transform.position.y,firstClone.transform.position.z+firstClone.transform.right.z*(float)0.1);
        GameObject secondClone = Instantiate(self);
        secondClone.transform.position = new Vector3(secondClone.transform.position.x-secondClone.transform.right.x*(float)0.1,secondClone.transform.position.y,secondClone.transform.position.z-secondClone.transform.right.z*(float)0.1);
        EnemyAI firstScript = firstClone.GetComponent<EnemyAI>();
        EnemyAI secondScript = secondClone.GetComponent<EnemyAI>();
        firstScript.SetupStatistic(this.level/2,this.phase);
        secondScript.SetupStatistic(this.level/2,this.phase);
        firstScript.isRunAnim = true;
        firstScript.anim.SetBool("isRun",true);
        secondScript.isRunAnim = true;
        secondScript.anim.SetBool("isRun",true);
        // firstScript.enabled = true;
        // secondScript.enabled = true;
        Destroy(gameObject);
    }

    public void SetupStatistic(int level,int phase){
        if(level == 0)  return;
        this.level = level;
        this.health = level;
        txtHealth.text = "" + this.health;
        if(this.level == 1){
            handleOutOfHP = Die;
        }
        SetScale();
        SetMaterial();
        DetectPhase(phase);
    }

    void Move(){
        //Debug.Log("Running");
        if(!Physics.Raycast(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+gameObject.transform.localScale.y/2,gameObject.transform.position.z),-transform.up,5f)&&!isFalling){
            isFalling = true;
            handleOutOfHP = Die;
            StartCoroutine(ProcessFalling());
        }
        if(this.phase != StageManager.Instance.currentPlayerPhase)  return;
        if(isDeathAnim)  return;
        if(isRunAnim == false){
            isRunAnim = true;
            anim.SetBool("isRun",true);
        }
        Vector3 currentHigher = new Vector3(gameObject.transform.position.x,player.transform.position.y,gameObject.transform.position.z);
        moveVector = player.transform.position - currentHigher;
        RaycastHit hit;
        Vector3 navigator = new Vector3(gameObject.transform.position.x+moveVector.normalized.x/5f*gameObject.transform.localScale.x,gameObject.transform.position.y+gameObject.transform.localScale.y,gameObject.transform.position.z+moveVector.normalized.z/5f*gameObject.transform.localScale.z);
        //dboj.transform.position = navigator;
        if(Physics.Raycast(navigator,-transform.up,out hit,gameObject.transform.localScale.y * 0.98f)){
            Debug.Log("hit sth?" + hit.transform.name);
            if(hit.transform.tag == "Terrain"){
                float height = hit.transform.position.y + hit.transform.localScale.y/2 ;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x , height , gameObject.transform.position.z);
            }
        }
        transform.position += moveVector.normalized * Time.deltaTime/2f ;
    }     

    public void SetScale(){
        gameObject.transform.localScale = new Vector3(1+(float)this.level/128 ,1+(float)this.level/128,1+(float)this.level/128);
        //Debug.Log("name:"+gameObject.name + "///"+ gameObject.transform.localScale.y);
    }

    public void SetMaterial(){
        switch(this.level){
            case 1:
            thisMaterial.color = x1Mat.color;
            break;
            case 2:
            thisMaterial.color = x2Mat.color;
            break;
            case 4:
            thisMaterial.color = x4Mat.color;
            break;
            case 8:
            thisMaterial.color = x8Mat.color;
            break;
            case 16:
            thisMaterial.color = x16Mat.color;
            break;
            case 32:
            thisMaterial.color = x32Mat.color;
            break;
            case 64:
            thisMaterial.color = x64Mat.color;
            break;
            default:
            thisMaterial.color = Color.white;
            break;
        }
    }

    public void GetLevelFromMaterial(){
        if(thisMaterial.color == x1Mat.color){
            this.level =1;
        }else if(thisMaterial.color == x2Mat.color){
            this.level =2;
        }else if(thisMaterial.color == x4Mat.color){
            this.level =4;
        }else if(thisMaterial.color == x8Mat.color){
            this.level =8;
        }else if(thisMaterial.color == x16Mat.color){
            this.level =16;
        }else if(thisMaterial.color == x32Mat.color){
            this.level =32;
        }else if(thisMaterial.color == x64Mat.color){
            this.level =64;
        }else{
            this.level =0;
        }
        //Debug.Log(this.level+"////"+thisMaterial.name);
    }

    public void DetectPhase(int phase){
        //if phase is pass from parent
        int currentPhase = 1;
        if(phase != -1){
            this.phase = phase;
            return;
        }
        //first time looking for themself
        Vector3 directToPlayerVector = player.transform.position - gameObject.transform.position;
        Vector3 direntPointVector;
        float oldDistance = 9999;
        for(int i=0;i<StageManager.Instance.currentLvStage.Length;i++){
            //Debug.Log("Loop count:"+ i);
            Vector3 currentPoint = StageManager.Instance.currentLvStage[i];
            direntPointVector = currentPoint - gameObject.transform.position;
            if(Vector3.Angle(directToPlayerVector,direntPointVector)>90){
                //Debug.Log("Angle: "+Vector3.Angle(directToPlayerVector,direntPointVector));
                break;
            }
            if(Vector3.Magnitude(direntPointVector) < oldDistance){
                oldDistance = Vector3.Magnitude(direntPointVector);
                currentPhase = i;
            }else{
                break;
            }
        }
        this.phase = currentPhase;
    }

    IEnumerator ProcessDeath(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator ProcessFalling(){
        yield return new WaitForSeconds(1);
        Die();
    }
}
    
