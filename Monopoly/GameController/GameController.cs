using Monopoly;
using System.Collections.Generic;

public class GameController 
{
	private List<IPlayer> _players;
	private Board _board;
	
	public GameController()
	{
		_board = new Board();
		_players = new List<IPlayer>();
	}
	
	public void AddPlayer(IPlayer player)
	{
		_players.Add(player);
	}
	
	public void RollDice()
	{
		
	}
}