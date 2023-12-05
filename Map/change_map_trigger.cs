using Godot;
using System;

public partial class change_map_trigger : Area3D
{
	GameS MainS;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MainS = GetNode<GameS>("/root/GameS");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node3D body){
		if (body is knight){
			((knight)(body)).passTroughScene();
			MainS.scene_controler++;
			MainS.coin_controler = true;
			MainS.door_controler = false;
			GetTree().ChangeSceneToFile(MainS.current_scene[MainS.scene_controler]);
		}
	}
}
