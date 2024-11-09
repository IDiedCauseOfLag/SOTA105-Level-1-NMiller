using Godot;
using System;
using System.Collections.Generic;

public partial class AmmoUI : HBoxContainer
{
	private List<TextureRect> AmmoIndicators;
	private PackedScene IndicatorSceneRef;
	private int UIAmmoCount;
	// Called when the node enters the scene tree for the first time.
	public AmmoUI InstanceAmmoUI(string SceneRef, int AmmoCount)
	{
		UIAmmoCount = AmmoCount;
		IndicatorSceneRef = GD.Load<PackedScene>(SceneRef);
		AmmoIndicators = PopulateAmmoUI(AmmoCount);
		return this;
	}
	private List<TextureRect> PopulateAmmoUI(int AmmoCount)
	{
		if(Util.IntToBool(AmmoCount)){return new List<TextureRect>();}
		else{
			List<TextureRect> Ammo = PopulateAmmoUI(AmmoCount - 1);
			Ammo.Add((TextureRect)IndicatorSceneRef.Instantiate());
			AddChild(Ammo[Ammo.Count - 1]);
			return Ammo;
		}
	}
	public bool UseAmmo(int AmmoUsed) //Updates Ammo display. returns true if successful. returns false if not enough ammo
	{
		if(AmmoUsed == 0){return true;}
		if(UIAmmoCount == 0){return false;}
		else{
			UIAmmoCount += Util.IntToPN(AmmoUsed);
			AmmoIndicators[UIAmmoCount].Visible = !AmmoIndicators[UIAmmoCount].Visible;
			return UseAmmo(AmmoUsed - Util.IntToPN(AmmoUsed));
		}	
	}
}
