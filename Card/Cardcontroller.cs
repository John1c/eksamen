using Godot;
using System;
using System.Collections.Generic;

public partial class Cardcontroller : Node2D
{
	List<card> cards = new List<card>();
    public class card
    {
        public int suit { get; set; } = 0; //0 = diamonds 1 = clubs 2 = hearts 3 = spades
        public int rank { get; set; } = 0; //1-13
        public bool faceup { get; set; } = false; //boolean for if the card is face up or not

        public card(int suit, int rank)
        {
            this.suit = suit;
            this.rank = rank;
            this.faceup = faceup;
        }    
    }

public void Deck(){
    createDeck();
    ShuffleDeck();
}
public void createDeck(){
    GD.Print("Creating deck");
    for (int i = 0; i < 4; i++)
    {
        for (int j = 1; j <= 13; j++)
        {
            card card = new card(i, j); // Create a new card object
            cards.Add(card); // Add the card to the list
        }
    }
}
public void ShuffleDeck(){
    //randomize the deck
    GD.Print("Shuffling deck");
    Random rnd = new Random();
    for (int i = 0; i < cards.Count; i++)
    {
        int r = rnd.Next(cards.Count);
        card temp = cards[i];
        cards[i] = cards[r];
        cards[r] = temp;
    }
}



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Deck();
        foreach (card card in cards)
        {
            GD.Print(card.rank + " of " + card.suit);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
