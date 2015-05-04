using System;
using System.Collections.Generic;
using System.Text;

namespace Sophmores_FinalProj
{
  /// <summary>
  /// Contains Methods to Create Players that can Equip Weapons, and
  /// Inspect Items, and more. Inherits from Character
  /// </summary>
  public class Player : Character
  {

    #region Private Fields
    /// <summary>
    /// Player's Default Rusty Sword
    /// </summary>
    private Weapon DefaultWeapon = new Weapon();

    #endregion Private Fields

    #region Public Properties

    /// <summary>
    /// number of the current door the player is on. 
    /// </summary>
    public int currentDoor { get; set; }
    /// <summary>
    /// Current Level the player is in
    /// </summary>
    public int currentStage { get; set; }

    /// <summary>
    /// list of door numbers that have been opened 
    /// </summary>
    public List<int> DoorsOpened { get; set; }
    /// <summary>
    /// Player's currently equipped weapon
    /// </summary>
    public Weapon EquippedWeapon { get; private set; }
    /// <summary>
    /// Player's Magical Damage Stat
    /// </summary>
    public int MagicDamage { get; private set; }
    /// <summary>
    /// Player;s Physical Damage Stat
    /// </summary>
    public int PhysicalDamage { get; private set; }

    /// <summary>
    /// true when player is in a stage, false when player is out of
    /// one/completes one.
    /// </summary>
    public bool Stage { get; set; }

    /// <summary>
    /// total actual damage value player is capable of after buff and weapon
    /// and damage
    /// </summary>
    public int TotalDamage { get; private set; }
    /// <summary>
    /// list containing all current Item's in inventory. 
    /// </summary>
    public List<Item> allItems { get; private set; }
    /// <summary>
    /// true after player completes tutorial 
    /// </summary>
    public bool TutorialComplete { get; set; }
    /// <summary>
    /// Player's shield stat, may be implemented as item later
    /// </summary>
    public int Shield { get; set; }    

    #endregion Public Properties

    #region Public Constructors
    /// <summary>
    /// Creates a Custom Character with a defined Health and Level
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Health"></param>
    /// <param name="Level"></param>
    public Player(string Name, int Health, int Level)
      : base(Name, Health, Level)
    {
      PhysicalDamage = 1;
      MagicDamage = 1;
      Equip(DefaultWeapon);
      BuffMultiplier = 1;
      int damage = PhysicalDamage + MagicDamage;
      TotalDamage = (int)Math.Round(damage * BuffMultiplier);
      TutorialComplete = false;
      Stage = false;
      currentDoor = 0;
      currentStage = 0;
      DoorsOpened = new List<int> { };
      Shield = 0;
    }

    /// <summary>
    /// Creates a Custom Character with a defined Health and Level
    /// </summary>
    /// <param name="Name">Player Character Name</param>
    /// <param name="Health">Player Character Health</param>
    public Player(string Name, int Health)
      : base(Name, Health)
    {
      PhysicalDamage = 1;
      MagicDamage = 1;
      Equip(DefaultWeapon);
      BuffMultiplier = 1;
      int damage = PhysicalDamage + MagicDamage;
      TotalDamage = (int)Math.Round(damage * BuffMultiplier);
      TutorialComplete = false;
      Stage = false;
      currentDoor = 0;
      currentStage = 0;
      DoorsOpened = new List<int> { };
      Shield = 0;
    }

    /// <summary>
    /// Creates Default Player named Douglas with basic attributes 
    /// </summary>
    public Player()
    {
      Name = "Douglas";
      PhysicalDamage = 1;
      MagicDamage = 1;
      Equip(DefaultWeapon);
      BuffMultiplier = 1;
      int damage = PhysicalDamage + MagicDamage;
      TotalDamage = (int)Math.Round(damage * BuffMultiplier);
      TutorialComplete = false;
      Stage = false;
      currentDoor = 0;
      currentStage = 0;
      DoorsOpened = new List<int> { };
    }

    #endregion Public Constructors

