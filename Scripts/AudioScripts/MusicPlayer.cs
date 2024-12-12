using Godot;
using System;

public partial class MusicPlayer : AudioPlayer
{
	public override void _Ready()
	{
		base._Ready();
		//PlayAudio(0, Global.SongTime);
	
	}
	public void OnQueueFree()
	{
		Global.SongTime = GetPlaybackPosition();
	}

	
}
