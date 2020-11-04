﻿using Player.Commands;
using UnityEngine;

namespace Player.States
{
    public class ClimbState : BaseState
    {
        public override void Start()
        {
            base.Start();
            
            Debug.Log("Climb State");
            
            Controller.Animator.SetInteger("AnimState", 0);
            
            Controller.Rigidbody.gravityScale = 0.0F;
        }

        public override void Update(Command cmd)
        {
            base.Update(cmd);

            Controller.UpdateTextureDirection();
            
            if (!Controller.CanClimb)
            {
                Controller.ChangeState(new IdleState());
            }

            Controller.MoveX(Input.GetAxisRaw("Horizontal"));
            Controller.MoveY(Input.GetAxisRaw("Vertical"));
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }   
}