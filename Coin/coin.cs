using Godot;
using System;

public partial class coin : Area3D
{

	public bool coin_status = true;
	GameS MainS;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MainS = GetNode<GameS>("/root/GameS");
		coin_status = MainS.coin_controler;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(!coin_status){
			QueueFree();
		}
	}

	private void _on_body_entered(Node3D body){
		if(body is knight){
			GD.Print("Coin collected!");
			if(GlobalPosition.X == 0 && GlobalPosition.Y == -3.5f && GlobalPosition.Z == 5){
				GD.Print("O Easter Egg vale 10!");
				((knight)(body)).Morrer();
			}
			((knight)(body)).addCoins(1);
			coin_status = false;
			MainS.coin_controler = coin_status;
			QueueFree();
		}
	}
	
	public void change_position(){
		GlobalPosition = new Vector3(-3,1,-4);
	}
}
