using Godot;
using System;
using System.Collections.Generic;

public partial class AudioPlayer : AudioStreamPlayer2D //class containing multiple audio files to play
{
	[Export] Godot.Collections.Array<string> LoadSounds;
	[Export] public bool Looping;
	internal List<AudioStream> AudioFiles = new List<AudioStream>();
	public override void _Ready()
	{
		for(int i = 0; i < LoadSounds.Count; i++)
		{
			AudioFiles.Add(GD.Load<AudioStream>(LoadSounds[i]));
		}
	}
	public void PlayAudio(int AudioIndex)
	{
		Stream = AudioFiles[AudioIndex];
		Play();
	}
	public void Loop()
	{
		if(Looping){PlayAudio(0);}
	}
	
}
