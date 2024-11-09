using Godot;
using System;

public partial class WeaponAnim : AnimatedSprite2D
{
	public override void _Process(double delta)
	{
		RotateTowardsMouse();
	}
	private void RotateTowardsMouse()//I should flip this between 90 & 270 degrees
	{
		LookAt(GetGlobalMousePosition());
	}
	public void FireAnimation(bool CanShoot)
	{
		if(CanShoot){Stop();Play("Fire");}
	}
	
}
