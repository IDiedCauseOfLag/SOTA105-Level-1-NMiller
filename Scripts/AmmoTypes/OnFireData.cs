using Godot;
using System;

public class OnFireData //class containing data on the shot fired
{
	public Vector2 RecoilImpulse; // strength of shots recoil
	public OnFireData(Vector2 Impulse)
	{
		RecoilImpulse = Impulse;
	}
}
