using Godot;
using System;

public partial class Game : UIKeyInputs
{
	[Export] public Vector2 CameraMax = new Vector2(576, -320);
	[Export] public Vector2 CameraMin = new Vector2(-576, 320);
	public override void _Ready()
	{
		Camera2D MainCamera = GetNode<Camera2D>("Player/Camera2D");
		MainCamera.LimitBottom = (int)CameraMin.Y;
		MainCamera.LimitLeft = (int)CameraMin.X;
		MainCamera.LimitRight = (int)CameraMax.X;
		MainCamera.LimitTop = (int)CameraMax.Y;
	}
}
