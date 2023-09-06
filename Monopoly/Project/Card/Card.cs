namespace Monopoly;

public class Card : Tile
{
	private TypeCard _typeCard; // enum
	private int _cardDesc;

	public TypeCard TypeCard { get => _typeCard; set => _typeCard = value; }
	public int CardDesc { get => _cardDesc; set => _cardDesc = value; }

	public Card(int position, TypeCard typeCard) : base(position)
	{
		// position = position;
		_typeCard = typeCard;
	}

	public Card(TypeCard typeCard, int position) : base(position)
	{
	}

}
