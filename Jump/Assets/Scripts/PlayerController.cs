using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier;
    private bool isOnGround;
    private bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private InputAction jumpAction;
    [SerializeField] InputActionAsset inputActions;
    private int currentLifes;
    [SerializeField] int maxLifes;
    [SerializeField] HudManager hudManager;

    // Start is called before the first frame update
    void Awake()
    {
        jumpAction = inputActions.FindAction("Jump");
    }

    private void OnEnable()
    {
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        jumpAction = inputActions.FindAction("Jump");
        currentLifes = maxLifes;
        hudManager.updateLifes(currentLifes);
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.WasPressedThisFrame() && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true; 
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            /*Debug.Log("Game Over");
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            dirtParticle.Stop();
            explosionParticle.Play();*/
            currentLifes--;
            hudManager.updateLifes(currentLifes);
            if (currentLifes == 0)
            {
                processGameOver();
            }
        }     
    }

    private void processGameOver()
    {
        Debug.Log("Game Over");
        gameOver = true;
        playerAnim.SetInteger("DeathType_int", 1);
        playerAnim.SetBool("Death_b", true);
        dirtParticle.Stop();
        explosionParticle.Play();
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
