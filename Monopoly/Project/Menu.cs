namespace Monopoly;

public delegate void MenuAction();
public class Menu
{
	private string? _nameMenu;
	private MenuAction _action;
	
	public string? NameMenu { get => _nameMenu; set => _nameMenu = value; }
	public MenuAction Action { get => _action; set => _action = value; }
}