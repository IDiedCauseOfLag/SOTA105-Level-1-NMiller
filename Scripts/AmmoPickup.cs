using Godot;
using System;

public partial class AmmoPickup : Area2D
{
	private float SuckDistance = 100;
	private float SuckSpeed = 100;	
	private Player PlayerRef;
	public void InstanceAmmo(float SckDis, float SckSpd)
	{
		SuckDistance = SckDis;
		SuckSpeed = SckSpd;
	}
	public override void _Ready()
	{
		PlayerRef = GetTree().Root.GetNode<Player>("Game/Player");
	}
	public override void _Process(double delta)
	{
		PickupAmmo((float)delta);
	}
	private void PickupAmmo(float delta)
	{
		if(PlayerRef.GlobalPosition.DistanceTo(GlobalPosition) <= SuckDistance && PlayerRef.AmmoLoaded < PlayerRef.SelectedAmmo.Count)
		{
			GlobalPosition = GlobalPosition.MoveToward(PlayerRef.GlobalPosition, 1 + delta * SuckSpeed);
		}
	}
	public void OnPlayerEnter(Node2D body)
	{
		if(PlayerRef.AmmoLoaded < PlayerRef.SelectedAmmo.Count)
		{
			PlayerRef.UseAmmo(-1);
			QueueFree();
		}
	}
}
