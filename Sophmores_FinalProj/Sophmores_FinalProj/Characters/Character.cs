using Sophmores_FinalProj.Utilities;
using System;

namespace Sophmores_FinalProj
{
  public class Character
  {
    #region Public Properties

    public double BuffMultiplier { get; set; }

    public int CurrentHP { get; set; }

    public Inventory inventory { get; set; }

    /// <summary>
    /// Doesn't do anything...yet 
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Base Character Class for all types of Characters Come with
    /// Inventories and supporting methods Can die, and even Emote!
    /// </summary>
    public string Name { get; set; }

    public int TotalHP { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Creates an Non-Player, Non-Combat Character 
    /// </summary>
    /// <param name="Name"> Character Name </param>
    /// <param name="Health"> Character Health </param>
    /// <param name="Level"> Character Level </param>
    public Character(string Name, int Health, int Level)
    {
      this.Name = Name;
      TotalHP = Health;
      CurrentHP = Health;
      this.Level = Level;
      inventory = new Inventory();
    }

    /// <summary>
    /// Creates an Nameable Default Character Similar to Peasants! 
    /// </summary>
    /// <param name="Name"> Character Name </param>
    public Character(string Name)
    {
      this.Name = Name;
      TotalHP = 10;
      CurrentHP = TotalHP;
      Level = 1;
      inventory = new Inventory();
    }

    /// <summary>
    /// Default Characters are peasants! 
    /// </summary>
    public Character()
    {
      Name = "Peasant";
      TotalHP = 10;
      CurrentHP = TotalHP;
      Level = 1;
      inventory = new Inventory();
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// Add Item to inventory 
    /// </summary>
    /// <param name="item"> item </param>
    /// <param name="itemCount"> Number of the SAME item to add </param>
    public void AddToInventory(Item item, int itemCount)
    {
      inventory.Add(item, itemCount);
    }

    /// <summary>
    /// Applies Specified value to Character BuffMultiplier 
    /// </summary>
    /// <param name="buff"> Value of new BuffMultiplier </param>
    public void ApplyBuffOrDebuff(double buff)
    {
      BuffMultiplier = buff;
    }

    /// <summary>
    /// For use when a character says something 
    /// </summary>
    /// <param name="says"> Thing the character says </param>
    public void Emote(string says)
    {
      Console.WriteLine(this.Name + " says: {0}", says);
    }

    /// <summary>
    /// Check to see if a Character is Alive 
    /// </summary>
    /// <returns> True is character is alive, false otherwise </returns>
    public bool isAlive()
    {
      if (CurrentHP > 0)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Use this Function to modify Character Health in-combat 
    /// </summary>
    /// <param name="delta"> Amount to increase or decrease CurrentHP </param>
    public void ModifyCurrentHP(int delta)
    {
      CurrentHP += delta;
      if (CurrentHP < 0)
      {
        CurrentHP = 0;
        // Call Death Method? (Not yet implemented) 
      }
      else if (CurrentHP > 100)
      {
        CurrentHP = 100;
      }
    }

    /// <summary>
    /// Resets BuffMultiplier to 1 
    /// </summary>
    public void RemoveBuff()
    {
      BuffMultiplier = 1;
    }

    /// <summary>
    /// Remove an Item from the character's Inventory 
    /// </summary>
    /// <param name="item"> Item to Remove </param>
    /// <param name="itemCount"> Number of Items to Remove </param>
    public void RemoveFromInventory(Item item, int itemCount)
    {
      inventory.Remove(item, itemCount);
    }

    /// <summary>
    /// Returns Character Name as String 
    /// </summary>
    /// <returns> Character Name </returns>
    public override string ToString()
    {
      return this.Name;
    }

    public void UseHealthPotion(HealthPotion pot)
    {
      ModifyCurrentHP(pot.Potency);
      RemoveFromInventory(pot, 1);
    }

    /// <summary>
    /// Uses the Specified Poison 
    /// </summary>

    public void UsePoison(Poison poi)
    {
      ApplyBuffOrDebuff(poi.Potency);
      RemoveFromInventory(poi, 1);
    }

    #endregion Public Methods
  }
}
