using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController3D : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] int speed=6;
    [SerializeField] float jumpforce = 10f;
    [SerializeField] float upwardGravityScale = 5f;
    [SerializeField] float downwardGravityScale = 6f;
    [SerializeField] float defaultGravityScale = 4.5f;
    float groundCheckRayLength = 1.25f;
    Vector3 input;

    bool grounded;

    GameControlls playerControlls;
    InputAction move;
    InputAction jump;

    void Awake()
    {
        playerControlls = new GameControlls();
    }

    private void OnEnable()
    {
        move = playerControlls.Player.Move;
        jump = playerControlls.Player.Jump;

        move.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(rb.velocity.y == 0)
        {
            rb.mass = defaultGravityScale;
        }else if(rb.velocity.y > 0.1f)
        {
            rb.mass = upwardGravityScale;
        }else if (rb.velocity.y < 0.1f)
        {
            rb.mass = downwardGravityScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();


        //Debug
        DebugCheckGround();
    }

    void Move()
    {

        input.x = move.ReadValue<Vector2>().x;
        input.z = move.ReadValue<Vector2>().y;
        input *= speed;

        input = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0)*input;

        input.y = rb.velocity.y;
        if(jump.triggered&&CheckGround())
        {
            input.y = 0;
            input.y = jumpforce;
        }

        rb.velocity = input;
    }

    bool CheckGround()
    {

        return Physics.Raycast(transform.position+Vector3.up, Vector3.down, groundCheckRayLength);
    }

    void DebugCheckGround()
    {
        Color debugColor = Color.red;
        if(CheckGround())
        {
            debugColor = Color.green;
        }else
        {
            debugColor = Color.red;
        }

        Debug.DrawRay(transform.position+Vector3.up, Vector3.down*groundCheckRayLength, debugColor);
    }


}
