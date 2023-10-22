using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 Position; //the position of the camera
    [Header("Settings")]
    public float Speed; //speed of camera
    public float xRotation = 0f; //x axis rotation value
    public float yRotation = 0f; //y axis rotation value
    public float sensitivity; //sensitivity of turning camera
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //locks camera within game view
        Cursor.visible = false;
        Position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        yRotation += Input.GetAxis("Mouse X") * sensitivity; //controls y axis of camera used by mouse
        xRotation += Input.GetAxis("Mouse Y") * -1 * sensitivity; //controls x axis of camera used by mouse
        transform.localEulerAngles = new Vector3(xRotation,yRotation,0); //turns mouse movement into rotation

        if(Input.GetKey(KeyCode.W)) //gets w input
        {
            Position.y += Speed/10; //moves camera upwards
        }
        if(Input.GetKey(KeyCode.S)) //gets s input
        {
            Position.y -= Speed/10; //moves camera downwards
        }
        if(Input.GetKey(KeyCode.A)) //gets a input
        {
            Position.x -= Speed/10; //moves camera to the left
        }
        if(Input.GetKey(KeyCode.D)) //gets d input
        {
            Position.x += Speed/10; //moves camera to the right
        }
        
        this.transform.position = Position;
    }
}
