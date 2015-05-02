using System;

namespace Sophmores_FinalProj
{
  /// <summary>
  /// Basic enemy Class, have Max and Min Damage values For random Hit damage 
  /// </summary>
  public class Enemy : Character
  {

    #region Public Properties
    /// <summary>
    /// Enemies are strong against non-matching affinities
    /// </summary>
    public string Affinity { get; set; }
    /// <summary>
    /// Max Damage an Enemy can do
    /// </summary>
    public int MaxDamage { get; set; }
    /// <summary>
    /// Minimum Damage an Enemy can do
    /// </summary>
    public int MinDamage { get; set; }
    /// <summary>
    /// Files that will be printed upon entering Combat
    /// </summary>
    public string[] ArtFiles { get; private set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Default Enemy with 'n' Affinity, Damage 5-10 
    /// </summary>
    public Enemy()
    {
      Name = "Plagued Rat";
      Affinity = "n";
      MinDamage = 5;
      MaxDamage = 10;
      BuffMultiplier = 1;
      ArtFiles = SetArtFiles();
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
      ArtFiles = SetArtFiles();
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
      ArtFiles = SetArtFiles();
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
      ArtFiles = SetArtFiles();
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
      ArtFiles = SetArtFiles();
    }

    #endregion Public Constructors

    #region Private Methods
    private string[] SetArtFiles()
    {
      ArtFiles = new string[3];
      for (int i = 0; i < ArtFiles.Length; i++)
      {
        ArtFiles[i] = (this.Name + i + ".txt");
      }
      return ArtFiles;
    }
    #endregion Private Methods
  }
}
