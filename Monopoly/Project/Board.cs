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
			// CardFactory chanceCard = new CardFactory(TypeCard.CHANCE);
			// CardFactory communityCard = new CardFactory(TypeCard.COMMUNITY_CHEST);
			
			List<Tile> tile = new List<Tile>();
			tile.Add(new Go(1, "Go!", "Starting point of the board", 200));
			tile.Add(new Property(2, "Indonesia", "Welcome to Indonesia!", 150, 50, PropertyType.COUNTRY));
			tile.Add(new CommunityChest(3, "Community Chest", "Please take a community card"));
			tile.Add(new Property(4, "Malaysia", "Welcome to Malaysia!", 150, 50, PropertyType.COUNTRY));
			tile.Add(new Tax(5, "Tax 1", "You should pay the tax $100!", 100));
			tile.Add(new Property(6, "Changi Airport", "You just land on airport.", PropertyType.AIRPORT));
			tile.Add(new Property(7, "Singapore", "Welcome to Singapore!", 200, 100, PropertyType.COUNTRY));
			tile.Add(new Chance(8, "Chance", "Please take a chance card")); 
			tile.Add(new Property(9, "Hongkong", "Welcome to Hongkong!", 200, 100, PropertyType.COUNTRY));
			tile.Add(new Property(10, "Taiwan", "Welcome to Taiwan!", 200, 100, PropertyType.COUNTRY));
			tile.Add(new VisitingJail(11, "Visiting Jail", "You just visiting the jail."));
			tile.Add(new Property(12, "Philipina", "Welcome to Philipina!", 200, 100, PropertyType.COUNTRY));
			tile.Add(new Utility(13, "Electric Company", ""));
			tile.Add(new Property(14, "Thailand", "Welcome to Thailand!", 200, 70, PropertyType.COUNTRY));
			tile.Add(new Property(15, "Vietnam", "Welcome to Vietnam!", 200, 70, PropertyType.COUNTRY));
			tile.Add(new Property(16, "Tokyo Station", "You just arrived at the station.", 100, 50, PropertyType.STATION));
			tile.Add(new Property(17, "Japan", "Welcome to Japan!", 250, 100, PropertyType.COUNTRY));
			tile.Add(new CommunityChest(18, "Community Chest", "Please take a community card"));
			tile.Add(new Property(19, "Korea", "Welcome to Korea!", 250, 100, PropertyType.COUNTRY));
			tile.Add(new Property(20, "India", "Welcome to India!", 100, 25, PropertyType.COUNTRY));
			tile.Add(new FreeParking(21, "Free Parking", ""));
			tile.Add(new Property(22, "China", "Welcome to China!", 250, 100, PropertyType.COUNTRY));
			tile.Add(new Chance(23, "Chance", "Please take a chance card")); 
			tile.Add(new Property(24, "Uni Soviet", "Welcome to Uni Soviet!", 300, 120, PropertyType.COUNTRY));
			tile.Add(new Property(25, "Italia", "Welcome to Italia!", 300, 120, PropertyType.COUNTRY));
			tile.Add(new Property(26, "London Station", "You just arrived at the station.", 100, 50, PropertyType.STATION));
			tile.Add(new Property(27, "England", "Welcome to England!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new Property(28, "France", "Welcome to France!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new Utility(29, "Water Works", ""));
			tile.Add(new Property(30, "Netherlands", "Welcome to Netherlands!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new GoToJail(31, "Go To Jail", ""));
			tile.Add(new Property(32, "Canada", "Welcome to Canada!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new Property(33, "USA", "Welcome to USA!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new CommunityChest(34, "Community Chest", "Please take a community card"));
			tile.Add(new Property(35, "Brazil", "Welcome to Brazil!", 300, 125, PropertyType.COUNTRY));
			tile.Add(new Property(36, "Sydney Harbour", "You just arrived at the harbour.", 100, 50, PropertyType.HARBOUR));
			tile.Add(new Chance(37, "Chance", "Please take a chance card")); 
			tile.Add(new Property(38, "Australia", "Welcome to Australia!", 200, 100, PropertyType.COUNTRY));
			tile.Add(new Tax(39, "Tax 2", "You should pay the tax $200!", 200));
			tile.Add(new Property(40, "Africa", "Welcome to Africa!", 150, 60, PropertyType.COUNTRY));
			
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
