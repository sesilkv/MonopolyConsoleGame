namespace Monopoly;

public delegate bool ActionCardDelegate(GameController game);
public class Card
{
	private string? _cardName;
	private string? _cardDesc;
	private CardType _cardType;
	private ActionCardDelegate _actionCard;
	
	public Card(string cardName, string cardDescription, CardType cardType)
	{
		_cardName = cardName;
		_cardDesc = cardDescription;
		// _actionCard = actionCard;
		_cardType = cardType;
	}
	
	public string CardName { get => _cardName; set => _cardName = value; }

	public string? CardDescription { get => _cardDesc; set => _cardDesc = value; }
	
	public CardType CardType { get => _cardType; set => _cardType = value; }
	
	public ActionCardDelegate ActionCardDelegate { get => _actionCard; set => _actionCard = value; }

	public bool ExecuteActionCard(ActionCardDelegate action, GameController game)
	{
		_actionCard = action;
		action.Invoke(game);
		return true;
	}
}