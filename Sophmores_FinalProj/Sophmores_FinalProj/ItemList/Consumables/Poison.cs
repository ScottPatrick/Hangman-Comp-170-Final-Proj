using System;

namespace Sophmores_FinalProj
{
  public class Poison : Item
  {
    #region Public Properties

    /// <summary>
    /// Health Potions restore health and are consumable 
    /// </summary>
    public double Potency { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Creates a custom Health Potion which restores currentHP and are consumable 
    /// </summary>
    /// <param name="name"> Name </param>
    /// <param name="description"> Poison Description </param>
    /// <param name="super">
    /// If true, creates a super poison. If false, creates a basic poison.
    /// </param>
    public Poison(string name, string description, bool super)
      : base(name, "Poison", description, true)
    {
      if (!super)
      {
        description = description + "\n effect: player attacks slightly more effective on enemy for three turns!";
        Potency = 3;
      }
      if (super)
      {
        description = description + "\n effect: player attacks twice as effective on enemy for three turns!";
        Potency = 2;
      }
      playerCanEquip = false;
    }

    /// <summary>
    /// creates a basic health potion. 
    /// </summary>
    public Poison()
    {
      name = "Basic Poison";
      type = "Poison";
      description = "Basic Poison that makes player's attacks slightly more effective on enemy for three turns.";
      playerCanEquip = false;
      consumable = true;
      Potency = 1.25;
    }

    #endregion Public Constructors

    #region Public Methods

    /// <summary>
    /// retrieves potency of poison. 
    /// </summary>
    public double getPotency(Poison poison)
    {
      return poison.Potency;
    }

    #endregion Public Methods
  }
}
