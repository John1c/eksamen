using Godot;
using System;
using System.Collections.Generic;

public partial class Card : Node2D
{
	public int Suit { get; }
    public int Rank { get; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    
	public Card(int suit, int rank)
    {
        Suit = suit; // Using numbers for suits (0-3)
        Rank = rank; // Using numbers for ranks (1-13)
    }
}
