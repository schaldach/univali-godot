using Godot;
using System;

public partial class credits : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	//Back
	public void _on_exit_pressed(){
		GetTree().ChangeSceneToFile("main_menu.tscn");
	}
}
