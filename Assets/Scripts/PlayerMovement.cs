using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
    {
        public CharacterController controller;
        public LayerMask layer;
        public float speed = 100f;
        public float gravity = -60f;
        public float jumpHeight = 5f;
        public float coolDown = 500f;
        public Transform groundCheck;
        public GameObject Player;

    public bool hasReachedEnding = false;
        
       
        public LayerMask groundMask;

        Vector3 velocity;
        bool isGrounded;

        void Update()
        {

        RaycastHit hit;
        float distance = 2f;
        Vector3 dir = new Vector3(0, -2);
        if (Physics.Raycast(transform.position, dir, out hit, distance , ~layer))
        {
            isGrounded = true;
        }
        else
        { 
            isGrounded = false;
        }

        if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (Player.transform.position.y < -500)
        {
            print(Player.transform.position);
            hasReachedEnding = true;
            Player.transform.position = new Vector3(0, 100, 0);
        }
        

    }

    IEnumerator Reload()
    {

        coolDown = 0f;
        Debug.Log("coolDown time baby");
        yield return new WaitForSeconds(5);
        coolDown = 10f;

    }
}