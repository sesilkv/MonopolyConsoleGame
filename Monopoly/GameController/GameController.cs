using Monopoly;
using System.Collections.Generic;
using System.Runtime.InteropServices;

class GameController
{
	private GameState _gameState;
	private Board _board;
	private List<IPlayer> _players;
	private int _currPlayer;
	private Dictionary<IPlayer, Tile> _playerPos = new Dictionary<IPlayer, Tile>();
	private Dictionary<IPlayer, int> _playerMoney = new Dictionary<IPlayer, int>();
	private Dictionary<IPlayer, bool> _jailOrNot;
	private Dictionary<IPlayer, List<Property>> _playerProperty;
	private List<Card> _playerCards;
	private List<IDice> _diceList = new List<IDice>();
	private List<int> _totalDice;

	public GameController(Board board)
	{
		_gameState = GameState.NOT_STARTED;
		_board = board;
		_players = new List<IPlayer>();
		_currPlayer = 0;
		_diceList = new List<IDice>();
		_totalDice = new();
	}

	public GameState GetGameState()
	{
		return _gameState;
	}

	public void SetGameState(GameState status)
	{
		_gameState = status;
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
		_playerMoney[player] = 2000;
	}

	public Player ActivePlayer()
	{
		if (_players.Count > 0)
		{
			int currPlayer = CurrentPlayer();
			IPlayer activePlayer = _players[currPlayer];
			return (Player)activePlayer;
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

	// for next player
	public bool SwitchTurn()
	{
		Player activePlayer = ActivePlayer();
		// if(activePlayer.IsBankrupt && activePlayer.IsInJail && !activePlayer.HasRolledDoubles && !activePlayer.HasPaidToGetOut && !activePlayer.HasCompletedActions)
		// {
		// 	return false;
		// }
		
		_currPlayer = (_currPlayer + 1) % _players.Count;
		return true;
	}

	// overloading switch turn for certain player
	public bool SwitchTurn(IPlayer player)
	{
		if(_players.Contains(player))
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

	// public bool Roll()
	// {
	// 	int result = 0;
	// 	foreach (var dice in _diceList)
	// 	{
	// 		result = dice.Roll();
	// 		_totalDice.Add(result);
	// 	}
	// 	return true;
	// }

	// public int TotalDice()
	// {
	// 	int total = 0;
	// 	foreach (int result in _totalDice)
	// 	{
	// 		total += result;
	// 	}
	// 	return total;
	// }

	// public List<int> GetTotalDice()
	// {
	// 	return _totalDice;
	// }

	// overload Roll
	public int Roll()
	{
		int tot = 0;
		foreach (var dice in _diceList)
		{
			int result = dice.Roll();
			tot += result;
		}
		
		_totalDice.Add(tot);
		return tot;
	}

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

	public List<IDice> GetAllDice()
	{
		return _diceList;
	}

	public void Move()
	{
		int steps = Roll();
		int currPos = GetPlayerPosition();
		if (currPos >= 0) //valid or not
		{
			int numOfTiles = _board.GetTileAll(); // total number of tile
			int newPos = (currPos + steps) % numOfTiles;

			Tile newTile = _board.GetTile(newPos);
			SetPlayerPosition(newTile); // set to the new tile
			TileAction(newTile); //buy, rent, draw
								 // IsWinner(ActivePlayer());
		}
	}

	public int GetPlayerPosition()
	{
		Player activePlayer = ActivePlayer();
		if (activePlayer != null && _playerPos.ContainsKey(activePlayer))
		{
			Tile tile = _playerPos[activePlayer];
			return tile.Position;
		}
		return -1; // there's no active player/their pos isnt tracked
	}

	// overload getplayerpos
	// public int GetPlayerPosition(IPlayer player)
	// {

	// }

	public void SetPlayerPosition(Tile tile)
	{
		Player activePlayer = ActivePlayer();
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
		Player activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();

		if (activePlayer != null && currPos >= 0)
		{
			Tile currTile = _board.GetTile(currPos);
			return currTile.TileName;
		}

		return null;
	}
	// overload parameter inputan user

	public string TileDesc()
	{
		Player activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();

		if (activePlayer != null && currPos >= 0)
		{
			Tile currTile = _board.GetTile(currPos);
			return currTile.TileDescription;
		}

		return null;
	}

	public void TileAction(Tile tile)
	{
		// switch (tile)
		// {
		// 	case Tax tax:
		// 	break;
		// 	break;
		// }
	}

	public List<Property> PlayerProperty()
	{
		Player activePlayer = ActivePlayer();
		if (activePlayer != null && _playerProperty.ContainsKey(activePlayer))
		{
			return _playerProperty[activePlayer];
		}
		return new List<Property>();
	}

	// BuyHouse()
	// BuyHotel()
	// Tax()
	// GetToJail()
	// GetOutOfJail()
	// RandomChanceCard()
	// RandomCommunityChestCard()
	// IsWinner() bool bankrupt
	// EndGame()

	// public int MoneyPlayer()
	// {

	// }

	// public void RollDice(Player player)
	// {
	// 	List<int> rollResults = new List<int>();
	// 	foreach (var dice in _diceList)
	// 	{
	// 		int rollResult = dice.Roll();
	// 		rollResults.Add(rollResult);
	// 	}
	// 	DiceRolled?.Invoke(player, rollResults);
	// }
}