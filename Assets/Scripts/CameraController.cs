using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 50f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;
    

    void Update()
    {
        if (GameManager.GameIsOver){
            this.enabled = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            doMovement = !doMovement;
        }

        if (!doMovement){
            return;
        }

        var direction = Input.inputString;

        switch (direction){
            
            case "w": 
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
                break;
            case "s":
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
                break;
            case "d":
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
                break;
            case "a":
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
                break;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        Vector3 pos = transform.position;
        pos.y -= scroll * 100 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
