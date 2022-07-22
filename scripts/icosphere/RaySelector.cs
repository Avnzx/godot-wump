using Godot;
using System;

class RaySelector : Label {

	RayCast raycast = new RayCast();
	Camera? camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		this.Text = "Placeholder";
		AddChild(raycast);

		camera = GetCamera();
	}

	private Camera GetCamera(){
		return GetNode<Camera>("../../Gimbal/Camera");
	}

	public override void _Input(InputEvent @event) {
	}

	public override void _PhysicsProcess(float delta) {
		// this.Text = ("Mouse speed: %s" % MouseSpeed.get_instant_mouse_speed());
		camera = GetCamera();
		
		if(raycast.IsColliding())
			GD.Print(raycast.GetCollider());

		float ray_length = 100f;
		
		var fromPos = camera!.ProjectRayOrigin(GetViewport().GetMousePosition());
		var toPos = camera.ProjectRayNormal(GetViewport().GetMousePosition()) * ray_length;
		// GD.Print(GetViewport().GetMousePosition());
		
		raycast.Translation = fromPos;
		// GD.Print(fromPos);
		raycast.CastTo = toPos;
		raycast.Enabled = true;
		raycast.DebugShapeThickness = 20;
		raycast.CollideWithAreas = true;
	}
		
}