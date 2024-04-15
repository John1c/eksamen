using Godot;
using System;
using System.Collections;

public partial class Mainscene : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
public void Stack_Check(){ // dur ikke
	if (CardID.pattern[1] == CardID.pattern[2]+1 || CardID.pattern[1] == CardID.pattern[2]-1){ // til pattern tjek
		if (CardID.nr[1] == CardID.nr[1]+1){ // til tjek af Nr
		 Stack();
		}else{
			sendback();
		}
	}else{
		sendback();
	}
 }
public void sendback(){ //funktion til at sætte tilbage hvis man ikke finder et match

}
public void Stack(){ //funktion til stacke

}


}
