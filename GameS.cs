using Godot;
using System;

public partial class GameS : Node
{	
	public int player_lifes = 3, player_coins = 0, scene_controler, jump_controler = 0;
	public float player_x, player_y, player_z;
	public bool game_pause = false, coin_controler = true, door_controler = false;
	public string[] current_scene = {"world1.tscn", "world2.tscn"};

	public void reset(){
		player_lifes = 3;
		player_coins = 0;
		jump_controler = 0;
		game_pause = false;
		scene_controler = 0;
		coin_controler = true;
		door_controler = false;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
