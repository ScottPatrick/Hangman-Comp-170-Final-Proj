using System;

namespace Sophmores_FinalProj
{
  public class Enemy : Character
  {
    /// <summary>
    /// Basic enemy Class, have Max and Min Damage values For random Hit damage 
    /// </summary>

    #region Public Properties

    public string Affinity { get; set; }

    public int MaxDamage { get; set; }

    public int MinDamage { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Default Enemy with 'n' Affinity, Damage 5-10 
    /// </summary>
    public Enemy()
    {
      Name = "Plauged Rat";
      Affinity = "n";
      MinDamage = 5;
      MaxDamage = 10;
      BuffMultiplier = 1;
    }

    /// <summary>
    /// Default Enemy with 'n' Affinity, Damage 5-10 
    /// </summary>
    public Enemy(string Name)
      : base(Name)
    {
      this.Name = Name;
      Affinity = "n";
      MinDamage = 5;
      MaxDamage = 10;
      BuffMultiplier = 1;
    }

    /// <summary>
    /// Creates an enemy the player can do battle with Affinity set to "n" 
    /// </summary>
    /// <param name="Name"> Enemy's Name </param>
    /// <param name="Health"> Enemy's Health Value </param>
    /// <param name="Level"> Enemy's Level </param>
    /// <param name="minDamage"> The lowest damage an enemy can do </param>
    /// <param name="maxDamage"> The highest damage and enemy can do </param>
    public Enemy(string Name, int Health, int Level, int minDamage,
                 int maxDamage)
      : base(Name, Health, Level)
    {
      MinDamage = minDamage;
      MaxDamage = maxDamage;
      BuffMultiplier = 1;
      Affinity = "n";
    }

    /// <summary>
    /// Creates an enemy the player can do battle with 
    /// </summary>
    /// <param name="Name"> Enemy's Name </param>
    /// <param name="Health"> Enemy's Health Value </param>
    /// <param name="Level"> Enemy's Level </param>
    /// <param name="minDamage"> The lowest damage an enemy can do </param>
    /// <param name="maxDamage"> The highest damage and enemy can do </param>
    /// <param name="affinity"> Enemy's Affinity </param>
    public Enemy(string Name, int Health, int Level, int minDamage,
                 int maxDamage, string affinity)
      : base(Name, Health, Level)
    {
      MinDamage = minDamage;
      MaxDamage = maxDamage;
      BuffMultiplier = 1;
      Affinity = affinity;
    }

    /// <summary>
    /// Creates an enemy the player can do battle with 
    /// </summary>
    /// <param name="Name"> Enemy's Name </param>
    /// <param name="Health"> Enemy's Health Value </param>
    /// <param name="Level"> Enemy's Level </param>
    /// <param name="minDamage"> The lowest damage an enemy can do </param>
    /// <param name="maxDamage"> The highest damage and enemy can do </param>
    /// <param name="affinity"> Enemy's Affinity </param>
    /// <param name="buffMultiplier"> Amount to multiply damage By </param>
    public Enemy(string Name, int Health, int Level, int minDamage,
                 int maxDamage, string affinity, int buffMultiplier)
      : base(Name, Health, Level)
    {
      MinDamage = minDamage;
      MaxDamage = maxDamage;
      Affinity = affinity;
      BuffMultiplier = buffMultiplier;
    }

    #endregion Public Constructors
  }
}
