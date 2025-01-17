﻿using Player.Commands;
using UnityEngine;

namespace Player.States
{
    public class ClimbState : BaseState
    {
        public override void Start()
        {
            base.Start();

            Controller.Animator.SetInteger("AnimState", 0);
            Controller.Animator.SetBool("Climbing", true);
            
            Controller.Rigidbody.gravityScale = 0.0F;
            
            Debug.Log("Climb state");
        }

        public override void Update(Command cmd)
        {
            base.Update(cmd);

            Controller.UpdateTextureDirection();

            if (cmd is SwitchWeaponCommand)
            {
                cmd.Execute(Controller);
            }
            
            if (!Controller.CanClimb)
            {
                Controller.ChangeState(new IdleState());
            }

            base.CheckAndHandleFallthrough();
            
            Controller.MoveX(Input.GetAxisRaw("Horizontal"));
            Controller.MoveY(Input.GetAxisRaw("Vertical"));
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }   
}