using Godot;
using System;
using System.Collections.Generic;
public partial class Testscreen : Node2D
{
	List<Cardcontroller> cards = new List<Cardcontroller>();
	private PackedScene cardPrefab;
	List<List<Cardcontroller>> piles = new List<List<Cardcontroller>>();
	List<Cardcontroller> inPlay = new List<Cardcontroller>();
	private int buttonPress = 0;

	public void _On_Area_1(Area2D area){
		Cardcontroller card = area.GetParent() as Cardcontroller;
		GD.Print("Area Entered");

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cardPrefab = ResourceLoader.Load("uid://cfufscq00nrbm") as PackedScene;
		InstantiateCard();
		PileMaker();
		//draw_card();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		update_pile();
		
		GD.Print(inPlay[inPlay.Count-1].IsStacked);
//		if(inPlay[inPlay.Count-1].IsStacked || inPlay[inPlay.Count-1].IsStacked_on_finish || inPlay[inPlay.Count-1].IsStacked_on_deck){
//		inPlay[inPlay.Count-1].is_at_start = false;
//		GD.Print(inPlay.Count-1);
//		inPlay.RemoveAt(inPlay.Count-1);
//		GD.Print(inPlay.Count-1);
//		inPlay[inPlay.Count-1].faceUp = true;
//		inPlay[inPlay.Count-1].UpdateCard();
//			}
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
public void PileMaker()
{
	for(int i = 0; i < 7; i++)
	{
	List<Cardcontroller> tempPile = new List<Cardcontroller>();	
		for(int j = 0; j < i+1; j++)
		{
			tempPile.Add(cards[0]);
			tempPile[j].faceUp = false;
			tempPile[j].UpdateCard();
			
			//GD.Print(tempPile[j].cardID);	
		
			//GD.Print(cards[0].cardID);
			cards.Remove(cards[0]);
			//GD.Print(cards[0].cardID.ToString() + "\n");
			tempPile[j].Position = new Vector2(i*75,j*110);
			//GD.Print(i);
			//GD.Print(j);
		}
	tempPile.Reverse();
	tempPile[0].faceUp = true;
	tempPile[0].UpdateCard();

	
	piles.Add(tempPile);
	GD.Print("Cap: " + piles[i].Capacity.ToString());
	}
	}
	public void update_pile()
	{
	for(int i = 0; i < piles.Count; i++)
	{ 
	if(piles[i][0].IsStacked || piles[i][0].IsStacked_on_finish	 || piles[i][0].IsStacked_on_deck)
	{
	//	cards.Add(piles[i][0]);
		piles[i].RemoveAt(0);
		if(piles[i].Count == 0)
		{
			piles.RemoveAt(i);
//			piles[i].Clear();
			break;
		} 
//		else if(piles[i].Count > 0){
//
//		}
		piles[i][0].faceUp = true;
		piles[i][0].UpdateCard();				
	}	

	}
	}


private void draw_card()
{
		buttonPress++;
		int length = inPlay.Count-1;
		//GD.Print(buttonPress);
		
		//tilføjer kort som kan ses og trækkes ud fra det resterende dæk
		if(cards.Count == 1){
			inPlay.Add(cards[0]);
			cards.Clear();
		} else if(cards.Count > 0){
			inPlay.Add(cards[0]);
//			inPlay[length].Show();
			cards.RemoveAt(0);	
		} 
		//Fylder cards op når den er tom
		else if(cards.Count == 0){
			GD.Print("Hello World");
			inPlay.Reverse();
			for(int i = 0; i < inPlay.Count;i++){
				cards.Add(inPlay[i]);
				cards[cards.Count-1].faceUp = false;
//				cards[cards.Count-1].Hide();
				GD.Print(i);
			}
			inPlay.Clear();
			for (int i = 0; i < cards.Count; i++)
				{
				cards[i].Position =  new Vector2(400, 0);
				cards[i].faceUp = false;
				cards[i].UpdateCard();
				}
		}
		inPlay[length].is_at_start = true;
		inPlay[length].faceUp = true;
		inPlay[length].UpdateCard();
		
		if(buttonPress > 3){
			inPlay[length-1].Position = new Vector2(460,300);
			inPlay[length-2].Position = new Vector2(380,300);
			inPlay[length].Position = new Vector2(540,300);
		if(buttonPress == 22){
				buttonPress = 0;
			}
		} else {
			inPlay[length].Position = new Vector2(380+80*buttonPress,300);
		}

		if(length > 0){
		inPlay[length-1].faceUp = false;
		inPlay[length-1].UpdateCard();
		}
			GD.Print(inPlay[length].cardID);

//		GD.Print(cards.Count);
//		GD.Print(length);
}
}
