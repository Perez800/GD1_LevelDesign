using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float gravity = -9.81f;

    [Header("Player Speed Settings")]
    public float speed = 2.0f;

    [Header("Player Jump Settings")]
    public float jump = 1.0f;

    [Header("Melee")]
    public int meleeDamage = 10;
    public GameObject attackHitBox;

    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        //Jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jump * -3.0f * gravity);
        }

        if (groundedPlayer == false)
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        
        controller.Move(playerVelocity * Time.deltaTime);

        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Attack());
        }

        
    }

    //Attack
    IEnumerator Attack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackHitBox.SetActive(false);
    }
}
