using Godot;
using System;

public class CustRoom : Room {
    public CustRoom() {}
    public CustRoom(int drawnroom) {
        this.drawnroom = drawnroom;
    }

    public int roomindex { get; set; }
    public int drawnroom { get; set; }

}