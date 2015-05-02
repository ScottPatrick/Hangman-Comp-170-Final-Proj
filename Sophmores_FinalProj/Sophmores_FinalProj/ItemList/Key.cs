namespace Sophmores_FinalProj
{
  /// <summary>
  /// Keys tend to be able to open doors...
  /// </summary>
  public class Key : Item
  {
    #region Public Constructors

    /// <summary>
    /// Keys, type Key, are consumable 
    /// </summary>
    public Key()
    {
      type = "Key";
      consumable = true;
    }

    #endregion Public Constructors
  }
}
