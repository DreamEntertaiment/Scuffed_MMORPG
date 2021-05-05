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
            PlayerCombat();
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
        Vector3 movePos = transform.right * x + transform.forward * z;
        Vector3 move = new Vector3(movePos.x, Rb.velocity.y, movePos.z);

        Rb.velocity = move;

        //Grounding
        Grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.4f, layerMask);
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && Grounded == true)
        {
            Rb.AddForce(new Vector3(transform.position.x, JumpForce, transform.position.z), ForceMode.Impulse);
        }
    }

    void PlayerCombat()
    {
        //Inputs
        //LeftClick
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("primaryAttack");
        }
        //RightClick
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("SecondaryAttack");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Spell1");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Spell2");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Spell3");
        }
    }

}
