using Monopoly;
using System.Collections.Generic;

namespace Monopoly
{
	public class Board
	{
		private List<Tile> _tile = new List<Tile>();

		public Board()
		{
			CreateBoard();
		}
		
		public List<Tile> Tile { get => _tile; set => _tile = value; }

		public void CreateBoard()
		{			
			_tile = new List<Tile>
			{
				new Go(0, "Go!", "Starting point of the board", 200),
				new Property(1, "Indonesia", "Welcome to Indonesia", 200, 100, 60, 50, PropertyType.Country),
				new CommunityChest(2, "Community Chest"),
				new Property(3, "Malaysia", "Welcome to Malaysia", 200, 100, 60, 50, PropertyType.Country),
				new Jail(4, "In Jail", "Sorry, you must go to jail", 100),
				new Property(5, "Changi Airport", "You just land on airport", PropertyType.Airport),
				new CommunityChest(6, "Community Chest"),
				new Chance(7, "Chance"),
				new Property(8, "Hogwarts", "Welcome to Hogwarts", 150, 50, 60, 50, PropertyType.Country),
				new Property(9, "Taiwan", "Welcome to Taiwan", 200, 100, 60, 50, PropertyType.Country),
				new Chance(10, "Chance"),
				new Property(11, "Philipina", "Welcome to Philipina", 200, 100, 60, 50, PropertyType.Country),
				new Property(12, "New Zealand", "Welcome to New Zealand", 300, 100, 60, 50, PropertyType.Country),
				new Property(13, "Thailand", "Welcome to Thailand", 200, 100, 60, 50, PropertyType.Country),
				new Jail(14, "In Jail", "Sorry, you must go to jail", 100),
				new Property(15, "Tokyo Station", "You just arrived at the station", PropertyType.Station),
				new Property(16, "Japan", "Welcome to Japan", 300, 100, 60, 50, PropertyType.Country),
				new CommunityChest(17, "Community Chest"),
				new Property(18, "Korea", "Welcome to Korea", 300, 100, 60, 50, PropertyType.Country),
				new Jail(19, "In Jail", "Sorry, you must go to jail", 100),
				new Property(20, "North Korea", "Welcome to North Korea", 300, 100, 60, 50, PropertyType.Country),
				new Property(21, "China", "Welcome to China", 250, 100, 60, 50, PropertyType.Country),
				new Chance(22, "Chance"),
				new Property(23, "Uni Soviet", "Welcome to Uni Soviet", 300, 120, 60, 50, PropertyType.Country),
				new CommunityChest(24, "Community Chest"),
				new Chance(25, "Chance"),
				new Property(26, "England", "Welcome to England", 300, 125, 60, 50, PropertyType.Country),
				new Property(27, "France", "Welcome to France", 300, 125, 60, 50, PropertyType.Country),
				new Utility(28, "Water Works", "Thank you for the payment", 100),
				new Property(29, "Netherlands", "Welcome to Netherlands", 300, 125, 60, 60, PropertyType.Country),
				new Jail(30, "Go To Jail", "Sorry, you must go to jail", 100),
				new Property(31, "Canada", "Welcome to Canada", 300, 125, 60, 50, PropertyType.Country),
				new Chance(32, "Chance"),
				new CommunityChest(33, "Community Chest"),
				new Property(34, "Brazil", "Welcome to Brazil", 300, 125, 60, 25, PropertyType.Country),
				new Property(35, "Sydney Harbour", "You just arrived at the harbour", PropertyType.Harbour),
				new Chance(36, "Chance"),
				new Property(37, "Australia", "Welcome to Australia", 200, 100, 60, 50, PropertyType.Country),
				new Tax(38, "Tax", "Thank you for the payment", 200),
				new Chance(39, "Chance")
			};
			
		}
		
		public void AddTile(Tile tile)
		{
			_tile.Add(tile);
		}
		
		public Tile GetTile(int position)
		{
			foreach (Tile tile in _tile)
			{
				if(tile.Position == position)
				{
					return tile;
				}
			}
			return null;
		}
		
		public int GetTileAll()
		{
			return _tile.Count;
		}

		internal Tile GetTile(object position)
		{
			throw new NotImplementedException();
		}
	}
}
