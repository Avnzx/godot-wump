using Godot;
using System;

public class CustButton : Godot.Button
{
    ShooterButtonAction whatami;
    
    public override void _Ready(){
        this.Connect("pressed", this, "ButtonPressed");
        if (this.Text == "Shoot") {
            whatami = ShooterButtonAction.Shoot;
        } else {
            whatami = ShooterButtonAction.Clear;
        }

        AddUserSignal("ShooterButtonTriggered");
		this.Connect("ShooterButtonTriggered", GetParent().GetParent<Shooter>(), "HandleButtonSignal");

    }

    public void ButtonPressed() {
        EmitSignal("ShooterButtonTriggered", new Godot.Collections.Array{whatami});
    }

    public enum ShooterButtonAction {Shoot, Clear}


    [Signal]
    public delegate void ShooterButtonTriggered(ShooterButtonAction actn);
}