    #region Public Methods
    /// <summary>
    /// Allows player to consume health potions
    /// Or use poisions against the enemy
    /// </summary>
    /// <param name="theItem"></param>
    public void ConsumeItem(Item theItem)
    {
      if (theItem is HealthPotion)
      {
        UseHealthPotion(theItem as HealthPotion);
      }
      else if (theItem is Poison)
      {
        UsePoison(theItem as Poison);
      }
      Update();
    }

    /// <summary>
    /// Displays Players Weapons in Inventory
    /// </summary>
    /// <returns></returns>
    public List<Weapon> DisplayAllWeaponsReturnList()
    {
      int count = 1;
      List<Weapon> conList = new List<Weapon>();
      string x = "\nWeapons: \n";
      StringBuilder builder = new StringBuilder(x);
      foreach (Item item in inventory)
      {
          if (item is Weapon && item.type.ToLower().Trim() != "shield")
        {
            if (item != EquippedWeapon)
            {
                builder.AppendLine(count + ") " + item + ", " + item.type);
                count++;
                conList.Add(new Item(item) as Weapon);
            }
        }
      }
      builder.AppendLine(count + ") Keep current weapon Equipped.");
      Console.WriteLine("" + builder);
      return conList;
    }
    /// <summary>
    /// Displays Player's Consumables in Inventory
    /// </summary>
    public void DisplayConsumables()
    {
      int i = 0;
      string x = "Consumables: ";
      StringBuilder builder = new StringBuilder(x);
      foreach (Item item in inventory)
      {
        if (item.consumable)
        {
          int numberofItems = 1;
          if (inventory.contents.TryGetValue(item, out numberofItems))
          {
            if (numberofItems > 1)
            {
              builder.AppendLine(i + ") " + item + ", " + numberofItems);
              i++;
              continue;
            }
          }
          builder.AppendLine(i + ") " + item);
          i++;
        }
      }
      Console.WriteLine("" + builder);
    }
    /// <summary>
    /// Displays Player's Consumables in Inventory
    /// </summary>
    /// <returns>Consumable Items as a List</returns>
    public List<Item> DisplayConsumablesReturnList()
    {
      int count = 1;
      List<Item> conList = new List<Item>();
      string x = "\nConsumables: \n";
      StringBuilder builder = new StringBuilder(x);
      foreach (Item item in inventory)
      {
        if (item.consumable)
        {
          conList.Add(item);
          int numberofItems = 1;
          if (inventory.contents.TryGetValue(item, out numberofItems))
          {
            if (numberofItems > 1)
            {
              builder.AppendLine(count + ") " + item + ", " + item.type + 
                                 ", Quantity: " + numberofItems);
              count++;
              continue;
            }
          }
          builder.AppendLine(count + ") " + item + ", " + item.type + 
                             ", Quantity: " + 1);
          count++;
        }
      }
      builder.AppendLine(count + ") Don't use an item.");
      Console.WriteLine("" + builder);
      return conList;
    }

    /// <summary>
    /// Displays Inventory or specific Item types to Player 
    /// </summary>
    /// <param name="itemType"> Types of items to show </param>
    public void DisplayInventoryContents(string itemType)
    {
      itemType.ToLower();
      if (itemType == "sword" || itemType == "axe" ||
          itemType == "bow" || itemType == "arrow" ||
          itemType == "shield" || itemType == "weapons")
      {
        itemType = "weapon";
      }
      else
      {
        string message = string.Format("That item type {0} is not a " +
                                       "supported item type!", itemType);
        Console.WriteLine(message);
        throw new NotSupportedException(message);
      }
      var inventoryList = new List<Item>(inventory.contents.Keys);
      inventoryList.Sort();
      if (itemType == "weapon")
      {
        DisplayWeapons(inventoryList);
      }
      else if (itemType == "key")
      {
        DisplayKeys(inventoryList);
      }
      else if (itemType == "potion")
      {
        DisplayPotions(inventoryList);
      }
      else if (itemType == "item")
      {
        DisplayItems(inventoryList);
      }
    }

    /// <summary>
    /// Displays ALL Inventory Contents to Player 
    /// </summary>
    public void DisplayInventoryContents()
    {
      var inventoryList = new List<Item>(inventory.contents.Keys);
      inventoryList.Sort();
      allItems = inventoryList;
      DisplayItems(inventoryList);
    }

