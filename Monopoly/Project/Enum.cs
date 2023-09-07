namespace Monopoly;

public enum GameState 
{
	NOT_STARTED,
	IN_PROGRESS,
	FINISHED
}

public enum CardType 
{
	CHANCE,
	COMMUNITY_CHEST
}

public enum CardDesc
{
	GET_OUT_OF_JAIL,
	MOVE,
	PAY,
	RECEIVE_MONEY //Dictionary<enum, delegate>
}

public enum TileType 
{
	GO,
	VISITING_JAIL,
	FREE_PARKING,
	GO_TO_JAIL,
	PROPERTY,
	UTILITY,
	TAX,
	CHANCE,
	COMMUNITY_CHEST
}

public enum PropertyType 
{
	COUNTRY,
	STATION,
	HARBOUR,
	AIRPORT
}

public enum PropertySituation
{
	UNOWNED,
	OWNED
}

public enum PurchasePropertyState
{
	SUCCESS,
	ALREADY_OWNED,
	NOT_ENOUGH_MONEY
}