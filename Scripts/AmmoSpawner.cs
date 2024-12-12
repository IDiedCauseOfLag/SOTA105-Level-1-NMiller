using Godot;
using System;

public partial class AmmoSpawner : Node2D
{
	
	private PackedScene AmmoPickupSceneRef;
	public AmmoPickup MyPickup;
	public override void _Ready()
	{
		AmmoPickupSceneRef = GD.Load<PackedScene>("res://Scenes/AmmoPickup.tscn");
		CallDeferred("SpawnNewAmmoPickup");
	}
	private void SpawnNewAmmoPickup()
	{
		MyPickup = AmmoPickupSceneRef.Instantiate<AmmoPickup>();
		GetNode("/root/Game").AddChild(MyPickup);
		MyPickup.GlobalPosition = GlobalPosition;
		MyPickup.TreeExiting += OnAmmoDestroy;
	}
	public void OnAmmoDestroy()
	{
		GetNode<Timer>("Timer").Start(1);
	}
}
