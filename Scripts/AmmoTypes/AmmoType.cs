using Godot;
using System;

public partial class AmmoType
{
	public Texture2D AmmoIndicatorSprite;
	internal float ProjectileDeviation = 0;
	internal PackedScene ProjectileRef;
	internal int ProjectileCount = 1;
	public AmmoType()
	{
		ProjectileRef = GD.Load<PackedScene>("res://Scenes/Projectile.tscn");
	}
	public virtual void ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
	{
		GD.Print("you shot a blank - you probably forgot to write out the function for shooting");
	}
	public void SpawnProjectile(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
	{
		Projectile MyProjectile = ProjectileRef.Instantiate<Projectile>();
		ProjectileParent.AddChild(MyProjectile);
		MyProjectile.GlobalPosition = FiringOrigin;
		MyProjectile.InstanceProjectile(FiringDirection, 500);
	}
}
