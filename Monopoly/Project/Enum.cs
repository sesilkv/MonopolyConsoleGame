namespace MonopolyEnum;

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
	LANDMARK,
	PROPERTY,
	UTILITY,
	TAX
}

public enum PropertySituation
{
	Unowned,
	Owned,
	Mortgaged
}