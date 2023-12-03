using Godot;
using System;
using System.Diagnostics;

public partial class knight : CharacterBody3D
{
	[Export] private float aceleracao=5.0f, altura_pulo=4.5f, gravidade=5f;
	private float jump_counter=0, coins=0, lifes=3;
	private Vector3 velocidade;
	private bool last_floor = true;

	//Variáveis de animação e câmera
	[Export] private float sensibilidade = 0.5f;
	private SpringArm3D pau_selfie;
	private Node3D modelo;
	private AnimationTree arvore_animacao;
	private AnimationNodeStateMachinePlayback estado_animacao;

	public override void _Ready() {
		pau_selfie = GetNode<SpringArm3D>("SpringArm3D");
		modelo = GetNode<Node3D>("Rig");
		arvore_animacao = GetNode<AnimationTree>("AnimationTree");
		estado_animacao = (AnimationNodeStateMachinePlayback)arvore_animacao.Get("parameters/playback");
	}
	public void Morrer(){
		GlobalPosition = new Vector3(0,0,0);
		lifes = lifes-1;
		GetNode<Label>("%LabelHealth").Text = "Vidas " + lifes.ToString() + "/3";
		if(lifes == 0){
			GetTree().ChangeSceneToFile("main_menu.tscn");
		}
		GD.Print("Você morreu.");
	}
	public override void _PhysicsProcess(double delta)
	{
		velocidade = Velocity;
		
		// Abismo
		if(GlobalPosition.Y<-5){
			Morrer();
		}

		// Gravidade
		if (!IsOnFloor()) {
			velocidade.Y -= gravidade * (float)delta;
			estado_animacao.Travel("Jump_Idle");
		}
		else{
			if(last_floor == false){
				estado_animacao.Travel("Jump_Land");
			}
			jump_counter = 0;
		}
		last_floor = IsOnFloor();
		
		// Pulo
		if (Input.IsActionJustPressed("pulo") && (IsOnFloor() || jump_counter<1)) {
			velocidade.Y = altura_pulo;
			estado_animacao.Travel("Jump_Start");
			if(!IsOnFloor()){
				jump_counter++;
			}
		}

		Movimento(delta);
		Animacao();
		Velocity = velocidade;
		MoveAndSlide();

	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventMouseMotion) {
			InputEventMouseMotion motion = (InputEventMouseMotion) @event;
			RotationDegrees = new Vector3(Mathf.Clamp(RotationDegrees.X - motion.Relative.Y * sensibilidade, -1, .25f),
			RotationDegrees.Y - motion.Relative.X  * sensibilidade, 0);
		}	
	}
	public void Movimento (double delta) {
		Vector2 entrada = Input.GetVector("esquerda","direita","frente","tras");
		var dir = new Vector3(entrada.X, 0, entrada.Y).Rotated(Vector3.Up, Rotation.Y);
		velocidade = new Vector3 (Mathf.Lerp(velocidade.X, dir.X * aceleracao, aceleracao*(float)delta), velocidade.Y, Mathf.Lerp(velocidade.Z, dir.Z * aceleracao, aceleracao*(float)delta));
	}

	public void Animacao () {
		Vector3 vm; //velocidade do modelo
		vm = velocidade * Transform.Basis;
		arvore_animacao.Set("parameters/IWR/blend_position", new Vector2(vm.X, -vm.Z)/aceleracao);

	}
	public void addCoins(int m) {
		coins += m;
		GetNode<Label>("%LabelCoins").Text = "Moedas " + coins.ToString() + "/9";
	}

}
