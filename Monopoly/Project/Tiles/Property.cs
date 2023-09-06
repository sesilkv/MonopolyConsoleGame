namespace Monopoly;

public class Property : Tile
{
	private PropertyType _propType;
	private int _priceProp;
	private int _rentProp;
	private string? _ownerProp;
	private PropertySituation _propSituation;
	public Property(int position, string? tileName, string? tileDescription, int priceProp, int rentProp, PropertyType propType) 
	{
		_tileType = TileType.PROPERTY;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_propType = propType;
		_priceProp = priceProp;
		_rentProp = rentProp;
		_ownerProp = null;
		_propSituation = PropertySituation.UNOWNED;
	}
	
	// for airport, safe space
	public Property(int position, string? tileName, string? tileDescription, PropertyType propType)
	{
		_tileType = TileType.PROPERTY;
		_tileName = tileName;
		_position = position;
		_tileDescription = tileDescription;
		_propType = propType;
	}

	public PropertyType PropType { get => _propType; set => _propType = value; }
	public int PriceProp { get => _priceProp; set => _priceProp = value; }
	public int RentProp 
	{
		get { return _rentProp; }
		set { _rentProp = value; }
	}
	public PropertySituation PropSituation { get => _propSituation; set => _propSituation = value; }
	
	public string GetOwner()
	{
		return _ownerProp;
	}
	
	public void SetOwner(string owner)
	{
		_ownerProp = owner;
		_propSituation = PropertySituation.OWNED;
	}
		
	
}