using Godot;
using System;
using System.Collections.Generic;

public partial class Card : Node2D
{

	static List<Card> cards = new List<Card>();
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

	public void Card_gen(){
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 1; j <= 13; j++)
            {
                Card card = new Card(i, j); // Create a new card object
                cards.Add(card); // Add the card to the list
            }
        }

        // Display the generated cards in the console
        foreach (Card card in cards)
        {
            Console.WriteLine($"Card: {card.Rank} of {card.Suit}");
        }
     }
	}

	public Card(int suit, int rank)
    {
        Suit = suit; // Using numbers for suits (0-3)
        Rank = rank; // Using numbers for ranks (1-13)
    }
}
