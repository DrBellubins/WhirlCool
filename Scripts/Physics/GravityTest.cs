using Godot;
using System;

public partial class GravityTest : RigidBody3D
{
	[Export] public Path3D Track;

	private Vector3 gravityDir;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		var pathPos = Track.Curve.GetClosestPoint(Track.Transform.Basis * GlobalTransform.Origin);
		gravityDir = (pathPos - Position).Normalized();
		//gravityDir = Position.DirectionTo(ToGlobal(Track.Curve.GetClosestPoint(Track.ToLocal(GlobalPosition))));
        //GD.PushWarning($"dir: {gravityDir}");
    }

	public override void _PhysicsProcess(double delta)
	{
		ApplyForce(gravityDir);

		if (Input.IsActionJustPressed("interact"))
		{
            var randVec = new Vector3((float)Random.Shared.NextDouble(),
				(float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble());

            ApplyImpulse(randVec);
        }
        
        //LinearVelocity = gravityDir * 1f;
        //AddConstantCentralForce(gravityDir * 0.1f);
    }

	/*public override void _IntegrateForces(PhysicsDirectBodyState3D state)
	{
		state.AddConstantForce(gravityDir * 100f);
	}*/
}
