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
	public void OnFrameCallAudio()
	{
		if(Frame == 5 && Animation == "Fire")
		{
			GetNode<AudioPlayer>("../AudioStreamPlayer2D").PlayAudio(0);
		}
		else if(Frame == 1 && Animation == "Fire")
		{
			GetNode<AudioPlayer>("../AudioStreamPlayer2D2").PlayAudio(1);
		}
	}	
}
