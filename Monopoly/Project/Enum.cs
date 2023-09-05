namespace Monopoly;

public enum GameState 
{
	WIN,
	BANKRUPT,
	ALIVE
}

public enum TypeCard 
{
	CHANCE,
	COMMUNITY_CHEST
}

public enum TileType 
{
	GO,
	VISITING_JAIL,
	FREE_PARKING,
	GO_TO_JAIL,
	PROPERTY,
	UTILITY,
	TAX
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
	Unowned,
	Owned,
	Mortgaged
}