using Godot;
using System;

public partial class AmmoTypeStandard : AmmoType
{
	
	public AmmoTypeStandard() : base()//fires a spread of 5 shots
	{
		ProjectileDeviation = 10;
		ProjectileCount = 5;
		AmmoIndicatorSprite = GD.Load<Texture2D>("res://Sprites/AmmoTypeIndicators/Shell.png");
		AmmoName = "Buckshot";
	}
    public override OnFireData ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
    {
		for(int i = 0; i < ProjectileCount; i++)
		{
			SpawnProjectile(FiringOrigin, FiringDirection.Rotated(Mathf.DegToRad(ProjectileDeviation - i*(ProjectileDeviation*2/(ProjectileCount-1)))), ProjectileParent);
		}
		return new OnFireData(1000);
	}
}
