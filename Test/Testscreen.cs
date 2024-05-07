using Godot;
using System;
using System.Collections.Generic;
public partial class Testscreen : Node2D
{
	List<Cardcontroller> cards = new List<Cardcontroller>();
	private PackedScene cardPrefab;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("uid://cfufscq00nrbm") as PackedScene;
		InstantiateCard();
		ShuffleDeck();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
private void InstantiateCard()
{	
	GD.Print("Instantiate card");

	for (int i = 0; i < 4; i++)
	{
		for (int j = 1; j <= 13; j++)
		{
			//	GD.Print("j="+ j +" I=" + i);
			Cardcontroller instantiatecard = cardPrefab.Instantiate<Node2D>() as Cardcontroller;
			instantiatecard.Position =  new Vector2(i*100, j*25);
			instantiatecard.OnCardInstantiate(j,i);
			AddChild(instantiatecard);
			
			cards.Add(instantiatecard); // Add the card to the list
//			GD.Print("ID: " + instantiatecard.cardID + " Pattern: " + instantiatecard.CardPattern);
//			GD.Print(instantiatecard.CardPattern);
			if (i==1){
				instantiatecard.UpdateCard();
			}
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
		Cardcontroller temp = cards[i];
		cards[i] = cards[r];
		cards[r] = temp;
	}
}

}

