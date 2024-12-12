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
			AmmoSelectors[i].InstanceAmmoSelector();
		}
		RetrieveLastAmmoSelection(Global.AmmoTypeIndexes);
	}
	private void RetrieveLastAmmoSelection(List<int> Indices)
	{
		if(Indices == null)
		{		
			Indices = new List<int>(){1,1,1,1,1,1}; //so wow
		}
		for(int i = 0; i < AmmoSelectors.Count; i++)
		{
			AmmoSelectors[i].GetNode<OptionButton>("OptionButton").Selected = Indices[i];
			AmmoSelectors[i].OnAmmoSelect(Indices[i]);
		}
	}
	public void OnStartLevelDown()
	{
		GetTree().Paused = false;
		Visible = false;
		List<AmmoType> LoadedAmmo = new List<AmmoType>();
		Global.AmmoTypeIndexes = new List<int>();
		foreach(AmmoSelectionSingle AmmoSelector in AmmoSelectors)
		{
			LoadedAmmo.Add(AmmoSelector.ReturnSelectedAmmo());
			Global.AmmoTypeIndexes.Add(AmmoSelector.GetNode<OptionButton>("OptionButton").Selected);
		}
		GetNode<Player>("../../Player").OnLevelStartInstanceUI(LoadedAmmo);
		

	}
}
