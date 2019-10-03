using System.Collections.Generic;

public interface IContainer
{
	List<IItemSelection> GetItems();
	void Refresh();
}