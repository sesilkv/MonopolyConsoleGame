using Monopoly;

namespace Monopoly
{
	public abstract class Tile
	{
		private int _position;
		private string? _tileName;
		private string? _tileDescription;
		private TileType _tileType;
		
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
		
		public Tile(int position)
		{
			_position = position;
		}
		
		public Tile() 
		{
			
		}

		// public abstract Tile GetTile(int position);
	}
}