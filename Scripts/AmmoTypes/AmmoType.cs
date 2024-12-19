using Godot;
using System;

public partial class AmmoType
{
	public string AmmoName;
	public Texture2D AmmoIndicatorSprite;
	internal float ProjectileDeviation = 0;
	internal PackedScene ProjectileRef;
	internal int ProjectileCount = 1;
	internal Vector2 RecoilImpulse = new Vector2(200, 600);
	internal float ProjectileSpeed = 500;
	public AmmoType()
	{
		ProjectileRef = GD.Load<PackedScene>("res://Scenes/Projectile.tscn");
	}
	public virtual OnFireData ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
	{
		GD.Print("you shot a blank - you probably forgot to write out the function for shooting");
		return new OnFireData(Vector2.One);
	}
	public void SpawnProjectile(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
	{
		Projectile MyProjectile = ProjectileRef.Instantiate<Projectile>();
		ProjectileParent.AddChild(MyProjectile);
		MyProjectile.GlobalPosition = FiringOrigin;
		MyProjectile.InstanceProjectile(FiringDirection, ProjectileSpeed);
	}
}
