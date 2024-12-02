using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export] public float ProjectileDelay = 2;
	[Export] public bool DropsAmmo = true;
	[Export] public float EngagementDistance;
	private PackedScene ProjectileRef;
	private Player PlayerRef;

	private float ProjectileRemainingDelay = 0;
		
	public override void _Ready()
	{
		ProjectileRef = GD.Load<PackedScene>("res://Scenes/Projectile.tscn");
		PlayerRef = GetNode<Player>("../Player");
	}

	
	public override void _Process(double delta)
	{
		ShootProjectile((float)delta);
	}
	private void ShootProjectile(float delta)
	{
		ProjectileRemainingDelay -= delta;
		if(ProjectileRemainingDelay <= 0 && GlobalPosition.DistanceTo(PlayerRef.GlobalPosition) <= EngagementDistance)
		{
			ProjectileRemainingDelay = ProjectileDelay;
			Projectile NewProjectile = ProjectileRef.Instantiate<Projectile>();
			GetParent().AddChild(NewProjectile);
			NewProjectile.InstanceProjectile(GlobalPosition.DirectionTo(PlayerRef.GlobalPosition), 50, true);	
			NewProjectile.GlobalPosition = GlobalPosition;
		}
	}
	public void Kill()
	{
		CallDeferred("DropAmmo");
		QueueFree();
	}
	private void DropAmmo()
	{
		if(DropsAmmo)
		{
			AmmoPickup NewPickup = GD.Load<PackedScene>("res://Scenes/AmmoPickup.tscn").Instantiate<AmmoPickup>();
			PlayerRef.GetParent().AddChild(NewPickup);
			NewPickup.GlobalPosition = GlobalPosition;
			NewPickup.InstanceAmmo(100, 100);
			DropsAmmo = false;
		}
	}
}
