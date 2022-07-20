using Godot;
using System;

public class worldgeom : Node
{
	private SceneManager? m_sceneManager;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {	
		m_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		RoomFactory _factory = new RoomFactory();

		
		GD.Print(_factory);
		AddChild(_factory,true);



		_factory.Initialise(GetPath());
		GD.Print(GetPath());

		_isflipped = true;


		_roomList = _factory.CreateRoomGroup();
		
		// _factory.RemoveRoom(_roomList[1]);
		// _factory.RemoveRoomGroup(_roomList);
	}

    public override void _Input(InputEvent @event) {

		if (Input.IsActionPressed("toggle_map")) {
			GD.Print(m_sceneManager);
			m_sceneManager!.DeferredGotoScene("res://scenes/icosphere/Main.tscn");
		}


    }

	[Export]
	public bool _isflipped = true;

	private CustRoom[]? _roomList = new CustRoom[4];

	private RoomFactory? _factory;
}
