namespace Monopoly;

public class CardFactory : TileFactory
{
	private TypeCard _typeCard;
	
	public CardFactory(TypeCard typeCard)
	{
		_typeCard = typeCard;
	}

    public TypeCard typeCard { get => _typeCard; set => _typeCard = value; }

    public override Tile GetTile(int position)
	{
		return new Card(typeCard, position);
	}
}

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
		_cardDesc = RandomCardDesc();
	}

	public Card(TypeCard typeCard, int position) : base(position)
	{
	}

	// public override Tile GetTile(int position)
	// {
	// 	return new Card(typeCard, position);
	// }

	public int RandomMoney()
	{
		Random randomMoney = new Random();
		int money = randomMoney.Next(0, 2000);
		return money;
	}

	public int RandomCardDesc()
	{
		Random randomCard = new Random();
		int card = randomCard.Next(0, 10);
		return card;
	}

	// public string CardInstruction()
	// {

	// }
}
