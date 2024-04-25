using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Mainscene : Node2D
{
    public Label _TidTager;
	public Timer _Timer;
	public int TimeStarted = 0;


	static List<Card> cards = new List<Card>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_TidTager = GetNode<Label>("GUI/Timer/TidTager");
		_Timer = GetNode<Timer>("GUI/Timer");
		_Timer.Start();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
public void _DisplayTimer(){
	TimeStarted++;
	_TidTager.Text = "Tid: " + TimeStarted;
}


public void Stack_Check(){ //

 }
public void sendback(){ //funktion til at sætte tilbage hvis man ikke finder et match

}
public void Stack(){ //funktion til stacke

}
public void undo(){

}

public void Restart(){

}

}
