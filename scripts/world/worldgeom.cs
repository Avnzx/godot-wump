using Godot;
using System;

public class worldgeom : Node
{
	private SceneManager? m_sceneManager;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {	
		m_sceneManager = GetNode<SceneManager>("/root/SceneManager");
		GameState _gamestate = GetNode<GameState>("/root/GameState");
		RoomFactory _factory = new RoomFactory();

		AddChild(_factory,true);
		_factory.Initialise(GetPath());


		// TODO: remove, only for debugging
		_isflipped = true;

		//prevRoom = new int[2] {3,1};

		// create roomgroup
		_roomList = _factory.CreateRoomGroup(_isflipped);

		if (prevRoom == null && _gamestate.adjacency != null) {
			Godot.Collections.Array arr = (Godot.Collections.Array) _gamestate.adjacency![_gamestate.CurrentPlayerRoom];
			for (int i = 1; i < (_roomList.Length); i++) {
				_roomList[i].roomindex = (int) arr[i-1];
			}
		} else if (_gamestate.adjacency != null) {
			GD.Print(prevRoom);
			Godot.Collections.Array arr = (Godot.Collections.Array) _gamestate.adjacency![_gamestate.CurrentPlayerRoom];

			int previousroom = arr.IndexOf(prevRoom![1]);

			int[] ordered = new int[3];
			ordered[prevRoom![0] - 1] = (int) arr[previousroom % 3];
			ordered[(prevRoom[0] % 3)] = (int) arr[previousroom + 1 % 3];
			ordered[((prevRoom[0] + 1) % 3)] = (int) arr[previousroom + 2 % 3];

			GD.Print(ordered);

			for (int i = 0; i < ordered.Length; i++) {
				_roomList[i+1].roomindex = ordered[i];
			}

		}
		
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
	public bool _isflipped = false;

	// direction + roomidx
	private int[]? prevRoom; 

	private CustRoom[]? _roomList = new CustRoom[4];

	private RoomFactory? _factory;
	private GameState? _gamestate;
}
