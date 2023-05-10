using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : GeneralAnimation
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        StatSetting(100, 10, 7, 10);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Stats.MoveSpeed, rb.velocity.y, 0);
        if(Input.GetAxis("Horizontal") != 0)
        {
            StateUpdate(CharacterStates.Run);
            ani.SetBool("Walk", true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateUpdate(CharacterStates.Jump);
            rb.velocity = Vector2.up * Stats.JumpForce;
        }
        if (!Input.anyKey&&rb.velocity == Vector2.zero)
        {
            StateUpdate(CharacterStates.Idle);
            ani.SetBool("Walk", false);
        }
        int key = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;

        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Ground"))
        {
            ani.SetBool("Jump", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            ani.SetBool("Jump", false);
        }
    }


}
