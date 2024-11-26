using Godot;
using System;

public partial class AmmoTypeSlug : AmmoType
{
	public AmmoTypeSlug() : base()
	{
		ProjectileDeviation = 0;
		ProjectileCount = 1;
		AmmoIndicatorSprite = GD.Load<Texture2D>("res://Sprites/AmmoTypeIndicators/Slug.png");
	}
    public override void ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
    {
		SpawnProjectile(FiringOrigin, FiringDirection, ProjectileParent);
	}
}
