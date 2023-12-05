using Godot;
using System;

public partial class pause_menu : Control
{
	GameS MainS;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MainS = GetNode<GameS>("/root/GameS");
	}


	//Resume
	public void _on_start_pressed(){
		GetTree().ChangeSceneToFile(MainS.current_scene[MainS.scene_controler]);
	}
	//Main Menu
	public void _on_credits_pressed(){
		MainS.reset();
		GetTree().ChangeSceneToFile("main_menu.tscn");
	}
	public void _on_exit_pressed(){
		GetTree().Quit();
	}
}
