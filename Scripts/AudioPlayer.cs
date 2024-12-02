using Godot;
using System;
using System.Collections.Generic;

public partial class AudioPlayer : AudioStreamPlayer2D
{
	[Export] Godot.Collections.Array<string> LoadSounds; 
	private List<AudioStream> AudioFiles = new List<AudioStream>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for(int i = 0; i < LoadSounds.Count; i++)
		{
			AudioFiles.Add(GD.Load<AudioStream>(LoadSounds[i]));
		}
		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void PlayAudio(int AudioIndex)
	{
		Stream = AudioFiles[AudioIndex];
		Play();
	}
	
}
