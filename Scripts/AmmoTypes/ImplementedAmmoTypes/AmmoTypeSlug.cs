using Godot;
using System;

public partial class AmmoTypeSlug : AmmoType
{
	public AmmoTypeSlug() : base() //single shot with slightly higher than base recoil and higher velocity
	{
		ProjectileDeviation = 0;
		ProjectileCount = 1;
		AmmoIndicatorSprite = GD.Load<Texture2D>("res://Sprites/AmmoTypeIndicators/Slug.png");
		AmmoName = "Slugs";
		ProjectileSpeed = 1000;
	}
	public override OnFireData ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
	{
		SpawnProjectile(FiringOrigin, FiringDirection, ProjectileParent);
		return new OnFireData(1100);
	}
}
