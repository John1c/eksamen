using Godot;
using System;
using System.Collections.Generic;
public partial class Testscreen : Node2D
{
	List<Cardcontroller> cards = new List<Cardcontroller>();
	private PackedScene cardPrefab;

	public void _On_Area_1(Area2D area){
		Cardcontroller card = area.GetParent() as Cardcontroller;
		GD.Print("Area Entered");

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("uid://cfufscq00nrbm") as PackedScene;
		InstantiateCard();
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
			instantiatecard.OnCardInstantiate(j,i);
			
//			if(i%2==0){
//				instantiatecard.faceUp = false;
//			}

			cards.Add(instantiatecard); // Add the card to the list
//			GD.Print("ID: " + instantiatecard.cardID + " Pattern: " + instantiatecard.CardPattern);
//			GD.Print(instantiatecard.CardPattern);
		}
	}
		ShuffleDeck();
		for (int i = 0; i < cards.Count; i++)
		{
			cards[i].Position =  new Vector2(i*30, i%3*150);
			AddChild(cards[i]);
			cards[i].UpdateCard();
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

