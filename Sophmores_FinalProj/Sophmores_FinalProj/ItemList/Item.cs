using System;

namespace Sophmores_FinalProj
{
  public class Item : IComparable
  {
    #region Public Properties

    public bool consumable { get; protected set; }

    public string description { get; set; }

    /// <summary>
    /// Items are as flexible as you want them to be By default not consumable 
    /// </summary>
    public string name { get; set; }

    public bool playerCanEquip { get; protected set; }

    public string type { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Creates a new Item, can be Consumable 
    /// </summary>
    /// <param name="Name"> Item Name </param>
    /// <param name="Type"> Type of Item as a string </param>
    /// <param name="Description"> Item Flavor Text </param>
    /// <param name="Consumable"> Is this item destroyed when used? </param>
    public Item(string Name, string Type, string Description, bool Consumable)
    {
      name = Name;
      type = Type;
      description = Description;
      consumable = Consumable;
    }

    /// <summary>
    /// Creates a new Non-Consumable Item 
    /// </summary>
    /// <param name="Name"> Item Name </param>
    /// <param name="Type"> Item Type as a string </param>
    /// <param name="Description"> Item Description </param>
    public Item(string Name, string Type, string Description)
    {
      name = Name;
      type = Type;
      description = Description;
      consumable = false;
    }

    #endregion Public Constructors

    #region Protected Constructors

    /// <summary>
    /// If you create an Item using this on purpose, you're wrong Please DO
    /// NOT Create Items using this Enables functionality of Child classes
    /// </summary>
    protected Item()
    {
    }

    #endregion Protected Constructors

    #region Public Methods

    /// <summary>
    /// Compares Item Names 
    /// </summary>
    /// <param name="obj"> Item to Compare </param>
    /// <returns></returns>
    public int CompareTo(object obj)
    {
      if (obj == null)
      {
        return 1;
      }
      Item otherItem = obj as Item;
      if (otherItem != null)
      {
        return this.name.CompareTo(otherItem.name);
      }
      else
        throw new ArgumentException("Object is not an Item");
    }

    /// <summary>
    /// Returns Item name as a string 
    /// </summary>
    /// <returns> Returns Item name as a string </returns>
    public override string ToString()
    {
      return this.name;
    }

    #endregion Public Methods
  }
}
