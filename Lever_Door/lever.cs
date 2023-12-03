using Godot;
using System;

public partial class lever : Area3D
{
	private bool inside = false;
	private bool pressed = false;
	private Node3D axe;
	private Node3D door;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		axe = GetNode<Node3D>("axe");
		door = GetNode<Node3D>("%wall_doorway/wall_doorway2/wall_doorway_door");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("interaction") && inside && !pressed){
			axe.RotateZ(60*(3.1415f/180));
			door.RotateY(90*(3.1415f/180));
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
		}
	}
}
