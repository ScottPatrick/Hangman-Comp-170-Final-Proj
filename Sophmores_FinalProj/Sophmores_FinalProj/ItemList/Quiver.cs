using System;
using System.Linq;

namespace Sophmores_FinalProj
{
  public class Quiver : Item
  {
    #region Public Properties

    public bool[] arrowSlots { get; private set; }

    public int capacity { get; set; }

    #endregion Public Properties

    #region Public Constructors

    /// <summary>
    /// Creates a customizable Quiver 
    /// </summary>
    /// <param name="QuiverName"> Name of Quiver </param>
    /// <param name="QuiverDescription"> Description of Quiver </param>
    /// <param name="QuiverCapacity"> Amount of Arrows Quiver can Hold </param>
    public Quiver(string QuiverName, string QuiverDescription, int QuiverCapacity)
      : base(QuiverName, "Quiver", QuiverDescription)
    {
      capacity = QuiverCapacity;
      arrowSlots = new bool[capacity];
      playerCanEquip = true;
    }

    /// <summary>
    /// Creates a Basic Quiver that can hold 8 arrows
    ///</summary>
    public Quiver()
    {
      name = "Simple Quiver";
      type = "Quiver";
      capacity = 8;
      arrowSlots = new bool[capacity];
      playerCanEquip = true;
    }

    #endregion Public Constructors

    /// <summary>
    /// Add the specified amount arrows to the specified quiver 
    /// </summary>
    /// <param name="item"> Quiver to Add to </param>
    /// <param name="AmountOfArrowsToAdd"> Number of arrows to Add </param>

    #region Public Methods

    public void Add(Item item, int AmountOfArrowsToAdd)
    {
      int startToAdd = 0;
      for (int i = 0; i < arrowSlots.Length; i++)
      {
        if (arrowSlots[i] == false)
        {
          startToAdd = i;
          break;
        }
      }
      if (AmountOfArrowsToAdd <= (capacity - startToAdd))
      {
        int currentAdd = startToAdd;
        for (int i = 0; i < AmountOfArrowsToAdd; i++)
        {
          arrowSlots[currentAdd] = true;
          currentAdd++;
        }
      }
      else
      {
        Console.WriteLine("Cannot add that many {0}'s to {1}. {1} is not big " +
                          "enough", item.name, this.name);
      }
    }

    /// <summary>
    /// Get Amount of Arrows in the Quiver 
    /// </summary>
    /// <returns> Number of arrows as int </returns>
    public int ArrowsInQuiver()
    {
      int amountofArrows = 0;
      foreach (bool arrowCheck in arrowSlots)
      {
        if (arrowCheck)
        {
          amountofArrows++;
        }
      }
      Console.WriteLine(amountofArrows);
      return amountofArrows;
    }

    /// <summary>
    /// Remove the amount of specified arrows from the specified quiver 
    /// </summary>
    /// <param name="item"> Quiver to Remove from </param>
    /// <param name="AmountOfArrowsToRemove"> Number of arrows to Remove </param>
    public void Remove(Item item, int AmountOfArrowsToRemove)
    {
      if (AmountOfArrowsToRemove <= (ArrowsInQuiver()))
      {
        arrowSlots.Reverse();
        int startToRemove = 0;
        for (int i = 0; i < arrowSlots.Length; i++)
        {
          if (i == (arrowSlots.Length - 1) && arrowSlots[i] == false)
          {
            Console.WriteLine("You don't have any of those");
          }
          else if (arrowSlots[i])
          {
            startToRemove = i;
            break;
          }
        }
        for (int i = 0; i < AmountOfArrowsToRemove; i++)
        {
          arrowSlots[i] = false;
        }
        arrowSlots.Reverse();
      }
      else
      {
        for (int i = 0; i < arrowSlots.Length; i++)
        {
          arrowSlots[i] = false;
        }
        Console.WriteLine(name + " is now empty...");
      }
    }

    #endregion Public Methods
  }
}
