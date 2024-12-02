using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
	[Export] public int AmmoCount; //Determines max rounds in gun - used for instantiating ui
	[Export] public float FireDelay; //firerate cap - should operate independently from animation speed
	public List<AmmoType> SelectedAmmo = new List<AmmoType>(); //list of ammo types loaded into the gun. filled from menu
	public int AmmoLoaded;//the actual rounds remaining
	public float Speed = 200; //players speed
	public float GravityCF = 20; // strength of gravity
	public float JumpImpulse = 300; //jump strength
	public float PlayerMaxHorizontalSpeed = 400; //max speed of player
	public float PlayerMaxVerticalSpeed = 1000; // function the terminal velocity of player
	public float AccelerationCF = .85f; // determines the sensitivity of aerial control
	private SceneTreeTimer FireDelayTimer; //reference for firing delay timer
	
	
	public override void _Ready()
	{
		GetNode<AmmoSelection>("../CanvasLayer/AmmoPicker").InstanceAmmoPicker(AmmoCount);
	}
	public void OnLevelStartInstanceUI(List<AmmoType> LoadedAmmo)
	{
		SelectedAmmo = LoadedAmmo;
		GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").InstanceAmmoUI("res://Scenes/UIScenes/RoundUI.tscn", SelectedAmmo);
		AmmoLoaded = SelectedAmmo.Count;
	}
	public override void _Process(double delta)
	{
	}
    public override void _PhysicsProcess(double delta)
    {
		Velocity = GetMovementVector(CheckKeyPress());
		Velocity += UseGun(Util.BoolToInt(CanShoot()));	
		MoveAndSlide();
    }
	private Vector2 CheckKeyPress()
	{
		return new Vector2(Input.GetAxis("p_left", "p_right"), GetJump());
	}
	private float GetJump()
	{
		return Util.BoolToInt(Input.IsActionJustPressed("p_jump") && IsOnFloor());
	}
	private float GetGravity()
	{
		return Util.BoolToInt(!IsOnFloor()) * GravityCF;
	}
	private Vector2 GetMovementVector(Vector2 KeyInput)
	{
		KeyInput.X = Mathf.Clamp(Velocity.X * Util.BoolToInt(!IsOnFloor()) * AccelerationCF + KeyInput.X * Speed, -PlayerMaxHorizontalSpeed, PlayerMaxHorizontalSpeed);
		KeyInput.Y = Mathf.Clamp(Velocity.Y + KeyInput.Y * -JumpImpulse + GetGravity(), -PlayerMaxVerticalSpeed, PlayerMaxVerticalSpeed);
		if(KeyInput.Y < 0 && Input.IsActionJustReleased("p_jump")){KeyInput /= 2;}
		return KeyInput;
	}
	private Vector2 UseGun(int AmmoUsed)
	{
		if(CanShoot()){
			OnFireData ShotData = SelectedAmmo[AmmoLoaded - 1].ShootAmmo(GlobalPosition, GlobalPosition.DirectionTo(GetGlobalMousePosition()), GetParent());//activates special effect of ammo - shoots projectiles
			UseAmmo(AmmoUsed);//behavior for decreasing ammo count and updating ui
			GetNode<WeaponAnim>("Weapon").FireAnimation(true);//Runs firing animation
			FireDelayTimer = GetTree().CreateTimer(FireDelay);//starts firing delay timer;
			return GetImpulseDirection() * ShotData.RecoilImpulse;
		}
		else{
			return Vector2.Zero;
		}
	}
	public void UseAmmo(int AmmoUsed)
	{
		AmmoLoaded -= AmmoUsed;//modifies ammo value
		AmmoLoaded = Mathf.Clamp(AmmoLoaded, 0, SelectedAmmo.Count);//ensures that ammo is not above the max ammo or below the min ammo
		GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").UseAmmo(-AmmoUsed);//updates UI
	}
	private bool CanShoot()
	{
		return Input.IsActionJustPressed("p_shoot") && AmmoLoaded > 0 && (FireDelayTimer == null || FireDelayTimer.TimeLeft <= 0);
	}
	private Vector2 GetImpulseDirection()
	{
		return (GetGlobalMousePosition().DirectionTo(GlobalPosition) * Util.BoolToInt(Input.IsActionJustPressed("p_shoot"))).Normalized();
	}
	public void OnHazardEntered(Node2D Body)
	{
		Speed = 0;
		GravityCF = 0;
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Death");
	}
	public void DeathAnimFinish()
	{
		if(GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation == "Death"){GetNode<Game>("../").ResetGame();}//recieves signal from animation notifying when animation is finished
	}
}
