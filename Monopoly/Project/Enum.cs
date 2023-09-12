namespace Monopoly;

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

public enum CardDesc
{
	GetOutOfJail,
	Move,
	Pay,
	ReceiveMoney //Dictionary<enum, delegate>
}

public enum TileType 
{
	Go,
	VisitingJail,
	FreeParking,
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