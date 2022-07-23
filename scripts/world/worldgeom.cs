using Godot;
using System;

public class worldgeom : Node
{
	private SceneManager? m_sceneManager;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {	
		m_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		_gamestate = GetNode<GameState>("/root/GameState");
		_factory = new RoomFactory();

		this.Name = "worldgeom";

		AddChild(_factory,true); 
		_factory.Initialise(GetPath());

		CreateRoomGroup();
		// _factory.RemoveRoom(_roomList[1]);
		// _factory.RemoveRoomGroup(_roomList);
	}

	public void HandleRoomDetector(Godot.Collections.Array arr) {
		Vector3 localtranslation = (Vector3) arr[3];

		GD.Print(arr);


		if (arr[0] == null) {
			prevRoom = null;
		} else {
			prevRoom = new int[2];
			prevRoom[0] = (int) arr[0];
			prevRoom[1] = (int) arr[1];
		}

		_gamestate.CurrentPlayerRoom = (int) arr[2];
		
		// _factory.RemoveRoom(_roomList[2]);
		_oldRooms = _roomList;
		_roomList = null;
		_factory!.RemoveRoomGroup(_oldRooms!);

		_factory.Translation = _factory.Translation + (localtranslation*0.9f);
		CreateRoomGroup();
	}

	public void CreateRoomGroup() {
		
		//prevRoom = new int[2] {3,1};

		// create roomgroup
		_roomList = _factory!.CreateRoomGroup(_isflipped);

		if (_gamestate!.adjacency == null) {
			CreateRoomGroup();
		} else if (_gamestate.adjacency[0] == null )
			CreateRoomGroup();



		if (prevRoom == null) {
			GD.Print("previous is null");

			Godot.Collections.Array arr = (Godot.Collections.Array) _gamestate.adjacency![_gamestate.CurrentPlayerRoom];
			for (int i = 1; i < (_roomList.Length); i++) {
				_roomList[i].roomindex = (int) arr[i-1];
			}
		} else {
			GD.Print("previous is NOT null");
			GD.Print(prevRoom);
			Godot.Collections.Array arr = (Godot.Collections.Array) _gamestate.adjacency![_gamestate.CurrentPlayerRoom];

			int previousroom = arr.IndexOf(prevRoom![1]);

			int[] ordered = new int[3];
			ordered[prevRoom![0] - 1] = (int) arr[previousroom % 3];
			ordered[(prevRoom[0] % 3)] = (int) arr[(previousroom + 1) % 3];
			ordered[((prevRoom[0] + 1) % 3)] = (int) arr[(previousroom + 2) % 3];

			GD.Print(ordered);

			for (int i = 0; i < ordered.Length; i++) {
				_roomList[i+1].roomindex = ordered[i];
			}

		}

		if(_gamestate.debugMode)
			GD.Print(_gamestate.CurrentPlayerRoom);

		// flip next room
		_isflipped = !_isflipped;
	}


    public override void _Input(InputEvent @event) {

		if (Input.IsActionPressed("toggle_map")) {
			GD.Print(m_sceneManager);
			m_sceneManager!.DeferredGotoScene("res://scenes/icosphere/Main.tscn");
		}

    }

	[Export]
	public bool _isflipped = false;

	// direction + roomidx
	private int[]? prevRoom; 

	private CustRoom[]? _roomList = new CustRoom[4];
	private CustRoom[]? _oldRooms = new CustRoom[4];

	private RoomFactory? _factory;
	private GameState? _gamestate;
}
