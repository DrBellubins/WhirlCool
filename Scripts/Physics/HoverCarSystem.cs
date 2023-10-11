using Godot;
using System;

public partial class HoverCarSystem : RigidBody3D
{
	[Export] public Path3D Track;
	[Export] public RayCast3D HoverRay;

	private Vector3 pathPos;
    private Vector3 gravityDir;

    public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
        pathPos = Track.Curve.GetClosestPoint(Track.Transform.Basis * GlobalTransform.Origin);
        gravityDir = (pathPos - Position).Normalized();
    }

	public override void _PhysicsProcess(double delta)
	{
		var steerAxis = Input.GetAxis("steer_left", "steer_right");
		var throttleAxis = Input.GetAxis("throttle", "brake");

        ApplyTorque(new Vector3(0f, steerAxis * 10f, 0f));

        var target_vector = GlobalPosition.DirectionTo(pathPos);
        var target_basis = Basis.LookingAt(target_vector, Vector3.Up);
        Basis = Basis.Slerp(target_basis, 0.5f);

        if (HoverRay.IsColliding())
		{
			var hoverNormal = HoverRay.GetCollisionNormal();
            
            ApplyForce(-gravityDir);
        }
		else
		{
            ApplyForce(gravityDir);
        }
    }
}
