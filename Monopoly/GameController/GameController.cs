using Monopoly;
using System.Collections.Generic;

public class GameController
{
	private List<IPlayer> _players;
	private int currentPlayer;
	private List<IDice> _dices;
	private List<int> _resultDice;
	private Dictionary<IPlayer, Tile> _playerPos;
	private Dictionary<IPlayer, int> _playerMoney;
	private Dictionary<IPlayer, bool> _jailOrNot;
	private bool gameStatus;
	private Board _board;

	public GameController()
	{
		_board = new Board();
		_players = new List<IPlayer>();
		_dices = new List<IDice>();
		_resultDice = new List<int>();
	}

	public void AddPlayer(string name)
	{
		Player player = new Player(name);
		_players.Add(player);
	}
	
	// public List<IPlayer> ActivePlayer()
	// {
		
	// }
	
	// public int CurrentPlayer()
	// {
		
	// }

	// public void RollDice()
	// {
		
	// }
}