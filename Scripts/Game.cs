using Godot;
using System;

public partial class Game : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UIKeyInputs();
	}
	private void UIKeyInputs()
	{
		if(Input.IsActionJustPressed("ui_toggle_fullscreen")){ToggleFullscreen();}
		if(Input.IsActionJustPressed("ui_close_window")){GetTree().Quit();}
		if(Input.IsActionJustPressed("ui_reset")){ResetGame();}
	}
	public Error ResetGame()
	{
		return GetTree().ReloadCurrentScene();
	}
	
	public void ToggleFullscreen()
	{
		if(DisplayServer.WindowGetMode() != (Godot.DisplayServer.WindowMode)3){DisplayServer.WindowSetMode((Godot.DisplayServer.WindowMode)3);}
		else{DisplayServer.WindowSetMode(0);}
	}
}
