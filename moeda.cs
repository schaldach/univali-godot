using Godot;
using System;

public partial class moeda : Area3D {
	public void _on_body_entered (Node3D body) {
		if (body is knight) {
			((knight)(body)).PegouMoeda(1);
			QueueFree();
		}
	}
}
