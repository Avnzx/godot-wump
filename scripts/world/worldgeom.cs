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

		_halfpolygon = CoordHelper.PolygonFlatToFlatDistance(6,15f);
		_threetwothpoly = _halfpolygon * (3f/2f);

		_factory.Initialise(GetPath());
		GD.Print(GetPath());

		_isflipped = true;


		if (!_isflipped) {
			// centre
			_roomList![0] = _factory.NewRoom();
			// bottom
			_roomList[1] = _factory.IllusionRoom();
			_roomList[1].Translation = Vector3.Forward * _halfpolygon;
			// top left
			_roomList[2] = _factory.IllusionRoom();
			_roomList[2].Translation = new Vector3(15f*1.5f,0,_halfpolygon/2f);
			// top right
			_roomList[3] = _factory.IllusionRoom();
			_roomList[3].Translation = new Vector3(-15f*1.5f,0,_halfpolygon/2f);
		} else {
			// centre
			_roomList![0] = _factory.NewRoom();
			// top
			_roomList[1] = _factory.IllusionRoom();
			_roomList[1].Translation = -Vector3.Forward * _halfpolygon;
			// bottom left
			_roomList[2] = _factory.IllusionRoom();
			_roomList[2].Translation = new Vector3(15f*1.5f,0,-_halfpolygon/2f);
			// bottom right
			_roomList[3] = _factory.IllusionRoom();
			_roomList[3].Translation = new Vector3(-15f*1.5f,0,-_halfpolygon/2f);

		}
		
		// _factory.RemoveRoom(_roomList[1]);
		// _factory.RemoveRoomGroup(_roomList);
	}

	public void CreateRoomGroup() {
		
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

	private float _halfpolygon;
	private float _threetwothpoly;


}
