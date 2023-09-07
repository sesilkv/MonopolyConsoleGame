namespace Monopoly;

public class Card : Tile
{
	private CardType _typeCard; // enum
	private int _cardDesc;

	public CardType TypeCard { get => _typeCard; set => _typeCard = value; }
	public int CardDesc { get => _cardDesc; set => _cardDesc = value; }

	public Card(int position, CardType typeCard) : base(position)
	{
		// position = position;
		_typeCard = typeCard;
	}

	public Card(CardType typeCard, int position) : base(position)
	{
	}

}
