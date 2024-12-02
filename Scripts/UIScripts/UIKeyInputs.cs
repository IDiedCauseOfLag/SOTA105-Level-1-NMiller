using Godot;
using System;

public partial class UIKeyInputs : Node2D
{
	public override void _Process(double delta)
	{
		KeyInputs();
	}
	private void KeyInputs()
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
