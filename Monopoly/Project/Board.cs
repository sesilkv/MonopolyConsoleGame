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
				new Property(1, "Indonesia", "Welcome to Indonesia", 150, 50, 25, PropertyType.Country),
				new CommunityChest(2, "Community Chest", "Please take a community card"),
				new Property(3, "Malaysia", "Welcome to Malaysia", 150, 50, 25, PropertyType.Country),
				// new Property(4, "Swiss", "Welcome to Swiss", 150, 50, 25, PropertyType.Country),
				new Tax(4, "Tax", "You paid the tax $100", 100),
				new Property(5, "Changi Airport", "You just land on airport", PropertyType.Airport),
				// new Property(6, "Bulgaria", "Welcome to Bulgaria", 150, 50, 25, PropertyType.Country),
				new Utility(6, "Electric Company", "You already paid the amount $50", 50),
				new Chance(7, "Chance", "Please take a chance card"),
				// new Property(8, "Hogwarts", "Welcome to Hogwarts", 150, 50, 25, PropertyType.Country),
				new Tax(8, "Tax", "You paid the tax $100", 100),
				new Property(9, "Taiwan", "Welcome to Taiwan", 200, 100, 25, PropertyType.Country),
				new VisitingJail(10, "Visiting Jail", "You just visiting the jail"),
				new Property(11, "Philipina", "Welcome to Philipina", 200, 100, 25, PropertyType.Country),
				
				// new Utility(12, "Electric Company", "You already paid the amount $50", 50),
				// new Property(13, "Thailand", "Welcome to Thailand", 200, 70, 25, PropertyType.Country),
				// new Tax(14, "Tax", "You paid the tax $100", 100),
				// new Property(15, "Tokyo Station", "You just arrived at the station", PropertyType.Station),
				// new Property(16, "Japan", "Welcome to Japan", 250, 100, 25, PropertyType.Country),
				// new CommunityChest(17, "Community Chest", "Please take a community card"),
				// new Property(18, "Korea", "Welcome to Korea", 250, 100, 25, PropertyType.Country),
				// new Utility(19, "Electric Company", "You already paid the amount $50", 50),
				// new FreeParking(20, "Free Parking", "You can choose any place"),
				// new Property(21, "China", "Welcome to China", 250, 100, 25, PropertyType.Country),
				// new Chance(22, "Chance", "Please take a chance card"),
				// new Property(23, "Uni Soviet", "Welcome to Uni Soviet", 300, 120, 25, PropertyType.Country),
				// new Property(24, "Italia", "Welcome to Italia", 300, 120, 25, PropertyType.Country),
				// new Tax(25, "Tax", "You paid the tax $100", 100),
				// new Property(26, "England", "Welcome to England", 300, 125, 25, PropertyType.Country),
				// new Property(27, "France", "Welcome to France", 300, 125, 25, PropertyType.Country),
				// new Utility(28, "Water Works", "Pay the amount $50", 50),
				// new Property(29, "Netherlands", "Welcome to Netherlands", 300, 125, 25, PropertyType.Country),
				// new Jail(30, "Go To Jail", "Sorry, you must go to jail", 100),
				// new Property(31, "Canada", "Welcome to Canada", 300, 125, 25, PropertyType.Country),
				// new Property(32, "USA", "Welcome to USA", 300, 125, 25, PropertyType.Country),
				// new CommunityChest(33, "Community Chest", "Please take a community card"),
				// new Property(34, "Brazil", "Welcome to Brazil", 300, 125, 25, PropertyType.Country),
				// new Property(35, "Sydney Harbour", "You just arrived at the harbour", PropertyType.Harbour),
				// new Chance(36, "Chance", "Please take a chance card"),
				// new Property(37, "Australia", "Welcome to Australia", 200, 100, 25, PropertyType.Country),
				// new Tax(38, "Tax", "You paid the tax $200", 200),
				// new Property(39, "Africa", "Welcome to Africa", 150, 60, 25, PropertyType.Country)
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
	}
}
