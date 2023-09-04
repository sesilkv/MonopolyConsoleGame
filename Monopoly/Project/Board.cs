using MonopolyEnum;
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
			List<Tile> tile = new List<Tile>();
			tile.Add(new GoTile(1, "Go!", "Starting point of the board", TileType.GO));
			tile.Add(new LandmarkTile(2, "Indonesia", "", TileType.LANDMARK));
			tile.Add(new CommunityTile(3, "Community Chest", "Please take the Community Card!", TileType.GO));
		
	// 	public LandmarkTile(string name, int location, string description, bool hasProperty, int initialPrice, int rent, int houseTotal, int hotelTotal, int housePrice, int hotelPrice, int maxHouse, int maxHotel)
	// {
	// 	this._type = TileType.LANDMARK;
	// 	this._name = name;
	// 	this._location = location;
	// 	this._description = description;
	// 	this._hasProperty = hasProperty;
	// 	this._initialPrice = initialPrice;
	// 	this._housePrice = housePrice;
	// 	this._hotelPrice = hotelPrice;
	// 	this._rent = rent;
	// 	this._houseTotal = houseTotal;
	// 	this._hotelTotal = hotelTotal;
	// 	this._maxHouse = maxHouse;
	// 	this._maxHotel = maxHotel;
	// 	this._owner = null;
	// }
		}
	}
}
