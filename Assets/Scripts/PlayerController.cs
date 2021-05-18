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
    private bool moveAble = true;

    public PlayerCameraScript PlayerCamera;

    public GameObject PlayerCameraObj;

    public Animator animator;


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
            PlayerCameraObj.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Checking if this is the LocalPlayer if so the player can move his character and do stuff
        if (this.isLocalPlayer)
        {
            PlayerMovement();
            PlayerCombat();
            PlayerCamera.CameraMovement();
            //Debug.Log(Rb.velocity.y);
        }
        else
        {
            return;
        }
    }

    void PlayerMovement()
    {
        if (moveAble == true)
        {
            //Input
            float x = Input.GetAxisRaw("Horizontal") * MoveSpeed;
            float z = Input.GetAxisRaw("Vertical") * MoveSpeed;
            //Movement
            Vector3 movePos = transform.right * x + transform.forward * z;
            Vector3 move = new Vector3(movePos.x, Rb.velocity.y, movePos.z);

            if (x != 0 || z != 0 && moveAble == true)
            {
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }

            //Debug.Log("float x" + x);
            //Debug.Log("float z" + z);

            Rb.velocity = move;

            //Grounding
            Grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.4f, layerMask);
            //Jumping
            if (Input.GetKeyDown(KeyCode.Space) && Grounded == true)
            {
                Rb.AddForce(new Vector3(transform.position.x, JumpForce, transform.position.z), ForceMode.Impulse);
                if (Rb.velocity.y >= JumpForce)
                {
                    Rb.velocity = new Vector3(transform.position.x, JumpForce, transform.position.z);
                }
            }
        }

    }

    IEnumerator AfterAttackAnimation(float time)
    {
        animator.SetBool("Attacking", true);
        moveAble = false;
        yield return new WaitForSeconds(time);

        animator.SetBool("Attacking", false);
        moveAble = true;
    }

    void PlayerCombat()
    {
        //Inputs
        //LeftClick
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("primaryAttack");
            StartCoroutine(AfterAttackAnimation(1.1f));
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
