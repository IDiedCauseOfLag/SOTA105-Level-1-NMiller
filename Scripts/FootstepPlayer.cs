using Godot;
using System;

public partial class FootstepPlayer : AudioPlayer
{
	private Player PRef;
	private RandomNumberGenerator Random;
    public override void _Ready()
    {
		PRef = GetNode<Player>("../");
		Random = new RandomNumberGenerator();
		Random.Randomize();
		base._Ready();
    }
    public override void _Process(double delta)
	{
		if(!Playing)
		{
			PlayFootstepAudio();
		}
	}
	public void PlayFootstepAudio()
	{
		if(PRef.CheckKeyPress().X != 0 && PRef.IsOnFloor()){PlayAudio(Random.RandiRange(0, AudioFiles.Count - 1));}
	}
}
