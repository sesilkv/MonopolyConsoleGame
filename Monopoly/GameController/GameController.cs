using Monopoly;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NLog;

public class GameController
{
	private static readonly Logger log = LogManager.GetCurrentClassLogger();
	private GameState _gameState;
	private Board _board;
	private List<IPlayer> _players;
	private int _currPlayer;
	private Dictionary<IPlayer, Tile> _playerPos;
	private Dictionary<IPlayer, int> _playerCash;
	private Dictionary<IPlayer, List<Property>> _playerProp;
	private Dictionary<IPlayer, bool> _jailOrNot;
	private Dictionary<IPlayer, int> _jailTurn;
	// private List<Card> _chanceCards = new List<Card>();
	// private List<Card> _communityChestCards = new List<Card>();
	private List<IDice> _diceList;
	private List<int> _totalDice;
	public event Action<IPlayer> PlayerNotifiedJail;
	
	// notify tax, utility, amount start, winner
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
		_jailTurn = new Dictionary<IPlayer, int>();
		_playerProp = new Dictionary<IPlayer, List<Property>>();
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

	public void AddPlayer(string name)
	{
		Player player = new Player(name);
		_players.Add(player);
		log.Info($"Player {name} added to game.");
		_playerPos[player] = _board.GetTile(0);
		_playerCash[player] = 1000;
		_jailOrNot[player] = false;
		_jailTurn[player] = 0;
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
	
	public List<IPlayer> GetPlayers()
	{
		return _players;
	}

	public IPlayer GetPlayerIndex(int index)
	{
		if (index >= 0 && index < _players.Count)
		{
			return _players[index];
		}
		return null; // Return null if the index is out of bounds
	}

	public Dictionary<IPlayer, int> GetPlayerCash()
	{
		return _playerCash;
	}

	public int CurrentPlayer()
	{
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
		log.Info("Dice is rolling.");
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
			if(newPos < currPos)
			{
				Go go = _board.Tile[0] as Go;
				int amountAdd = go.AmountStart; // call the amountstart
				
				if(_playerCash.ContainsKey(activePlayer))
				{
					_playerCash[activePlayer] += amountAdd;
				}
				log.Info($"{activePlayer.GetName()} has crossed the first tile.");
				NotifyPlayer("You have got the amount start $200", activePlayer.GetName());
			}
			
			TileAction(newTile); //buy, rent, tax, utility, jail, chance, community
		}
	}
	
