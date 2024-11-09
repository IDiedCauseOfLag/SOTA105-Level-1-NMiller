using Godot;
using System;

public partial class LevelEnd : Node2D
{
	[Export] string ToLevel;
	public void BodyEntered(Node2D body)
	{
		GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, ToLevel);
	}
}
