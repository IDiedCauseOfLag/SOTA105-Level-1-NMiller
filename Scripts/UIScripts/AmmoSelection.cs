using Godot;
using System;
using System.Collections.Generic;

public partial class AmmoSelection : Control
{
	private PackedScene IndividualAmmoPickerRef;
	private List<AmmoSelectionSingle> AmmoSelectors = new List<AmmoSelectionSingle>();
	public void InstanceAmmoPicker(int AmmoCount)
	{
		GetTree().Paused = true;
		IndividualAmmoPickerRef = GD.Load<PackedScene>("res://Scenes/UIScenes/IndividualAmmoPicker.tscn");
		Control Container = GetNode<Control>("ColorRect/HBoxContainer");
		for(int i = 0; i < AmmoCount; i++)
		{
			AmmoSelectors.Add(IndividualAmmoPickerRef.Instantiate<AmmoSelectionSingle>());
			Container.AddChild(AmmoSelectors[i]);
		}
	}
	public void OnStartLevelDown()
	{
		GetTree().Paused = false;
		Visible = false;
		List<AmmoType> LoadedAmmo = new List<AmmoType>();
		foreach(AmmoSelectionSingle AmmoSelector in AmmoSelectors)
		{
			LoadedAmmo.Add(AmmoSelector.ReturnSelectedAmmo());
		}
		GetNode<Player>("../../Player").OnLevelStartInstanceUI(LoadedAmmo);

	}
}
