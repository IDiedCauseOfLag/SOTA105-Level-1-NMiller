using Godot;
using System;

public class OnFireData //class containing data on the shot fired
{
	public float RecoilImpulse; // strength of shots recoil
	public OnFireData(float Impulse = 1000)
	{
		RecoilImpulse = Impulse;
	}
}
