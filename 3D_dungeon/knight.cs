using Godot;
using System;
using System.Diagnostics;

public partial class knight : CharacterBody3D
{
	[Export] private float aceleracao=5.0f, altura_pulo=10, gravidade=16f, jump_counter=0, coins=0;
	private Vector3 velocidade;

	
	public override void _PhysicsProcess(double delta)
	{
		
		velocidade = Velocity;
		// Abismo
		if(GlobalPosition.Y<-5){
			GlobalPosition = new Vector3(0,0,0);
			GD.Print("Você morreu.");
		}
		// Gravidade
		if (!IsOnFloor()) {
			velocidade.Y -= gravidade * (float)delta;
		}
		else{
			jump_counter = 0;
		}
		// Pulo
		if (Input.IsActionJustPressed("pulo") && (IsOnFloor() || jump_counter<1)) {
			velocidade.Y = altura_pulo;
			if(!IsOnFloor()){
				jump_counter++;
			}
		}
		// Movimento
		Vector2 controle_entrada = Input.GetVector("esquerda","direita","frente","tras");
		Vector3 direcao = (Transform.Basis * new Vector3(controle_entrada.X, 0, controle_entrada.Y)).Normalized();
		
		if(Input.IsActionPressed("rot_direita")){
			RotateY(-0.04f);
		}
		if(Input.IsActionPressed("rot_esquerda")){
			RotateY(0.04f);
		}
		if (direcao != Vector3.Zero) {
			velocidade.X = direcao.X * aceleracao;
			velocidade.Z = direcao.Z * aceleracao;
		}
		else {
			velocidade.X = 0;
			velocidade.Z = 0;
			//velocidade.X = Mathf.MoveToward(velocidade.X, 0, aceleracao);
			velocidade.Z = Mathf.MoveToward(velocidade.Z, 0, aceleracao);
		}

		Velocity = velocidade;
		MoveAndSlide();
	}

	private void _on_area_3d_3_body_entered_death(Node3D body)
	{
		if(body is knight){
			GlobalPosition = new Vector3(0,0,0);
			GD.Print("Você morreu.");
		}
	}
	
	public void add_coins(int n){
		coins += n;
		GetNode<Label>("%label_coins").Text = coins.ToString();
	}
}
