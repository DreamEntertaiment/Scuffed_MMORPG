using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public float mouseSensetivity = 6f;

    public Transform Target, Player;

    float mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraMovement()
    {
        //Input
        //-----> CursorLock/Unlock with Escape

        mouseX += Input.GetAxis("Mouse X") * mouseSensetivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensetivity;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        Player.rotation = Quaternion.Euler(0, mouseX, 0);
        
    }

}
