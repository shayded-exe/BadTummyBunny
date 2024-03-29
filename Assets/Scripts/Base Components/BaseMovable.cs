﻿using UnityEngine;

namespace PachowStudios.BadTummyBunny
{
  public abstract class BaseMovable : IMovable
  {
    public virtual Vector3 Velocity { get; protected set; }
    public virtual Vector3 LastGroundedPosition { get; protected set; }

    public virtual bool IsActivated { get; set; } = true;

    public virtual Vector2 MovementDirection => Velocity.normalized;
    public virtual bool IsFalling => Velocity.y < 0f;
    public virtual bool IsGrounded => CharacterController.IsGrounded;
    public virtual bool WasGrounded => CharacterController.WasGroundedLastFrame;
    public virtual LayerMask CollisionLayers => CharacterController.PlatformMask;

    public float? MoveSpeedOverride { get; set; }

    public float Gravity => Config.Gravity;
    public float MoveSpeed => MoveSpeedOverride ?? Config.MoveSpeed;

    protected CharacterController2D CharacterController => View.CharacterController;
    protected float MovementDamping => IsGrounded ? Config.GroundDamping : Config.AirDamping;

    private BaseMovableSettings Config { get; }
    private IView View { get; }

    protected BaseMovable(BaseMovableSettings config, IView view)
    {
      Config = config;
      View = view;
    }

    public virtual void Move(Vector3 moveVelocity)
      => Velocity = CharacterController.Move(moveVelocity * Time.deltaTime);

    public virtual bool Jump(float height)
    {
      if (height <= 0f || !IsGrounded)
        return false;

      Velocity = Velocity.Set(y: Mathf.Sqrt(2f * height * -Gravity));

      return true;
    }

    public virtual void ApplyKnockback(Vector2 knockback, Vector2 source)
    {
      if (knockback.IsZero())
        return;

      knockback.x += Mathf.Abs(knockback.x.Square() * -Gravity).SquareRoot();

      var newVelocity = Velocity;

      if (IsGrounded)
        newVelocity.y = Mathf.Abs(knockback.y * -Gravity).SquareRoot();

      // If the source is to the right of this, knockback to the left
      knockback.Scale(View.Position.RelationTo(source));

      if (!knockback.IsZero())
        Move(newVelocity + knockback.ToVector3());
    }

    public virtual void Disable() { }
  }
}