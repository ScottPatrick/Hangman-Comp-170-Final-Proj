namespace Sophmores_FinalProj
{
  public class HealthPotion : Item
  {
    #region Public Properties

    /// <summary>
    /// Health Potions restore health and are consumable 
    /// </summary>
    public int Potency { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Creates a custom Health Potion which restores currentHP And are
    /// consumable by default
    /// </summary>
    /// <param name="name"> Name </param>
    /// <param name="description"> Potion Description </param>
    /// <param name="super">
    /// if true, creates super poison. If false, creates basic poison.
    /// </param>
    public HealthPotion(string name, string description, bool super)
      : base(name, "Potion", description, true)
    {
      if (!super)
      {
        this.description = description + "\n effect: restores 20 HP";
        Potency = 20;
      }
      else if (super)
      {
        this.description = description + "\n effect: restores all HP";
        Potency = 100;
      }
      playerCanEquip = false;
    }

    /// Creates a custom Health Potion which restores currentHP and are
    /// consumable </summary> <param name="name"> Name </param> <param
    /// name="description"> Potion Description </param> <param
    /// name="potency"> Amount of HP to restore </param>
    public HealthPotion(string name, string description, int potency)
      : base(name, "Potion", description, true)
    {
      Potency = potency;
      playerCanEquip = false;
    }

    public HealthPotion(HealthPotion potion)
      : base(potion.name, potion.type, potion.description, true)
    {
      Potency = potion.Potency;
      playerCanEquip = false;
    }

    /// <summary>
    /// Creates a Basic Health Potion that restores 20HP 
    /// </summary>
    public HealthPotion()
    {
      name = "Basic Health Potion";
      type = "Potion";
      description = "Restores 20 HP";
      playerCanEquip = false;
      consumable = true;
      Potency = 20;
    }

    #endregion Public Constructors
  }
}
