using Godot;
using System;

public partial class Projectile : Area2D
{
	private float ProjectileSpeed;
	private Vector2 ProjectileDirection = Vector2.Zero;
	public void InstanceProjectile(Vector2 Direction, float Speed)
	{
		ProjectileDirection = Direction;
		ProjectileSpeed = Speed;
	}
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += ProjectileDirection * ProjectileSpeed * (float)delta;
    }
	public void OnCollideWithWall(Node2D body)
	{
		QueueFree();
	}
}
