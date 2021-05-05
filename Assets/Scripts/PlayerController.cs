using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public Rigidbody Rb;
    public LayerMask layerMask;

    public float MoveSpeed = 5;
    public bool Grounded = true;
    public float JumpForce = 5;

    public PlayerCameraScript PlayerCamera;

    public GameObject PlayerCameraObj;


    // Start is called before the first frame update
    void Start()
    {
        if (this.isLocalPlayer)
        {
            //LockCursos?
            //Activate Canvas/Camera
            PlayerCameraObj.SetActive(true);
        }
        else
        {
            //Disable Canvas/Camera so we dont have more than one Canvas/Camera in the scene
            PlayerCameraObj.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Checking if this is the LocalPlayer if so the player can move his character and do stuff
        if (this.isLocalPlayer)
        {
            Debug.Log("is lcoalPlayer");
            PlayerMovement();
            PlayerCamera.CameraMovement();
        }
        else
        {
            return;
        }
    }

    void PlayerMovement()
    {
        //Input
        float x = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        float z = Input.GetAxisRaw("Vertical") * MoveSpeed;

        //Movement
        Vector3 PlayerMovement = new Vector3(x, 0f, z) * MoveSpeed * Time.deltaTime;
        transform.Translate(PlayerMovement, Space.Self);

        //Grounding
        Grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), 0.4f, layerMask);
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && Grounded == false)
        {
            Rb.AddForce(new Vector3(transform.position.x, JumpForce, transform.position.z), ForceMode.Impulse);
        }
    }

}
