using Godot;
using System;
using System.Reflection.Metadata;

public partial class Player : CharacterBody2D
{
	[Export] public int AmmoCount;
	[Export] public float FireDelay;
	private SceneTreeTimer FireDelayTimer;
	public float Speed = 200;
	public float GravityCF = 20;
	public float JumpImpulse = 300;
	public float PlayerMaxHorizontalSpeed = 400;
	public float PlayerMaxVerticalSpeed = 1000;
	public float AccelerationCF = .85f;
	public float ShootImpulseCF = 1000;
	public override void _Ready()
	{
		GetNode<AmmoUI>("../CanvasLayer/AmmoCountUI").InstanceAmmoUI("res://Scenes/RoundUI.tscn", AmmoCount);
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
			AmmoCount -= AmmoUsed;//modifies ammo value
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
		return Input.IsActionJustPressed("p_shoot") && AmmoCount > 0 && (FireDelayTimer == null || FireDelayTimer.TimeLeft <= 0);
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
