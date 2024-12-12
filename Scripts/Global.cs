using Godot;
using System;
using System.Collections.Generic;

public partial class Global : Node
{
	public static Global Instance;
	public static float SongTime = 0;
	public static List<int> AmmoTypeIndexes;
	private Node CurrentScene;
	public override void _Ready()
	{
		Instance = this;
		CurrentScene = GetTree().Root.GetChild(-1);

	}
	public void GoToScene(string FilePath)
	{
		CallDeferred(MethodName.DeferredGoToScene, FilePath); 	
	}
	private void DeferredGoToScene(string FilePath)
	{
		CurrentScene.Free();
		CurrentScene = GD.Load<PackedScene>(FilePath).Instantiate();
		GetTree().Root.AddChild(CurrentScene);
		GetTree().CurrentScene = CurrentScene;

	}
}
