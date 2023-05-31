using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    [SerializeField] private float speed = 7;
    [SerializeField] private float sprintSpeedMultiplier = 10f; // Additional speed when sprinting
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private Fish playerFish;
    [SerializeField] private Fish testFish;



    private bool isSprinting = false; // Flag to track if the player is sprinting

    // Start is called before the first frame update
    void Start()
    {
        this.playerFish.onLevelUp += GrowUp;  
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector += playerCamera.cameraDirection;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVector -= playerCamera.cameraDirection;
        }

        inputVector = inputVector.normalized;

        // Check if the Shift key is held down to sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            inputVector *= speed * sprintSpeedMultiplier;
        }
        else
        {
            isSprinting = false;
            inputVector *= speed;
        }
        // Test Attack and Eat func
        if(Input.GetKey(KeyCode.Z)){
            Debug.Log("attack");
            this.playerFish.Attack(this.testFish);
        }
        if(Input.GetKey(KeyCode.X)){
            this.playerFish.Eat(this.testFish);
        }
        transform.position += inputVector * Time.deltaTime;
        // Debug.Log(playerFish.GetSize());
        // Debug.Log(transform.localScale);
    }

    private void GrowUp(object sender, EventArgs e)
    {
        Debug.Log("Level Up Player");
        // transform.localScale = new Vector3(playerFish.GetSize()*2,playerFish.GetSize()*2,playerFish.GetSize()*2);
        transform.localScale = new Vector3(1,1,1);
    }
}
