using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{   
    public static VisionController Instance; 

    GameObject crosshair;
    GameObject canvas;
    GameObject player;
    RectTransform canvasRectTransform ;
    RectTransform crosshairRectTransform ;

    [SerializeField] float horizontalRotateRange = 30f;
    [SerializeField] float verticalRotateRange = 20f;
    [SerializeField] float rotationSpeed = 0.001f;

    //buffer mouse and cursor position
    Vector3 oldMousePos = new Vector3(0,0,0);
    float newXCursorPosition,newYCursorPosition;
    public static bool isShooting;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        canvas = GameObject.Find("Canvas");
        crosshair = GameObject.Find("Crosshair");
        player = GameObject.Find("Player");
        canvasRectTransform = canvas.gameObject.GetComponent<RectTransform> ();
        crosshairRectTransform = crosshair.gameObject.GetComponent<RectTransform> ();
    }

    private void Update() {
        RotationHandle();
    }

    private void OnMouseDrag() {
        //Check first click
        if(oldMousePos.magnitude == 0){
            oldMousePos = Input.mousePosition;
        }

        //Moving cursor
        float xPos=Input.mousePosition.x;
        float yPos=Input.mousePosition.y;
        

        Vector3 moveVector = new Vector3(xPos - oldMousePos.x , yPos - oldMousePos.y , 0);
        if(moveVector.magnitude > 0){
            //Debug.Log("Moving Cursor");
            Vector3 moveCursorVector = new Vector3(moveVector.x * canvasRectTransform.rect.width / Screen.width,
            moveVector.y * canvasRectTransform.rect.height / Screen.height,0);
            newXCursorPosition = Mathf.Abs(crosshairRectTransform.anchoredPosition.x + moveCursorVector.x) > canvasRectTransform.rect.width/2 ? Mathf.Sign(crosshairRectTransform.anchoredPosition.x + moveCursorVector.x)*canvasRectTransform.rect.width/2:(crosshairRectTransform.anchoredPosition.x + moveCursorVector.x);
            newYCursorPosition = Mathf.Abs(crosshairRectTransform.anchoredPosition.y + moveCursorVector.y) > canvasRectTransform.rect.height/2 ? Mathf.Sign(crosshairRectTransform.anchoredPosition.y + moveCursorVector.y)*canvasRectTransform.rect.height/2:(crosshairRectTransform.anchoredPosition.y + moveCursorVector.y);
            crosshairRectTransform.anchoredPosition = new Vector2(newXCursorPosition,newYCursorPosition);
        }
        oldMousePos = Input.mousePosition;
    }

    //Reset buffer mouse position
    private void OnMouseUp() {
        oldMousePos = new Vector3(0,0,0);
        isShooting = false;
    }

    private void OnMouseDown(){
        isShooting = true;
    }

    //Handle rotation
    private void RotationHandle(){
        float currentCamXRotation = player.transform.localEulerAngles.x > 180 ? player.transform.localEulerAngles.x -360f:player.transform.localEulerAngles.x ;
        float currentCamYRotation = player.transform.localEulerAngles.y > 180 ? player.transform.localEulerAngles.y -360f:player.transform.localEulerAngles.y ;
        float xRotation,yRotation;
        float signXCursorMove = crosshairRectTransform.anchoredPosition.y > 0 ? 1 : -1;
        float signYCursorMove = crosshairRectTransform.anchoredPosition.x > 0 ? 1 : -1;
        xRotation = currentCamXRotation + rotationSpeed * crosshairRectTransform.anchoredPosition.y * Mathf.Abs(currentCamXRotation - verticalRotateRange * signXCursorMove)/60f ;
        yRotation = currentCamYRotation + rotationSpeed * crosshairRectTransform.anchoredPosition.x * Mathf.Abs(currentCamYRotation - horizontalRotateRange * signYCursorMove)/60f ;
        xRotation = Mathf.Abs(xRotation) > 20f ? 20f * signXCursorMove : xRotation;
        yRotation = Mathf.Abs(yRotation) > 30f ? 30f * signXCursorMove : yRotation;
        player.transform.localRotation = Quaternion.Euler(xRotation,yRotation,0);
    }
}
