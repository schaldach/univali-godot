using Godot;
using System;
using System.Diagnostics;

public partial class knight : CharacterBody3D
{
	[Export] private float aceleracao=5.0f, altura_pulo=4.5f, gravidade=9.8f;
	private Vector3 velocidade;

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
	public override void _PhysicsProcess(double delta)
	{
		velocidade = Velocity;

		// Gravidade
		if (!IsOnFloor()) {
			velocidade.Y -= gravidade * (float)delta;
		}
		// Pulo
		if (Input.IsActionJustPressed("pulo") && IsOnFloor()) {
			velocidade.Y = altura_pulo;
		}

		// Pulo Duplo (espaço para implementar a lógica de pulo duplo)


		// Movimento
		Movimento(delta);

		//Animacao
		Animacao();

		Velocity = velocidade;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventMouseMotion) {
			InputEventMouseMotion motion = (InputEventMouseMotion) @event;
			//pau_selfie.Rotation = new Vector3(Mathf.Clamp(pau_selfie.Rotation.X - motion.Relative.Y / 1000 * sensibilidade, -1, .25f),
			//	pau_selfie.Rotation.Y - motion.Relative.X / 1000 * sensibilidade, 0);
			pau_selfie.RotationDegrees = new Vector3(Mathf.Clamp(pau_selfie.RotationDegrees.X - motion.Relative.Y * sensibilidade, -1, .25f),
			pau_selfie.RotationDegrees.Y - motion.Relative.X  * sensibilidade, 0);
		}	
	}
	public void Movimento (double delta) {
		Vector2 entrada = Input.GetVector("esquerda","direita","frente","tras");
		var dir = new Vector3(entrada.X, 0, entrada.Y).Rotated(Vector3.Up, pau_selfie.Rotation.Y);
		velocidade = new Vector3 (Mathf.Lerp(velocidade.X, dir.X * aceleracao, aceleracao*(float)delta), velocidade.Y, Mathf.Lerp(velocidade.Z, dir.Z * aceleracao, aceleracao*(float)delta));
	}

	public void Animacao () {
		Vector3 vm; //velocidade do modelo
		vm = velocidade * modelo.Transform.Basis;
		arvore_animacao.Set("parameters/IWR/blend_position", new Vector2(vm.X, vm.Y)/aceleracao);

	}
	public void PegouMoeda(int m) {
		GD.Print("Mais "+m+" moedas");
	}
	/*
	void Movimento(double delta) {
		
		Vector2 controle_entrada = Input.GetVector("esquerda","direita","frente","tras");
		Vector3 direcao = (Transform.Basis * new Vector3(controle_entrada.X, 0, controle_entrada.Y)).Normalized();
		
		if (direcao != Vector3.Zero) {
			velocidade.X = direcao.X * aceleracao;
			velocidade.Z = direcao.Z * aceleracao;
		}
		else {
			velocidade.X = 0;
			velocidade.Z = 0;
			//velocidade.X = Mathf.MoveToward(velocidade.X, 0, aceleracao);
			//velocidade.Z = Mathf.MoveToward(velocidade.Z, 0, aceleracao);
		}
	}
*/	
}
