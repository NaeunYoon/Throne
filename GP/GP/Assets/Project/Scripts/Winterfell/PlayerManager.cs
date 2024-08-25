using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerManager : SceneActivity_Winterfell
{
    public Animator anim = null;
    public float _speed = 5f;
    public Transform cam = null;
    public CinemachineVirtualCamera _playercam = null;
    public VirtualJoystick _joystick = null;
    public Transform camPos = null;
    
   public override void Create()
   {
        anim = this.GetComponent<Animator>();
       _speed = 5f;
       cam = FindObjectOfType<Camera>().transform;
       _joystick = App.inst.uiMgr.joystick;
       //_joystick.GetPlayer(this);
       
       _playercam.Follow = camPos;
       _playercam.LookAt = camPos;
       
   }
   
   public void Move(Vector2 inputDirection)
   {
       Vector2 moveInput = inputDirection;
       bool isMove = moveInput.magnitude != 0;
       anim.SetInteger("anim", 1);
       if (isMove)
       {
           Vector3 lookForward = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;
           Vector3 lookRight = new Vector3(cam.right.x, 0f, cam.right.z).normalized;
           Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
           this.transform.forward = moveDir;
           //Debug.Log(moveDir);
           this.transform.position += moveDir * Time.deltaTime * _speed;
       }
   }

   public void FollowingMaid()
   {
       
   }
   
   
   
}
