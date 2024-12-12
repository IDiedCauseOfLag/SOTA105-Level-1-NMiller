using Godot;
using System;
using System.Collections.Generic;

public partial class AmmoUI : HBoxContainer
{
	private List<TextureRect> AmmoIndicators;
	private PackedScene IndicatorSceneRef;
	private int UIAmmoCount;
	// Called when the node enters the scene tree for the first time.

	public AmmoUI InstanceAmmoUI(string SceneRef, List<AmmoType> LoadedAmmo)
	{ 
		UIAmmoCount = LoadedAmmo.Count;
		IndicatorSceneRef = GD.Load<PackedScene>(SceneRef);
		AmmoIndicators = PopulateAmmoUI(LoadedAmmo.Count, LoadedAmmo);
		return this;
	}
	private List<TextureRect> PopulateAmmoUI(int AmmoCount, List<AmmoType> LoadedAmmoTypes)
	{
		if(AmmoCount == 0){return new List<TextureRect>();}
		else{
			List<TextureRect> Ammo = PopulateAmmoUI(AmmoCount - 1, LoadedAmmoTypes);
			Ammo.Add((TextureRect)IndicatorSceneRef.Instantiate());
			AddChild(Ammo[Ammo.Count - 1]);
			Ammo[Ammo.Count - 1].Texture = LoadedAmmoTypes[AmmoCount - 1].AmmoIndicatorSprite;
			return Ammo;
		}
	}
	public void UseAmmo(int AmmoUsed) //Updates Ammo display. fuck this goddamn function 
	{
		if(AmmoUsed < 0){
			for (int i = 0; i < Mathf.Abs(AmmoUsed) && i > -Mathf.Abs(AmmoUsed); i += Util.IntToPN(AmmoUsed)){
				AmmoIndicators[UIAmmoCount + i + Util.IntToPN(AmmoUsed)].Visible = !AmmoIndicators[UIAmmoCount + i + Util.IntToPN(AmmoUsed)].Visible;
			}
		}
		else if(AmmoUsed > 0){
			for (int i = 0; i < Mathf.Abs(AmmoUsed) && i > -Mathf.Abs(AmmoUsed); i += Util.IntToPN(AmmoUsed)){
				AmmoIndicators[UIAmmoCount - 1 + i + Util.IntToPN(AmmoUsed)].Visible = !AmmoIndicators[UIAmmoCount - 1 + i + Util.IntToPN(AmmoUsed)].Visible;
			}
		}
		UIAmmoCount += AmmoUsed;
	}//I want to cry
}
