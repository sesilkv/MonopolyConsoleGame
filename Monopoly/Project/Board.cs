using Monopoly;
using System.Collections.Generic;

namespace Monopoly
{
	public class Board
	{
		private List<Tile> _tile;

		public Board()
		{
			CreateBoard();
		}
		
		public List<Tile> Tile { get => _tile; set => _tile = value; }

		public void CreateBoard()
		{
			CardFactory chanceCard = new CardFactory(TypeCard.CHANCE);
			CardFactory communityCard = new CardFactory(TypeCard.COMMUNITY_CHEST);
			
			List<Tile> tile = new List<Tile>();
			tile.Add(new Go(1, "Go!", "Starting point of the board", TileType.GO));
			tile.Add(new Property(2, "Indonesia", "Welcome to Indonesia!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(communityCard.GetTile(3));
			tile.Add(new Property(4, "Malaysia", "Welcome to Malaysia!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Tax(5, "Tax 1", "You should pay the tax $100!", TileType.TAX, 100));
			tile.Add(new Property(6, "Changi Airport", "", TileType.PROPERTY, PropertyType.AIRPORT));
			tile.Add(new Property(7, "Singapore", "Welcome to Singapore!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(chanceCard.GetTile(8)); 
			tile.Add(new Property(9, "Hongkong", "Welcome to Hongkong!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(10, "Taiwan", "Welcome to Taiwan!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new VisitingJail(11, "Visiting Jail", "You just visiting the jail.", TileType.VISITING_JAIL));
			tile.Add(new Property(12, "Philipina", "Welcome to Philipina!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Utility(13, "Electric Company", "", TileType.UTILITY));
			tile.Add(new Property(14, "Thailand", "Welcome to Thailand!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(15, "Vietnam", "Welcome to Vietnam!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(16, "Tokyo Station", "", TileType.PROPERTY, PropertyType.STATION));
			tile.Add(new Property(17, "Japan", "Welcome to Japan!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(communityCard.GetTile(18));
			tile.Add(new Property(19, "Korea", "Welcome to Korea!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(20, "India", "Welcome to India!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new FreeParking(21, "Free Parking", "", TileType.FREE_PARKING));
			tile.Add(new Property(22, "China", "Welcome to China!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(chanceCard.GetTile(23));
			tile.Add(new Property(24, "Uni Soviet", "Welcome to Uni Soviet!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(25, "Italia", "Welcome to Italia!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(26, "London Station", "", TileType.PROPERTY, PropertyType.STATION));
			tile.Add(new Property(27, "England", "Welcome to England!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(28, "France", "Welcome to France!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Utility(29, "Water Works", "", TileType.UTILITY));
			tile.Add(new Property(30, "Netherlands", "Welcome to Netherlands!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new GoToJail(31, "Go To Jail", "", TileType.GO_TO_JAIL));
			tile.Add(new Property(32, "Canada", "Welcome to Canada!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(33, "USA", "Welcome to USA!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(communityCard.GetTile(34));
			tile.Add(new Property(35, "Brazil", "Welcome to Brazil!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Property(36, "Sydney Harbour", "", TileType.PROPERTY, PropertyType.HARBOUR));
			tile.Add(chanceCard.GetTile(37));
			tile.Add(new Property(38, "Australia", "Welcome to Australia!", TileType.PROPERTY, PropertyType.COUNTRY));
			tile.Add(new Tax(39, "Tax 2", "You should pay the tax $200!", TileType.TAX, 200));
			tile.Add(new Property(40, "Africa", "Welcome to Africa!", TileType.PROPERTY, PropertyType.COUNTRY));
			
		}
	}
}
