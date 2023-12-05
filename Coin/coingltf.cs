using Godot;
using System;

public partial class coingltf : Node3D
{
	private float frame_count = 0;
	private float first_x = 0;
	private float first_y = 0;
	private float first_z = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		first_x = GlobalPosition.X;
		first_y = GlobalPosition.Y;
		first_z = GlobalPosition.Z;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		frame_count++;
		RotateY(0.065f);
		GlobalPosition = new Vector3(first_x,first_y + (float)(Math.Sin(frame_count/25f)/2),first_z);
	}
}
