using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController player;
    GameObject Model;
    CameraMovement MovementPossible;
    void Start()
    {
        player = GetComponent<CharacterController>();
        Model = transform.GetChild(0).gameObject;
        MovementPossible = Camera.main.gameObject.GetComponent<CameraMovement>();
       
    }



    public float CharacterSpeed,Gravity,RotateSpeed;
    Vector3 TravelDirection;
    void PlayerDirection()
    {
        if(MovementPossible.EditMode==false)
        {
            TravelDirection = new Vector3((Input.GetAxis("Horizontal")), 0, (Input.GetAxis("Vertical")));
            TravelDirection = transform.TransformDirection(TravelDirection);
            TravelDirection *= CharacterSpeed;
            if (Input.GetKey(KeyCode.LeftShift)) { TravelDirection *= 2; }
            TravelDirection = PlayerJump(TravelDirection);
            if (TravelDirection != Vector3.zero)
            {
                Model.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(TravelDirection), (float)RotateSpeed);
            }
            TravelDirection.y -= Gravity * Time.deltaTime;
            player.Move(TravelDirection * Time.deltaTime);
        }
 
    }


    public bool OnGround;
    public float JumpSpeed;
   Vector3 PlayerJump(Vector3 pos)
    {
        if (OnGround == true && Input.GetButtonDown("Jump"))
        {
            OnGround = false;
            pos.y = JumpSpeed;
        }
        return pos;
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.name == "Ground")
        {
            OnGround = true;
        }
    }








    // Update is called once per frame
    void Update () {
       
        PlayerDirection();

    }







}
