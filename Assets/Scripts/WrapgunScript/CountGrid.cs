using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public float fireRateBuffer = 0;
    public static float fireRate = 3;
    public int bulletCame = 0;
    [SerializeField] Text txtFirerate;

    private void OnTriggerEnter(Collider other) {
        fireRateBuffer += other.gameObject.GetComponent<InviBullet>().weight;
        Debug.Log(other.gameObject.name+": "+ fireRateBuffer + "/thisWeight:"+other.gameObject.GetComponent<InviBullet>().weight);
        bulletCame++;
        if(bulletCame == 7){
            fireRate = fireRateBuffer;
            txtFirerate.text = fireRate +" / sec";
            fireRateBuffer = 0;
            bulletCame = 0;
        }
    }
}
