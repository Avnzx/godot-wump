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

		raycast.Enabled = true;
		raycast.DebugShapeThickness = 20;
		raycast.CollideWithAreas = true;

		AddUserSignal("RoomHovered");
		this.Connect("RoomHovered", GetNode<FaceSelect>("/root/Icosphere/faceselector"), "OnRoomHover");
	}


	StaticBody? collider;
	StaticBody? previouscollider;


	public void SendData() {
		// otherwise Godot will crash if we don't ratelimit the method
		if (raycast.IsColliding()) {
			collider = (StaticBody) raycast.GetCollider();
		}

		if (collider != previouscollider) {
			EmitSignal("RoomHovered", new Godot.Collections.Array{collider});
		}

		previouscollider = collider;

	}

	private Camera GetCamera() =>  GetNode<Camera>("../../Gimbal/Camera");

	public override void _Input(InputEvent @event) {
	}

	public override void _PhysicsProcess(float delta) {
		// this.Text = ("Mouse speed: %s" % MouseSpeed.get_instant_mouse_speed());
		camera = GetCamera();

		SendData();
		
		
		float ray_length = 100f;
		
		var fromPos = camera!.ProjectRayOrigin(GetViewport().GetMousePosition());
		var toPos = camera.ProjectRayNormal(GetViewport().GetMousePosition()) * ray_length;
		// GD.Print(GetViewport().GetMousePosition());
		raycast.Translation = fromPos;
		raycast.CastTo = toPos;
		
	}

	[Signal]
    public delegate void RoomHovered(StaticBody room);

		
}