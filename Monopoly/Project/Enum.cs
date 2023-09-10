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

public enum PropState
{
	SUCCESS,
	ALREADY_OWNED,
	NOT_ENOUGH_MONEY
}

public enum TaxUtilityState
{
	SUCCESS,
	NOT_ENOUGH_MONEY
}

// public PurchaseState BuyProperty()
// 	{
// 		Player activePlayer = ActivePlayer();
// 		int currPos = GetPlayerPosition();

// 		if (activePlayer != null && currPos >= 0)
// 		{
// 			Tile currTile = _board.GetTile(currPos);

// 			if (!(currTile is Property prop) || prop.Owner != null)
// 			{
// 				return PurchaseState.ALREADY_OWNED;
// 			}

// 			int startPos = _board.GetTile(0).Position;
// 			int numOfTile = _board.GetTileAll();

// 			int totalSteps = TotalDice();
// 			int stepsPassed = (currPos - startPos + totalSteps) % numOfTile;
// 			if (stepsPassed <= totalSteps)
// 			{
// 				return PurchaseState.ALREADY_OWNED;
// 			}

// 			int propertyPrice = prop.PriceProp;

// 			if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= propertyPrice)
// 			{
// 				_playerCash[activePlayer] -= propertyPrice;
// 				_playerPos[activePlayer] = prop;
// 				prop.SetOwner(activePlayer.GetName());
// 				if (!_playerProp.ContainsKey(activePlayer))
// 				{
// 					_playerProp[activePlayer] = new List<Property>();
// 				}
// 				_playerProp[activePlayer].Add(prop);
// 			}
// 			else
// 			{
// 				return PurchaseState.NOT_ENOUGH_MONEY;
// 			}
// 		}
// 		return PurchaseState.SUCCESS;
// 	}