using MonopolyEnum;

namespace Monopoly
{
	public abstract class Tile
	{
		private int _position;
		private string? _tileName;
		private string? _tileDescription;
		private TileType _tileType;

		public Tile(int position, string? tileName, string? tileDescription, TileType tileType)
		{
			_position = position;
			_tileName = tileName;
			_tileDescription = tileDescription;
			_tileType = tileType;
		}

		public Tile()
		{

		}

		public int Position { get => _position; set => _position = value; }
		public string? TileName { get => _tileName; set => _tileName = value; }
		public string? TileDescription { get => _tileDescription; set => _tileDescription = value; }
		public TileType TileType { get => _tileType; set => _tileType = value; }
	}

	// CORNER TILE
	public class GoTile : Tile
	{
		public GoTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// CORNER TILE
	public class VisitingJailTile : Tile
	{
		public VisitingJailTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// CORNER TILE
	public class FreeParkingTile : Tile
	{
		public FreeParkingTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// CORNER TILE
	public class GoToJailTile : Tile
	{
		public GoToJailTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// CHANCE
	public class ChanceTile : Tile
	{
		public ChanceTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// COMMUNITY CHEST
	public class CommunityTile : Tile
	{
		public CommunityTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}

	// LANDMARK
	public class LandmarkTile : Tile
	{
		public LandmarkTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}
	
	// PROPERTY: station, airport, harbour
	public class PropertyTile : Tile 
	{
		public PropertyTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}
	
	// UTILITY: electric company & water works
	public class UtilityTile : Tile 
	{
		public UtilityTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}
	
	// TAX
	public class TaxTile : Tile 
	{
		public TaxTile(int position, string? tileName, string? tileDescription, TileType tileType) : base(position, tileName, tileDescription, tileType)
		{

		}
	}
}