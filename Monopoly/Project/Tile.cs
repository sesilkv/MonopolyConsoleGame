using Monopoly;

namespace Monopoly
{
	public abstract class Tile
	{
		protected int _position;
		protected string? _tileName;
		protected string? _tileDescription;
		protected TileType _tileType;
		
		public int Position { get => _position; set => _position = value; }
		public string? TileName { get => _tileName; set => _tileName = value; }
		public string? TileDescription { get => _tileDescription; set => _tileDescription = value; }
		public TileType TileType { get => _tileType; set => _tileType = value; }

		public Tile(int position, string? tileName, string? tileDescription, TileType tileType)
		{
			_position = position;
			_tileName = tileName;
			_tileDescription = tileDescription;
			_tileType = tileType;
		}
		
		public Tile(int position, string? tileName)
		{
			_position = position;
			_tileName = tileName;
		}
		
		public Tile(int position)
		{
			_position = position;
		}
		
		public Tile() 
		{
			
		}
	}
}