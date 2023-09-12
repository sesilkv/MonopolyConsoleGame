using Monopoly;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class GameController
{
	private GameState _gameState;
	private Board _board;
	private List<IPlayer> _players;
	private int _currPlayer;
	private Dictionary<IPlayer, Tile> _playerPos;
	private Dictionary<IPlayer, int> _playerCash;
	private Dictionary<IPlayer, bool> _jailOrNot;
	private Dictionary<IPlayer, List<Property>> _playerProp;
	// private Dictionary<IPlayer, int> _playerHouses;
	// private Dictionary<IPlayer, PlayerData> _playerData;
	private List<Card> _playerCards;
	private List<IDice> _diceList;
	private List<int> _totalDice;
	private bool _finishTurn;
	public event Action<IPlayer> MoveToJailEvent;
	
	// for tax and utility
	public delegate void PlayerNotificationHandler(string message, string playerName);
	public event PlayerNotificationHandler PlayerNotified;

	public GameController(Board board)
	{
		_gameState = GameState.NotStarted;
		_board = board;
		_players = new List<IPlayer>();
		_playerPos = new Dictionary<IPlayer, Tile>();
		_playerCash = new Dictionary<IPlayer, int>();
		_jailOrNot = new Dictionary<IPlayer, bool>();
		_playerProp = new Dictionary<IPlayer, List<Property>>();
		// _playerHouses = new Dictionary<IPlayer, int>();
		_currPlayer = 0;
		_diceList = new List<IDice>();
		_totalDice = new();
	}

	public GameState GetGameState()
	{
		return _gameState;
	}

	public void SetGameState(GameState state)
	{
		_gameState = state;
	}

	// public void IsWinner(Player player)
	// {
	// 	if()
	// 	{
	// 		EndGame();
	// 		SetGameState(GameState.FINISHED);
	// 	}
	// }

	public void AddPlayer(string name)
	{
		Player player = new Player(name);
		_players.Add(player);
		_playerPos[player] = _board.GetTile(0);
		_playerCash[player] = 2000;
		_jailOrNot[player] = false;
	}

	public IPlayer ActivePlayer()
	{
		if (_players.Count > 0)
		{
			int currPlayer = CurrentPlayer();
			IPlayer activePlayer = _players[currPlayer];
			return activePlayer;
		}
		return null;
	}

	public int CurrentPlayer()
	{
		// method baru
		// if (_currPlayer < 0 || _currPlayer >= _players.Count)
		// {
		// 	_currPlayer = 0;
		// }
		return _currPlayer;
	}

	public int MoneyPlayer()
	{
		IPlayer activePlayer = ActivePlayer();
		if (_playerCash.ContainsKey(activePlayer))
		{
			return _playerCash[activePlayer];
		}
		return 0;
	}

	// for next player
	public bool SwitchTurn()
	{
		_currPlayer = (_currPlayer + 1) % _players.Count;
		return true;
	}

	// overloading switch turn for certain player
	public bool SwitchTurn(IPlayer player)
	{
		if (_players.Contains(player))
		{
			_currPlayer = _players.IndexOf(player);
		}
		return false;
	}

	public bool AddDice(int x)
	{
		if (x > 0)
		{
			Dice dice = new Dice(x);
			_diceList.Add(dice);
			return true;
		}
		else
		{
			return false;
		}
	}

	public int Roll()
	{
		_totalDice.Clear();
		int result = 0;
		foreach (var dice in _diceList)
		{
			result = dice.Roll();
			_totalDice.Add(result);
		}
		return result;
	}
	
	// public int Roll()
	// {
	// 	_totalDice.Clear();
	// 	int resDice = 0;
	// 	foreach (var dice in _diceList)
	// 	{
	// 		int result = dice.Roll();
	// 		resDice += result;
	// 	}

	// 	_totalDice.Add(resDice);
	// 	return resDice;
	// }

	// overload Roll
	public int Roll(int x)
	{
		List<Dice> _diceList = new List<Dice>();
		if (x >= 0 && x < _diceList.Count)
		{
			return _diceList[x].Roll();
		}
		else
		{
			throw new ArgumentException("Invalid dice index.");
		}
	}

	public int TotalDice()
	{
		int total = 0;
		foreach (int result in _totalDice)
		{
			total += result;
		}
		return total;
	}

	public List<int> GetTotalDice()
	{
		return _totalDice;
	}

	// public List<IDice> GetAllDice()
	// {
	// 	return _diceList;
	// }

	public void Move()
	{
		int steps = TotalDice();
		int currPos = GetPlayerPosition();
		IPlayer activePlayer = ActivePlayer();
		if (activePlayer != null && currPos >= 0) //valid or not
		{
			int numOfTiles = _board.GetTileAll(); // total number of tile
			int newPos = (currPos + steps) % numOfTiles;
			
			Tile newTile = _board.GetTile(newPos);
			SetPlayerPosition(newTile); // set to the new tile
			
			// check the player passed the Go tile
			// if(newPos > _board.GetTileAll())
			if(newPos < currPos)
			{
				Go go = _board.Tile[0] as Go;
				int amountAdd = go.AmountStart; // call the amountstart
				
				if(_playerCash.ContainsKey(activePlayer))
				{
					_playerCash[activePlayer] += amountAdd;
				}
				
				NotifyPlayer("You have got the amount start $200", activePlayer.GetName());
				// newPos = newPos % _board.GetTileAll();
			}
			
			TileAction(newTile); //buy, rent, draw
								 // IsWinner(ActivePlayer());
		}
	}

	public int GetPlayerPosition()
	{
		IPlayer activePlayer = ActivePlayer();
		if (activePlayer != null && _playerPos.ContainsKey(activePlayer))
		{
			Tile tile = _playerPos[activePlayer];
			return tile.Position;
		}
		return 0; // there's no active player/their pos isnt tracked
	}

	// overload getplayerpos
	public int GetPlayerPosition(IPlayer player)
	{
		if (_playerPos.ContainsKey(player))
		{
			Tile tile = _playerPos[player];
			return tile.Position;
		}
		return -1; //player/position not found
	}

	public void SetPlayerPosition(Tile tile)
	{
		IPlayer activePlayer = ActivePlayer();
		if (_playerPos.ContainsKey(activePlayer)) //already contain for active player
		{
			_playerPos[activePlayer] = tile; // replace the old pos to the new one
		}
		else
		{
			_playerPos.Add(activePlayer, tile); // first time setting the player's position, add new entry
		}
	}

	public string TileName()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();

		if (activePlayer != null && currPos >= 0)
		{
			Tile currTile = _board.GetTile(currPos);
			return currTile.TileName;
		}
		return null;
	}
	
	public string TileDescription()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();

		if (activePlayer != null && currPos >= 0)
		{
			Tile currTile = _board.GetTile(currPos);
			return currTile.TileDescription;
		}
		return null;
	}
	
	// overload TileName parameter inputan user
	// public string TileName(IPlayer player)
	// {
	// 	Player _player = GetPlayerByName(player);
	// 	if (_player != null)
	// 	{
	// 		int _playerPos = GetPlayerPosition(_player);
	// 		if (_playerPos >= 0){
	// 			Tile tile = _board.GetTile();
	// 			return tile.TileName;
	// 		}
	// 	}
	// 	return null;
	// }

	// public IPlayer GetPlayerByName(IPlayer player)
	// {
	// 	return players.FirstOrDefault(player => player.GetName() == playerName);
	// }

	public void TileAction(Tile tile)
	{
		switch (tile)
		{
			case Tax taxTile:
				PayTax(taxTile);
				break;
			case Utility utilityTile:
				PayUtility(utilityTile);
				break;
			default:
				break;
		}
	}

	public void MoveToJail()
	{
		IPlayer activePlayer = ActivePlayer();
		Jail jailTile = GetJailTile();

		if (activePlayer != null && jailTile != null)
		{
			SetPlayerPosition(jailTile);
			_jailOrNot[activePlayer] = true;
			// _jailTurns[activePlayer] = 0;
			MoveToJailEvent?.Invoke(activePlayer);
		}
	}

	public List<IPlayer> Jailed()
	{
		List<IPlayer> jailedPlayer = new List<IPlayer>();
		foreach(var player in _jailOrNot.Keys)
		{
			// Console.WriteLine($"{player}");
			if (_jailOrNot[player])
			{
				jailedPlayer.Add(player);
			}
		}
		return jailedPlayer;
	}
	
	public bool GetOutOfJailByDice()
	{
		IPlayer activePlayer = ActivePlayer();
		if (activePlayer != null && _jailOrNot.ContainsKey(activePlayer) && _jailOrNot[activePlayer])
		{
			List<int> totDice = GetTotalDice();
			if (totDice.Count == 2 && totDice[0] == totDice [1])
			{
				_jailOrNot[activePlayer] = false;
				return true;
			}
		}
		return false;
	}
	
	public bool PayToGetOutOfJail()
	{
		IPlayer activePlayer = ActivePlayer();
		if (activePlayer != null && _jailOrNot.ContainsKey(activePlayer) && _jailOrNot[activePlayer])
		{
			Jail jailTile = GetJailTile();
			
			if (jailTile != null)
			{
				int payJail = jailTile.PayJail;
				
				if(_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= payJail)
				{
					_playerCash[activePlayer] -= payJail;
					_jailOrNot[activePlayer] = false;
					return true;
				}
			}
		}
		return false;
	}

	public Jail GetJailTile()
	{
		for (int pos = 0; pos < _board.GetTileAll(); pos++)
		{
			Tile tile = _board.GetTile(pos);
			if (tile is Jail jailTile)
			{
				return jailTile;
			}
		}
		return null;
	}

	public void NotifyPlayer(string message, string playerName)
	{
		PlayerNotified?.Invoke(message, playerName);
	}

	public TaxUtilityState PayTax(Tile tile)
	{
		IPlayer activePlayer = ActivePlayer();
		if(tile is Tax taxTile)
		{
			int taxAmount = taxTile.PayTax;
			if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= taxAmount)
			{
				_playerCash[activePlayer] -= taxAmount;
				NotifyPlayer("You have successfully paid the tax $100", activePlayer.GetName());
				return TaxUtilityState.Success;
			}
		}
		NotifyPlayer("You don't have enough money to pay the tax $100", activePlayer.GetName());
		return TaxUtilityState.NotEnoughMoney;
	}

	public TaxUtilityState PayUtility(Tile tile)
	{
		IPlayer activePlayer = ActivePlayer();
		if(tile is Utility utilityTile)
		{
			int utilityAmount = utilityTile.PayUtility;
			if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= utilityAmount)
			{
				_playerCash[activePlayer] -= utilityAmount;
				NotifyPlayer("You have successfully paid the utility $50", activePlayer.GetName());
				return TaxUtilityState.Success;
			}
		}
		NotifyPlayer("You don't have enough money to pay the utility $50", activePlayer.GetName());
		return TaxUtilityState.NotEnoughMoney;
	}

	public List<Property> PlayerProperty()
	{
		IPlayer activePlayer = ActivePlayer();
		if (activePlayer != null && _playerProp.ContainsKey(activePlayer))
		{
			return _playerProp[activePlayer];
		}
		return new List<Property>();
	}

	public PropState BuyProperty()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();

		if (activePlayer != null && currPos >= 0)
		{
			Tile currTile = _board.GetTile(currPos);

			if (!(currTile is Property prop) || prop.Owner != null)
			{
				return PropState.AlreadyOwned;
			}
			
			// if (prop.PropType != PropertyType.Country)
			// {
			// 	return PropState.NotProperty;
			// }

			// int startPos = _board.GetTile(0).Position;
			// int numOfTile = _board.GetTileAll();

			// int totalSteps = TotalDice();
			// int stepsPassed = (currPos - startPos + totalSteps) % numOfTile;
			// if (stepsPassed <= totalSteps)
			// {
			// 	return PropState.AlreadyOwned;
			// }

			int propertyPrice = prop.PriceProp;

			if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= propertyPrice)
			{
				_playerCash[activePlayer] -= propertyPrice;
				_playerPos[activePlayer] = prop;
				prop.SetOwner(activePlayer.GetName());
				if (!_playerProp.ContainsKey(activePlayer))
				{
					_playerProp[activePlayer] = new List<Property>();
				}
				_playerProp[activePlayer].Add(prop);
			}
			else
			{
				return PropState.NotEnoughMoney;
			}
		}
		return PropState.Success;
	}
	
	public bool SellProperty()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();
		Tile currTile = _board.GetTile(currPos);
		Property prop = currTile as Property;
		if (_playerProp.ContainsKey(activePlayer))
		{
			List<Property> props = _playerProp[activePlayer];
			if (props.Contains(prop) && prop.Owner == activePlayer.GetName())
			{
				int propertyPrice = prop.PriceProp;

				if (_playerCash.ContainsKey(activePlayer))
				{
					_playerCash[activePlayer] += propertyPrice;
					props.Remove(prop);
					prop.SetOwner(null);
				}
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}
	
	// public int PlayerHouse()
	// {
	// 	IPlayer activePlayer = ActivePlayer();
	// 	int currPos = GetPlayerPosition();
	// 	if (activePlayer != null && currPos > 0)
	// 	{
	// 		Tile currTile = _board.GetTile(currPos);
	// 		if (currTile is Property prop && prop.Owner == activePlayer.GetName())
	// 		{
	// 			return prop.NumberOfHouse;
	// 		}
	// 	}
	// 	return 0;
	// }

	public bool BuyHouse()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();
		Tile currTile = _board.GetTile(currPos);
		Property prop = currTile as Property;

		if (prop.PropSituation != PropertySituation.Owned || prop.Owner != activePlayer.GetName())
		{
			return false;
		}

		if (prop.PropType != PropertyType.Country)
		{
			return false;
		}

		int housePrice = prop.HousePrice;
		int maxHouse = 3;

		if (prop.NumberOfHouse >= maxHouse)
		{
			return false;
		}

		if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= housePrice)
		{
			_playerCash[activePlayer] -= housePrice;
			prop.AddHouse();
			return true;
		}
		else
		{
			return false;
		}
	}
	

	// RandomChanceCard()
	// RandomCommunityChestCard()
	// IsWinner() bool bankrupt
	// EndGame()
}