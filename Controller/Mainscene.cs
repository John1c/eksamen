using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Mainscene : Node2D
{

	static List<Card> cards = new List<Card>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
/* skal lige fixes
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
*/
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
