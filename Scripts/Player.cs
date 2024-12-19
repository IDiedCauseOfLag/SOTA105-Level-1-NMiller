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
	public float Speed = 140; //players speed
	public float GravityCF = 15; // strength of gravity
	private float VerticalIncreaseResistance = 2; //determines how much gravity effecs the player when moving up. also determines verticle speed
	public float JumpImpulse = 250; //jump strength
	public float PlayerMaxHorizontalSpeed = 300; //max speed of player achievable by self propulsion
	public float PlayerMaxVerticalSpeed = 700; //terminal velocity of player
	public float Acceleration = 40; 
	public float Deceleration = 20;
	private SceneTreeTimer FireDelayTimer; //reference for firing delay timer
	
	
	public override void _Ready()
	{
		GetNode<AmmoSelection>("../CanvasLayer/AmmoPicker").InstanceAmmoPicker(AmmoCount);
		//GetNode<AudioPlayer>("AudioPlayerMusic").PlayAudio(0); //plays music. comment out to disable
	}
	public void OnLevelStartInstanceUI(List<AmmoType> LoadedAmmo)
	{
		SelectedAmmo = LoadedAmmo;
		GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").InstanceAmmoUI("res://Scenes/UIScenes/RoundUI.tscn", SelectedAmmo);
		AmmoLoaded = SelectedAmmo.Count;
	}
	public override void _Process(double delta)
	{
		GetNode<Label>("/root/Game/CanvasLayer/Velocity").Text = "" + Velocity;
		if(IsOnFloor() != IsGrounded())
		{
			GD.Print("TimerTest");
		}
		GD.Print(GetNode<Timer>("BHopGracePeriod").TimeLeft);
		
	}
	public override void _PhysicsProcess(double delta)
	{
		Velocity = GetMovementVector(CheckKeyPress());
		Velocity += UseGun(Util.BoolToInt(CanShoot()));	
		MoveAndSlide();
	}
	public Vector2 CheckKeyPress()
	{
		return new Vector2(Input.GetAxis("p_left", "p_right"), GetJump());
	}
	private Vector2 GetMovementVector(Vector2 KeyInput)  
	{
		if(IsGrounded()){
			KeyInput.X = KeyInput.X * Speed;
		}else{
			KeyInput.X = Velocity.X + Util.ClampDelta(Velocity.X, KeyInput.X * Acceleration, PlayerMaxHorizontalSpeed, -PlayerMaxHorizontalSpeed) - Util.BoolToInt(KeyInput.X == 0) * Deceleration * Util.IntToPN((int)Velocity.X);
		}
		//KeyInput.X = Velocity.X * Util.BoolToInt(!IsOnFloor()) * Acceleration + KeyInput.X * Speed;
		KeyInput.Y = Mathf.Clamp(Velocity.Y * Util.BoolToInt(!Util.IntToBool((int)KeyInput.Y)) + KeyInput.Y * -JumpImpulse + GetGravity(), -PlayerMaxVerticalSpeed, PlayerMaxVerticalSpeed);
		if(KeyInput.Y < 0 && Input.IsActionJustReleased("p_jump")){KeyInput.Y /= 2;}
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
	private float GetJump()
	{
		return Util.BoolToInt(Input.IsActionJustPressed("p_jump") && CanJump());
	}
	private bool IsGrounded()
	{
		Timer GroundedTimer = GetNode<Timer>("BHopGracePeriod");
		if(!IsOnFloor()){
			GroundedTimer.Start(0);
		}
		return IsOnFloor() && GroundedTimer.TimeLeft == 0;
	}
	
	private float GetGravity()
	{
		return Util.BoolToInt(!IsOnFloor()) * GravityCF - (VerticalIncreaseResistance * Util.BoolToInt(Velocity.Y < 0));
	}

	private bool CanShoot()
	{
		return Input.IsActionJustPressed("p_shoot") && AmmoLoaded > 0 && (FireDelayTimer == null || FireDelayTimer.TimeLeft <= 0);
	}
	private bool CanJump()
	{
		return GetNode<Area2D>("Jump").HasOverlappingBodies();
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
