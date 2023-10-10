using Godot;
using System;

public partial class HoverCarSystem : RigidBody3D
{
	[Export] public RayCast3D[] HoverRays;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
}
