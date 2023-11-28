using Godot;
using System;

public partial class coin : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_body_entered(Node3D body){
		if(body is knight){
			GD.Print("Coin collected!");
			if(GlobalPosition.X == 0 && GlobalPosition.Y == -3.5f && GlobalPosition.Z == 5){
				GD.Print("O Easter Egg vale 10!");
				((knight)(body)).Morrer();
			}
			((knight)(body)).addCoins(1);
			QueueFree();
		}
	}
}
