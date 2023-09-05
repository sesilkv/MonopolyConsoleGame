namespace Monopoly;

public class Property : Tile
{
	private PropertyType _propertyType;
	public Property(int position, string? tileName, string? tileDescription, TileType tileType, PropertyType propertyType) : base(position, tileName, tileDescription, tileType)
	{
		_propertyType = propertyType;
	}

	public PropertyType PropertyType { get => _propertyType; set => _propertyType = value; }
}