using Godot;
using System;
using System.Collections.Generic;
public partial class AmmoSelectionSingle : Control
{
	private List<AmmoType> AmmoTypesRef = new List<AmmoType>();
	private int SelectedIndex = 0;
	public override void _Ready()
	{
		string[] AmmoTypes = DirAccess.GetFilesAt("res://Scripts/AmmoTypes/ImplementedAmmoTypes/");
		foreach(String ClassName in AmmoTypes)
		{
			AmmoTypesRef.Add((AmmoType)Activator.CreateInstance(Type.GetType(ClassName.Substring(0, ClassName.IndexOf('.')))));
			GetNode<OptionButton>("OptionButton").AddIconItem(AmmoTypesRef[AmmoTypesRef.Count - 1].AmmoIndicatorSprite, AmmoTypesRef[AmmoTypesRef.Count - 1].AmmoName);
		}
		GetNode<TextureRect>("TextureRect").Texture = AmmoTypesRef[0].AmmoIndicatorSprite;
	}
	public void OnAmmoSelect(int Index)
	{
		GetNode<TextureRect>("TextureRect").Texture = AmmoTypesRef[Index].AmmoIndicatorSprite;
		SelectedIndex = Index;
	}
	public AmmoType ReturnSelectedAmmo()
	{
		return AmmoTypesRef[SelectedIndex];
	}
}
