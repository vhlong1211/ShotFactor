using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDirection;
    private float moveSpeed = 7f;

    //For debug

    // Update is called once per frame
    private void Start() {
        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        transform.position += moveSpeed * shootDirection * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }

    public void Setup(Vector3 shootDirection){
        this.shootDirection = shootDirection;
    }

    IEnumerator SelfDestruct()
 {
     yield return new WaitForSeconds(7f);
     Destroy(gameObject);
 }
}