    /// <summary>
    /// Equips specified weapon by creating new instance of said Weapon and
    /// changing player EquippedWeapon Reference
    /// </summary>
    /// <param name="WeapontoEquip"> Weapon the player will equip </param>
    public void Equip(Weapon WeapontoEquip)
    {
      if (WeapontoEquip.playerCanEquip)
      {
        if (EquippedWeapon != null)
        {
          UnEquip();
        }
        EquippedWeapon = new Weapon(WeapontoEquip);
        PhysicalDamage += WeapontoEquip.physicalDamage;
        MagicDamage += WeapontoEquip.magicalDamage;
        Update();
      }
      else
      {
        Console.WriteLine("You can't Equip that!");
      }
    }

    /// <summary>
    /// For Players to read Item Descriptions 
    /// </summary>
    /// <param name="item"> Item to Describe </param>
    public void Inspect(Item item)
    {
      Console.WriteLine("\nName: {0}", item.name);
      Console.WriteLine("Type: {0}", item.type);
      Console.WriteLine("Description: {0}", item.description);
      if (item is Weapon)
      {
        InspectWeapon(item as Weapon);
      }
      else if (item is HealthPotion)
      {
        InspectPotion(item as HealthPotion);
      }
      else if (item is Key)
      {
        InspectKey(item as Key);
      }
      else if (item is Quiver)
      {
        InspectQuiver(item as Quiver);
      }
    }

    /// <summary>
    /// Unequips currently Equipped Weapon and Equips the Default Weapon
    /// Player will never not have Weapon Equipped
    /// </summary>
    private void UnEquip()
    {
      PhysicalDamage -= EquippedWeapon.physicalDamage;
      MagicDamage -= EquippedWeapon.magicalDamage;
      EquippedWeapon = DefaultWeapon;
      Update();
    }

    /// <summary>
    /// update's stats of player to reflect changes to buff. 
    /// </summary>
    public void Update()
    {
      int damage = PhysicalDamage + MagicDamage;
      TotalDamage = (int)Math.Round(damage * BuffMultiplier);
    }

    #endregion Public Methods

    #region Private Methods

    private void DisplayItems(List<Item> itemList)
    {
        int i = 1;
        Console.WriteLine("\nAll Items:");
        foreach (Item item in itemList)
        {
            Console.WriteLine(i + ") " + item + ", Quantity: " + 
                              inventory.contents[item]);
            i++;
      }
    }

    private void DisplayKeys(List<Item> keyList)
    {
      int i = 0;
      Console.WriteLine("Keys:");
      foreach (Key key in keyList)
      {
        Key tryKey = key as Key;
        if (tryKey != null)
        {
          Console.WriteLine(i + ") " + key + " " + inventory.contents[key]);
          i++;
        }
      }
    }

    private void DisplayPotions(List<Item> potionList)
    {
      int i = 0;
      Console.WriteLine("Potions:");
      foreach (HealthPotion potion in potionList)
      {
        HealthPotion tryPotion = potion as HealthPotion;
        if (tryPotion != null)
        {
          Console.WriteLine(i + ") " + potion + " " + inventory.contents[potion]);
          i++;
        }
      }
    }

    private void DisplayWeapons(List<Item> weaponList)
    {
      int i = 0;
      Console.WriteLine("Weapons:");
      foreach (Item weapon in weaponList)
      {
        Weapon tryWeapon = weapon as Weapon;
        if (tryWeapon != null)
        {
          Console.WriteLine(i + ") " + weapon + " " + inventory.contents[weapon]);
          i++;
        }
      }
    }

    private void InspectKey(Key key)
    {
      Console.WriteLine("What does this key open...?");
    }

    private void InspectPotion(HealthPotion potion)
    {
      Console.WriteLine("Potency: {0}", potion.Potency);
    }

    private void InspectQuiver(Quiver quiver)
    {
      Console.WriteLine("Capacity: {0}", quiver.capacity);
      quiver.ArrowsInQuiver();
    }

    private void InspectWeapon(Weapon weapon)
    {
      Console.WriteLine("Physical Damage: {0}", weapon.physicalDamage);
      Console.WriteLine("Magical Damage: {0}", weapon.magicalDamage);
    }

    #endregion Private Methods
  }
}
