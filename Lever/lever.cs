using Godot;
using System;

public partial class lever : Area3D
{
	private bool inside = false;
	private bool pressed = false;
	private Node3D axe;
	private Area3D coinRef;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		axe = GetNode<Node3D>("axe");
		coinRef = GetNode<Area3D>("%Coin");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("interaction") && inside && !pressed){
			axe.RotateZ(60*(3.1415f/180));
			((coin)(coinRef)).change_position();
			pressed = true;
		}
	}
	
	private void _on_body_entered(Node3D body)
	{
		if(body is knight){
			inside = true;
		}
	}
	
	private void _on_body_exited(Node3D body)
	{
		if(body is knight){
			inside = false;
			if(pressed){
				axe.RotateZ(-60*(3.1415f/180));
			}
			pressed = false;
		}
	}
}
