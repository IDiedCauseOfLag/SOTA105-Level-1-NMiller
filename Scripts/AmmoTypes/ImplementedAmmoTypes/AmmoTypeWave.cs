using Godot;
using System;

public partial class AmmoTypeWave : AmmoType
{
	public AmmoTypeWave() : base()//fires shots all around player character at lower recoil impulse
	{
		ProjectileDeviation = 180;
		ProjectileCount = 200;
		AmmoIndicatorSprite = GD.Load<Texture2D>("res://Sprites/AmmoTypeIndicators/Wave.png");
		AmmoName = "Waveshot";
	}
    public override OnFireData ShootAmmo(Vector2 FiringOrigin, Vector2 FiringDirection, Node ProjectileParent)
    {
		for(int i = 0; i < ProjectileCount; i++)
		{
			SpawnProjectile(FiringOrigin, FiringDirection.Rotated(Mathf.DegToRad(ProjectileDeviation - i*(ProjectileDeviation*2/(ProjectileCount-1)))), ProjectileParent);
		}
		return new OnFireData(700);
	}
}
