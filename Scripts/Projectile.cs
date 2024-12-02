using Godot;
using System;
using System.Collections;

public partial class Projectile : Area2D
{
	bool EnemyProjectile = false;
	private float ProjectileSpeed;
	private Vector2 ProjectileDirection = Vector2.Zero;
	public void InstanceProjectile(Vector2 Direction, float Speed, bool ProjectileSource = false)
	{
		ProjectileDirection = Direction;
		ProjectileSpeed = Speed;
		EnemyProjectile = ProjectileSource;
	}
    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += ProjectileDirection * ProjectileSpeed * (float)delta;
    }
	public void OnCollideWithWall(Node2D body)
	{
		if(body.GetType().IsSubclassOf(typeof(CollisionObject2D)))
		{
			if(((CollisionObject2D)body).GetCollisionLayerValue(5) && !EnemyProjectile)
			{
				((Enemy)body).Kill();
				QueueFree();	
			}
			else if(((CollisionObject2D)body).GetCollisionLayerValue(3) && EnemyProjectile)
			{
				((Player)body).OnHazardEntered(this);
				QueueFree();
			}
			
		}
		else
		{
			QueueFree();
		}
		
	}
}
