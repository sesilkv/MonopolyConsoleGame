namespace Monopoly;

// there are 7 enums
public enum GameState 
{
	NotStarted,
	InProgress,
	Finished
}

public enum CardType 
{
	Chance,
	CommunityChest
}

public enum TileType 
{
	Go,
	VisitingJail,
	GoToJail,
	Property,
	Utility,
	Tax,
	Chance,
	CommunityChest
}

public enum PropertyType 
{
	Country,
	Station,
	Harbour,
	Airport
}

public enum PropertySituation
{
	Unowned,
	Owned
}

public enum PropState
{
	Success,
	AlreadyOwned,
	NotEnoughMoney,
	NotProperty
}

public enum TaxUtilityState
{
	Success,
	NotEnoughMoney
}