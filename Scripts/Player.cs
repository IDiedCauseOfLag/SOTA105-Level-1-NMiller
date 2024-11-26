using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
	[Export] public float FireDelay;
	public List<AmmoType> SelectedAmmo = new List<AmmoType>();
	public float Speed = 200;
	public float GravityCF = 20;
	public float JumpImpulse = 300;
	public float PlayerMaxHorizontalSpeed = 400;
	public float PlayerMaxVerticalSpeed = 1000;
	public float AccelerationCF = .85f;
	public float ShootImpulseCF = 1000;
	private SceneTreeTimer FireDelayTimer;
	private int AmmoLoaded;
	
	public override void _Ready()
	{
		SelectedAmmo.Add(new AmmoTypeStandard());
		SelectedAmmo.Add(new AmmoTypeStandard());
		SelectedAmmo.Add(new AmmoTypeStandard());
		SelectedAmmo.Add(new AmmoTypeStandard());	
		SelectedAmmo.Add(new AmmoTypeStandard());
		SelectedAmmo.Add(new AmmoTypeSlug());
		GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").InstanceAmmoUI("res://Scenes/RoundUI.tscn", SelectedAmmo);
		AmmoLoaded = SelectedAmmo.Count;
	}
	public override void _Process(double delta)
	{
	}
    public override void _PhysicsProcess(double delta)
    {
		Velocity = GetMovementVector(CheckKeyPress());
		Velocity += UseGun(Util.BoolToInt(CanShoot())) * ShootImpulseCF;	
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
			SelectedAmmo[AmmoLoaded - 1].ShootAmmo(GlobalPosition, GlobalPosition.DirectionTo(GetGlobalMousePosition()), GetParent());//activates special effect of ammo - shoots projectiles
			AmmoLoaded -= AmmoUsed;//modifies ammo value
			GetNode<WeaponAnim>("Weapon").FireAnimation(true);//Runs firing animation
			GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").UseAmmo(-AmmoUsed);//updates UI
			FireDelayTimer = GetTree().CreateTimer(FireDelay);//starts firing delay timer;
			return GetImpulseDirection();
		}
		else{
			return Vector2.Zero;
		}
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
		if(GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation == "Death"){GetNode<Game>("../").ResetGame();}
	}
}