	public Tile GetTile()
	{
		int currPos = GetPlayerPosition();
		Tile currTile = _board.GetTile(currPos);
		return currTile;
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
		log.Info($"{activePlayer.GetName()}'s position is {tile.TileName}.");
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
	
	// overload TileName parameter inputan user
	public string TileName(string playerName)
	{
		IPlayer _player = GetPlayerByName(playerName);
		if (_player != null)
		{
			int _playerPos = GetPlayerPosition(_player);
			if (_playerPos >= 0)
			{
				Tile tile = _board.GetTile(_playerPos);
				return tile.TileName;
			}
		}
		return null;
	}

	public IPlayer GetPlayerByName(string playerName)
	{
		foreach (var _player in _players)
		{
			if (_player.GetName() == playerName)
			{
				return _player;
			}
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
			case Jail:
				MoveToJail();
				break;
			case Property property:
				PayRent(property);
				break;
			case Chance:
				GenerateCardChance();
				break;
			case CommunityChest:
				GenerateCardCommunityChest();
				break;
			default:
				break;
		}
	}
	
	public bool IsNotChanceOrCommunityChest(Tile tile)
	{
		return !(tile is Chance) && !(tile is CommunityChest);
	}

	public void MoveToJail()
	{
		IPlayer activePlayer = ActivePlayer();
		Jail jailTile = GetJailTile();

		if (activePlayer != null && jailTile != null)
		{
			SetPlayerPosition(jailTile);
			_jailOrNot[activePlayer] = true;
			_jailTurn[activePlayer] = 0;
			PlayerNotifiedJail?.Invoke(activePlayer);
			log.Info($"{activePlayer.GetName()} move to jail.");
		}
	}

	public List<IPlayer> JailedPlayer()
	{
		List<IPlayer> jailedPlayer = new List<IPlayer>();
		foreach(var player in _jailOrNot.Keys)
		{
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
				_jailOrNot[activePlayer] = false; // the player is no longer in jail
				log.Info($"{activePlayer.GetName()} can get out from jail by double dice.");
				return true; // the player successfully got out of jail
			}
			log.Info($"{activePlayer.GetName()} can't get out from jail by double dice.");
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
					_playerCash[activePlayer] -= 100;
					_jailOrNot[activePlayer] = false;
					log.Info($"{activePlayer.GetName()} paid to get out from jail.");
					NotifyPlayer("Congrats, you can get out from jail", activePlayer.GetName());
					return true;
				}
				_playerCash[activePlayer] -= 100;
				log.Info($"{activePlayer.GetName()} paid to get out from jail and bankrupt at the same time.");
				NotifyPlayer("You can get out from jail, but it seems you are bankrupt", activePlayer.GetName());
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
				log.Info($"{activePlayer.GetName()} paid tax.");
				NotifyPlayer("You have successfully paid the tax $100", activePlayer.GetName());
				return TaxUtilityState.Success;
			} 
			else 
			{
				_playerCash[activePlayer] -= taxAmount;
				log.Info($"{activePlayer.GetName()} paid tax and bankrupt.");
				NotifyPlayer("Actually, you don't have enough money to pay the tax $100.\nIt seems you are bankrupt", activePlayer.GetName());
			}
		}
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
				log.Info($"{activePlayer.GetName()} paid utility.");
				NotifyPlayer("You have successfully paid the utility $100", activePlayer.GetName());
				return TaxUtilityState.Success;
			}
			else 
			{
				_playerCash[activePlayer] -= utilityAmount;
				log.Info($"{activePlayer.GetName()} paid utility and bankrupt.");
				NotifyPlayer("Actually, you don't have enough money to pay the utility $100. \nIt seems you are bankrupt", activePlayer.GetName());
			}
		}
		return TaxUtilityState.NotEnoughMoney;
	}
	
	public void PayRent(Property prop)
	{
		IPlayer activePlayer = ActivePlayer();
		string propOwner = prop.Owner;
		string currPlayer = activePlayer.GetName();
		
		if (propOwner != null && propOwner != currPlayer)
		{
			int rent = prop.RentProp + (prop.HousePrice * prop.NumberOfHouse) + (prop.HotelPrice * prop.NumberOfHotel);
			if (_playerCash.ContainsKey(activePlayer))
			{
				_playerCash[activePlayer] -= rent;
				NotifyPlayer($"\nThank you for paid the rent ${rent} on {TileName()}", currPlayer);
			}
			
			IPlayer owner = GetPlayerByName(propOwner);
			if(_playerCash.ContainsKey(owner))
			{
				_playerCash[owner] += rent;
				NotifyPlayer($"\nYou got rent income ${rent} from {currPlayer}", propOwner);
			}
			log.Info($"{currPlayer} paid the rent to {propOwner}.");
		} 
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

			if (!(currTile is Property prop))
			{
				return PropState.NotProperty;
			}
			
			if(prop.Owner != null)
			{
				return PropState.AlreadyOwned;
			}
			
			if (prop.PropType != PropertyType.Country)
			{
				return PropState.NotProperty;
			}

			int propertyPrice = prop.BuyProp;

			if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= propertyPrice)
			{
				// // Clear previous ownership and property improvements
				// if (prop.Owner != null)
				// {
				// 	IPlayer previousOwner = _players.FirstOrDefault(player => player.GetName() == prop.Owner);
				// 	if (previousOwner != null)
				// 	{
				// 		_playerProp[previousOwner].Remove(prop);
				// 	}
				// 	prop.SetOwner(null);
				// 	prop.RemoveHouseHotel();
				// }

				_playerCash[activePlayer] -= propertyPrice;
				_playerPos[activePlayer] = prop;
				prop.SetOwner(activePlayer.GetName());
				if (!_playerProp.ContainsKey(activePlayer))
				{
					_playerProp[activePlayer] = new List<Property>();
				}
				_playerProp[activePlayer].Add(prop);
				log.Info($"{prop.TileName} property was bought by {activePlayer.GetName()}.");
			}
			else
			{
				log.Warn($"{activePlayer.GetName()} doesn't have enough money to buy {prop.TileName} property.");
				return PropState.NotEnoughMoney;
			}
		}
		return PropState.Success;
	}
	
	public bool SellProperty() //also the house and hotel
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
				int propertyPrice = prop.BuyProp;
				int housePrice = prop.HousePrice * prop.NumberOfHouse;
				int hotelPrice = prop.HotelPrice * prop.NumberOfHotel;

				if (_playerCash.ContainsKey(activePlayer))
				{
					_playerCash[activePlayer] += propertyPrice + housePrice + hotelPrice;
					props.Remove(prop);
					prop.SetOwner(null);
				}
				log.Info($"{activePlayer.GetName()} sells {prop.TileName} property.");
				return true;
			}
			else
			{
				return false;
			}
		}
		return false;
	}

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
			log.Info($"{activePlayer.GetName()} buys {prop.NumberOfHouse} house on {prop.TileName} property.");
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public bool BuyHotel()
	{
		IPlayer activePlayer = ActivePlayer();
		int currPos = GetPlayerPosition();
		Tile currTile = _board.GetTile(currPos);
		Property prop = currTile as Property;
		
		int hotelPrice = prop.HotelPrice;
		int maxHotel = 2;
		
		if (prop.NumberOfHotel >= maxHotel)
		{
			return false;
		}
		
		if (_playerCash.ContainsKey(activePlayer) && _playerCash[activePlayer] >= hotelPrice)
		{
			_playerCash[activePlayer] -= hotelPrice;
			prop.AddHotel();
			log.Info($"{activePlayer.GetName()} buys {prop.NumberOfHotel} hotel on {prop.TileName} property.");
			return true;
		}
		else
		{
			return false;
		}
	}
	
	public int GetNumberOfHouses()
	{
		int currPos = GetPlayerPosition();
		Tile currTile = _board.GetTile(currPos);
		if (currTile is Property prop)
		{
			return prop.NumberOfHouse;
		}
		else
		{
			return 0;
		}
	}
	
	public void GenerateCardChance()
	{
		
		Card chanceCard = new Card("Chance Card 1", "Happy birthday!", CardType.Chance);
		// _chanceCards.Add(new Card("Collect Money", "Collect $100!", CardType.Chance));
		
		CardType type = chanceCard.CardType;
		ActionCardDelegate skillBirthday = CardSkill.ChanceBirthday;
		
		chanceCard.ExecuteActionCard(skillBirthday, this);
	}
	
	// public void DrawChanceCard()
	// {
	// 	Random random = new Random();
	// 	int chanceCardIndex = random.Next(_chanceCards.Count);
	// 	Card chanceCard = _chanceCards[chanceCardIndex];
	// 	IPlayer currPlayer = ActivePlayer();
	// 	chanceCard.ExecuteActionCard(this);
	// }
	
	public void GenerateCardCommunityChest()
	{
		Card communityChestCard = new Card("Community Chest Card 1", "Pay Tax!", CardType.CommunityChest);

		CardType type = communityChestCard.CardType;
		// ActionCardDelegate action;
		ActionCardDelegate skillPayTax = CardSkill.CommunityPayTax;
		// _skillCards.Add(type, skillPayTax);
		communityChestCard.ExecuteActionCard(skillPayTax, this);
	}

	// public Card DrawCommunityChestCard()
	// {
	// 	Random random = new Random();
	// 	int index = random.Next(_communityChestCards.Count);
	// 	return _communityChestCards[index];
	// }
}