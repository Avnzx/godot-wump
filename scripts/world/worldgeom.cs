using Godot;
using System;

public class worldgeom : Node
{
	private SceneManager? m_sceneManager;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {	
		m_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		RoomFactory _factory = RoomFactory.Instance;

		if (RoomFactory.IsInstanceValid(_factory)){
			GD.Print("instance IS valid??");
		} else {
			GD.Print("instance INVALID :(");
		}

		
		GD.Print(_factory);
		AddChild(_factory,true);

		_factory.Initialise(GetPath());
		GD.Print(GetPath());
		_factory.NewRoom();
		_factory.NewRoom().Translation = Vector3.Left;
		_factory.NewRoom();
		
		

	}

    public override void _Input(InputEvent @event) {

		if (Input.IsActionPressed("toggle_map")) {
			GD.Print(m_sceneManager);
			m_sceneManager!.DeferredGotoScene("res://scenes/icosphere/Main.tscn");
		}


    }

	private float _halfpolygon;


}
