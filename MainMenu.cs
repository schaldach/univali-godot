using Godot;
using System;

public partial class MainMenu : Control
{
	public void _on_start_pressed(){
		GetTree().ChangeSceneToFile("world1.tscn");
	}
	public void _on_credits_pressed(){
		GetTree().ChangeSceneToFile("credits.tscn");
	}
	public void _on_exit_pressed(){
		GetTree().Quit();
	}
}
