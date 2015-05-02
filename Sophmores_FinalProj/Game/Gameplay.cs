using IntroCS;
using Sophmores_FinalProj.Utilities;
using System;
using System.Collections.Generic;

namespace Sophmores_FinalProj
{
  internal class Program
  {
    #region Private Methods
    /// <summary>
    /// Checks if the player is Dead or has Run from Combat
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="restoreHealth">
    /// How much health to restore upon exit of Combat
    /// </param>
    /// <returns></returns>
    private static bool DeadOrRunCheck(Player player, int restoreHealth)
    {
      if (!player.isAlive() || Combat.run)
      {
        if (!player.isAlive())
        {
          player.CurrentHP = restoreHealth;
        }
        return true;
      }
      return false;
    }
    /// <summary>
    /// First Level of Game
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy1">First Enemy</param>
    /// <param name="enemy2">Second Enemy</param>
    /// <param name="boss">Stage Boss</param>
    private static void DoorStage(Player player, Enemy enemy1,
                                  Enemy enemy2, Enemy boss)
    {
      Combat.StartCombat(player, enemy1);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }
      Console.ForegroundColor = ConsoleColor.DarkYellow;
      TextUtil.PrintTextFile("foreshadownote.txt");
      Console.ResetColor();
      TextUtil.PressAnyKeyBufferClear();
      Combat.StartCombat(player, enemy2);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }
      Combat.StartCombat(player, boss);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }

      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("\nAfter defeating the {0}, {1} leaves the room. " +
                        "You hear \n the door lock behind you as you step " +
                        "into the lobby.", boss.Name, player.Name);
      Console.ResetColor();
      player.Stage = false;
      Stage2.PrintPreDoorMsg();
      player.Shield = 1;
    }
    /// <summary>
    /// Provides Logic for Plot and Combat for the second stage
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy1">First Enemy to fight</param>
    /// <param name="enemy2">Second Enemy to fight</param>
    /// <param name="boss">Final Boss of Stage</param>
    private static void DoorStage2(Player player, Enemy enemy1,
                                   Enemy enemy2, Enemy boss)
    {
      Stage2.PreNoteMsgs();
      Item note = Stage2.PlayerViewsNote();
      player.AddToInventory(note, 1);
      Stage2.BreakWebsScene();
      Combat.StartCombat(player, enemy1);
      if (DeadOrRunCheck(player, 20))
      {
        return;
      }
      Stage2.JumpOrClimbScene();
      Combat.StartCombat(player, enemy2);
      if (DeadOrRunCheck(player, 20))
      {
        return;
      }
      Stage2.BossScene();
      Combat.StartCombat(player, boss);
      if (DeadOrRunCheck(player, 20))
      {
        return;
      }
      Stage2.Stage2Beat();
      player.Stage = false;
    }
    /// <summary>
    /// Provides Logic for Plot and Combat for the third stage
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy1">First Enemy to fight</param>
    /// <param name="enemy2">Second Enemy to fight</param>
    /// <param name="boss">Final Boss of Stage</param>
    private static void DoorStage3(Player player, Enemy enemy1,
                                    Enemy enemy2, Enemy boss)
    {
      string noteDescription = TextUtil.ReturnTextFile("note 3.txt");
      Item note3 = new Item("Note from skeleton", "Paper", noteDescription);
      Console.WriteLine(note3.description);
      player.AddToInventory(note3, 1);

      Combat.StartCombat(player, enemy1);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }
      Item silverkey = new Item("Silver Key", "key", "Silver key connected to " +
                                                      "a chain");
      Console.WriteLine("Silver Key has been addred to your inventory!");

      Combat.StartCombat(player, enemy2);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }
      Combat.StartCombat(player, boss);
      if (DeadOrRunCheck(player, 10))
      {
        return;
      }
      player.currentStage = 3;
      player.Stage = false;
    }
    /// <summary>
    /// Provides Logic for Plot and Combat for the fourth stage
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy1">First Enemy to fight</param>
    /// <param name="enemy2">Second Enemy to fight</param>
    /// <param name="boss">Final Boss of Stage</param>
    private static void DoorStage4(Player player, Enemy boss1,
                                    Enemy boss2)
    {
      Combat.StartCombat(player, boss1);
      if (DeadOrRunCheck(player, 40))
      {
        return;
      }
      player.Shield = 2;
      Combat.StartCombat(player, boss2);
      if (DeadOrRunCheck(player, 40))
      {
        return;
      }
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine();
      Console.WriteLine("After defeating the {0} and {1}, {2} has " +
                        "Defeated the Game!!", boss1.Name, boss2.Name,
                        player.Name);
      Console.ResetColor();
      player.Stage = false;
      player.currentStage = 4;
    }

    /// <summary>
    /// Get's choice from user based on number of choices, user must enter a
    /// number between 1 and numChoices, otherwise they are asked to enter a
    /// valid input and the prompt is repeated.
    /// </summary>
    /// <param name="numberOfChoices"> The total number of choices. </param>
    /// <param name="choices">
    /// The prompt of the choices, format of choices must be
    ///   <example>
    ///   "optional prompt \n 1)choice1 \n 2)choice2 \n 3)choice3... etc"
    ///   </example>
    /// </param>
    /// <returns>The player choice selected</returns>
    public static int getChoice(int numberOfChoices, string choices, bool door = false)
    {
      string[] options = new string[] { "Door 1", "Door 2", "Door 3",
                                        "Large Door", "Open Inventory" };
      Console.ForegroundColor = ConsoleColor.Cyan;
      if (door == false)
      {
        Console.WriteLine("\n \n" + choices);
      }
      else if (door == true)
      {
        Console.WriteLine("\n \nWhich door would you like to enter?\n ");
        for (int i = 0; i < options.Length; i++)
        {
          if (i == 3)
          {
            Console.ForegroundColor = ConsoleColor.Magenta;
          }
          else if (i == 4)
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
          }
          Console.WriteLine((i + 1) + ") " + options[i]);
        }
        Console.ResetColor();
      }
      Console.ResetColor();

      int choice = UI.PromptInt("\nPlease enter a choice number: ");
      while (choice < 1 || choice > numberOfChoices)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n{0} is not a valid choice!", choice);
        Console.ForegroundColor = ConsoleColor.Blue;
        if (door == false)
        {
          Console.WriteLine(choices);
        }
        else if (door == true)
        {
          Console.WriteLine("\n \nWhich door would you like to enter?\n ");
          for (int i = 0; i < options.Length; i++)
          {
            if (i == 3)
            {
              Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (i == 4)
            {
              Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine((i + 1) + ") " + options[i]);
          }
          Console.ResetColor();
        }
        Console.ResetColor();
        choice = UI.PromptInt("\nPlease enter a valid choice number: ");
      }
      return choice;
    }
    /// <summary>
    /// Creates all enemy objects with Loop drops already added to their
    /// respective inventories
    /// </summary>
    /// <returns>All enemies in an Array</returns>
    private static Enemy[] CreateAllEnemies()
    {
      List<Enemy> AllEnemies = new List<Enemy>();

      //Tutorial Enemy
      Enemy Spider = new Enemy("Small Spider");
      Poison venom = new Poison("Spider Venom", "athough it has a very " +
        "attractive smell, \n this venom will weaken anyone who touches it.",
        false);
      Spider.AddToInventory(venom, 1);


      //STAGE 1 ENEMIES
      Enemy Skeleton = new Enemy("Skeleton", 25, 1, 5, 6);
      Weapon IronSword = new Weapon("Iron Sword", "sword",
        "A Sword made of Iron", 10, 0);
      HealthPotion SkeleFluid = new HealthPotion("Skeleton Fluid",
        "\n Fluid from a skeleton... it looks nutritious", false);
      Skeleton.AddToInventory(IronSword, 1);
      Skeleton.AddToInventory(SkeleFluid, 2);

      Enemy Goblin = new Enemy("Goblin", 50, 2, 7, 9);
      Weapon SteelSword = new Weapon("Blessed Steel Sword", "sword",
        "sword made of steel, with slight magic damage", 15, 10);
      Poison GoblinBlood = new Poison("Goblin Blood",
        "blood from the heart of the Goblin, \na known harmful neurotoxin.", false);
      Goblin.AddToInventory(SteelSword, 1);
      Goblin.AddToInventory(GoblinBlood, 2);

      Enemy Giant = new Enemy("Giant", 70, 3, 10, 13);
      Weapon Shield = new Weapon("Basic Shield", "shield",
        "Shield that slightly reduces enemy damage in battle. " +
        "Automatically equipped when using a sword.", 0, 0);
      HealthPotion GiantMagic = new HealthPotion("Giant Magic",
        "magic potion used by the Giant to cure his injuries", true);
      Item key1 = new Item("Key I", "key",
        "this is the key colloected from first stage");
      Giant.AddToInventory(key1, 1);
      Giant.AddToInventory(Shield, 1);
      Giant.AddToInventory(GiantMagic, 1);

      //STAGE 2 ENEMIES
      Enemy GiantSpider = new Enemy("Giant Spider", 40, 10, 5, 30);
      Weapon BasicBow = new Weapon("BasicBow", "bow", "basic wooden bow",
        20, 0);
      Poison spidervenom = new Poison("Spider Venom", "poison", false);
      GiantSpider.AddToInventory(BasicBow, 1);
      GiantSpider.AddToInventory(spidervenom, 1);

      Enemy Alligator = new Enemy("Alligator", 50, 10, 10, 30, "bow");
      Poison FireArrows = new Poison("Fire Arrows",
        "this quiver contains a poisonous fire arrow", true);
      HealthPotion aligatoreggs = new HealthPotion("Alligator Eggs",
      "Delicous and nourishing alligator eggs!.", false);
      aligatoreggs.Potency = 50;
      Alligator.AddToInventory(FireArrows, 1);
      Alligator.AddToInventory(aligatoreggs, 1);

      Enemy Kraken = new Enemy("Kraken", 70, 10, 10, 40, "bow");
      Weapon Crossbow = new Weapon("CrossBow", "bow,",
        "Higher damage than basic bow, gains magical powers when combined " +
        "with fire arrows", 20, 15);
      Item key2 = new Item("Key II", "key",
        "this is the key collected from second stage");
      HealthPotion Krakenblood = new HealthPotion("Kraken Blood",
        "magical blood that fully restores health", true);
      Kraken.AddToInventory(Crossbow, 1);
      Kraken.AddToInventory(Krakenblood, 1);
      Kraken.AddToInventory(key2, 1);

      //STAGE 3 ENEMIES
      Enemy Wolf = new Enemy("Wolf", 60, 10, 10, 30);
      Weapon Staff = new Weapon("Magic Staff", "staff",
                                "Basic magic staff", 10, 20);
      HealthPotion WolfMilk = new HealthPotion("Wolf Milk",
        "Restores partial health", false);
      Wolf.AddToInventory(Staff, 1);
      Wolf.AddToInventory(WolfMilk, 1);

      Enemy Zombie = new Enemy("Zombie", 80, 10, 10, 40);
      Weapon Lstaff = new Weapon("Lightning Staff", "staff",
        "Metal staff with stronger magical power", 15, 25);
      Poison zblood = new Poison("Zombie Blood",
        "Destructive when poured on skin", true);
      Zombie.AddToInventory(Lstaff, 1);
      Zombie.AddToInventory(zblood, 1);

      Enemy Orc = new Enemy("Orc", 80, 10, 10, 50);
      Item Key3 = new Item("Key III", "key",
        "this is the key collected from third stage");
      Weapon OrcStaff = new Weapon("Orc Staff", "staff",
        "Super-Powered staff from Orc boss", 20, 30);
      HealthPotion orcpotion = new HealthPotion("Orc Portion",
        "Magic potion that restores all health", true);
      Orc.AddToInventory(OrcStaff, 1);
      Orc.AddToInventory(orcpotion, 1);
      Orc.AddToInventory(Key3, 1);

      //Final Stage ENEMIES
      Enemy Dragon = new Enemy("Dragon", 90, 4, 20, 50, "sword");
      Weapon DragonSheild = new Weapon("Dragon Scale Sheild", "sheild",
        "magical sheild that can be used to block magic powers", 0, 0);
      HealthPotion DragonFruit = new HealthPotion("Dragon Fruit",
        "restoes heatlh fully", true);
      Dragon.AddToInventory(DragonSheild, 1);
      Dragon.AddToInventory(DragonFruit, 1);

      Enemy Ghost = new Enemy("Odalf", 100, 5, 20, 55);

      AllEnemies.Add(Spider);         // 0          // Index in the
      AllEnemies.Add(Skeleton);       // 1          // returned array
      AllEnemies.Add(Goblin);         // 2
      AllEnemies.Add(Giant);          // 3
      AllEnemies.Add(GiantSpider);    // 4
      AllEnemies.Add(Alligator);      // 5
      AllEnemies.Add(Kraken);         // 6
      AllEnemies.Add(Wolf);           // 7
      AllEnemies.Add(Zombie);         // 8
      AllEnemies.Add(Orc);            // 9
      AllEnemies.Add(Dragon);         // 10
      AllEnemies.Add(Ghost);          // 11

      return AllEnemies.ToArray();
    }
    /// <summary>
    /// Main Game
    /// </summary>
    private static void Main()
    {
      TextUtil.SetStartingDirectory(); // Snapshots relative directory paths
                                       // Enhanced search speed and avoids
                                       // Some errors if started from shortcut
      ShowGameTitleScreen();
      Enemy[] Enemies = CreateAllEnemies();
      Player p1 = GameIntro.Start(new Player("", 20));
      Tutorial(p1, Enemies);
      Console.WriteLine("Its seems we have come to a fork. There are 4 doors " +
                        "ahead of us.\nLook! there's a note on the wall.");
      string noteDescription = TextUtil.ReturnTextFile("note.txt");
      Item note = new Item("Note", "Paper", noteDescription);
      int response = getChoice(2, " Would you like to: \n 1) Take a Look \n " +
                                  "2) Add to Inventory");
      if (response == 1)
      {
        p1.AddToInventory(note, 1);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(note.description);
        Console.ResetColor();
        Console.WriteLine("\n\n**Hmmmm, kind of odd that the name at the " +
                          "bottom is ripped off**\n\n");
      }
      else
      {
        p1.AddToInventory(note, 1);
      }
      Console.WriteLine("Well lets not take too long, lets start finding " +
                        "keys!\n");
      string doorAccept = "\nOkay, we are going into door ";
      bool playerWins = false;
      while (!(playerWins))
      {
        if (Enemies[11].isAlive() == false)
        {
          playerWins = true;
          break;

        }
        response = getChoice(5, "", true);  // gets player input here
        if (1 <= response && response <= 3)     //checks if response is a door number
        {
          if (!p1.Stage)              // makes sure player is not currently in the middle of a stage.
          {
            if (p1.DoorsOpened.Count == 0)      // checks to see if player has not completed any stages
            {
              if (OpenedDoors(response, p1))      // code for stage 1
              {
                Console.WriteLine(doorAccept + "{0}", response);

                DoorStage(p1, Enemies[1], Enemies[2], Enemies[3]);

                continue;               ////Stage 1 ends here
              }
            }
            else if (p1.DoorsOpened.Count == 1) // checks to see if player has completed 1 stage (so that stage 2 can start)
            {
              if (OpenedDoors(response, p1))  //checks to see if the desired door has been opened before
              {
                Console.WriteLine(doorAccept + "{0}", response);

                DoorStage2(p1, Enemies[4], Enemies[5], Enemies[6]);
                continue;
              }
              else    // response if door has already been opened
              {
                DoorAlreadyComplete();
                continue;
              }
            }
            else if (p1.DoorsOpened.Count == 2) // checks to see if stage 2 has been completed (so that stage 3 can start)
            {
              if (OpenedDoors(response, p1)) // checks to see if the desired door has been opened before
              {
                Console.WriteLine(doorAccept + "{0}", response);

                DoorStage3(p1, Enemies[7], Enemies[8], Enemies[9]);
                continue;
              }
              else               // response if door has been opened before
              {
                DoorAlreadyComplete();
                continue;
              }
            }
            else if (p1.DoorsOpened.Count == 3)
            {
              DoorAlreadyComplete();
              continue;
            }
          }
          else if (p1.Stage)            // if player dies/runs, they are returned to the lobby. In this case stage will be true.
          {                            // this code makes it so player is only allowed to return to the door where they died/ran from.
            if (response == p1.currentDoor)
            {
              int stagesCompleted = p1.DoorsOpened.Count;
              switch (stagesCompleted)
              {
                case 1:
                  DoorStage(p1, Enemies[1], Enemies[2], Enemies[3]);
                  break;

                case 2:
                  DoorStage2(p1, Enemies[4], Enemies[5], Enemies[6]);
                  break;

                case 3:
                  DoorStage3(p1, Enemies[7], Enemies[8], Enemies[9]);
                  break;
                case 4:
                  DoorStage4(p1, Enemies[10], Enemies[11]);
                  break;

              }
              continue;
            }
            else if (response != p1.currentDoor)
            {
              Console.WriteLine("This door is sealed shut and will not open.");
              continue;
            }
          }
        }
        else if (response == 5)
        {
          p1.DisplayInventoryContents();
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("{0}) Close the inventory", p1.allItems.Count + 1);
          Console.ResetColor();
          int choice = getChoice(p1.allItems.Count + 1, "Please type the " +
                                 "number corresponding to the Item \nyou " +
                                 "wish to see the description of.");
          if (choice <= p1.allItems.Count)
          {
            p1.Inspect(p1.allItems[choice - 1]);
            TextUtil.PressAnyKeyBufferClear();
            continue;
          }
          else if (choice == p1.allItems.Count + 1)
          {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nYou have chosen to close the inventory.");
            Console.ResetColor();
            continue;
          }
        }
        else if (response == 4)
        {
          if (p1.currentStage != 3)
          {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nThis door is locked, it smells like plants..." +
                               "\nThis door also seems to have 3 keyholes... " +
                               "very strange...");
            Console.ResetColor();
            TextUtil.PressAnyKeyBufferClear();
            continue;
          }
          else if (p1.currentStage == 3)
          {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You have found all three keys and the door " +
                              "now opens! We are going into the final door!");
            Console.ResetColor();
            DoorStage4(p1, Enemies[10], Enemies[11]);
            if (p1.currentStage == 4)
            {
              break;
            }
          }
        }
      }
      PlayerBeatsGame();
    }
    /// <summary>
    /// Code to End the Game
    /// </summary>
    private static void PlayerBeatsGame()
    {
      TextUtil.PressAnyKeyBufferClear();
      Console.ForegroundColor = ConsoleColor.DarkRed;
      TextUtil.PrintTextFile("GameOver.txt");
      Console.WriteLine("Congratulations on beating the Game!!!");
      TextUtil.PressAnyKeyBufferClear("You have restored Peace to the Woods!");
      for (int i = 0; i < 5; i++)
      {
        Console.WriteLine("...");
      }
      TextUtil.PressAnyKeyBufferClear("Or have you...?");
      TextUtil.PrintTextFile("gamelogo2.txt");
    }
    /// <summary>
    /// Prompts player that the selected door is complete
    /// </summary>
    private static void DoorAlreadyComplete()
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine("\nYou have already completed this Door " +
                        "and it is now sealed, choose another.");
      Console.ResetColor();
    }
    /// <summary>
    /// Provides framework for one-time tutorial
    /// </summary>
    /// <param name="Enemies">All Enemies array</param>
    /// <param name="p1">Main Character</param>
    private static void Tutorial(Player p1, Enemy[] Enemies)
    {
      Combat.StartCombat(p1, Enemies[0]);
      // 
      p1.TutorialComplete = true;
      Console.WriteLine("Congrats on Defeating your first Enemy, {0}!", p1.Name);


      Console.WriteLine("{0}, good job on your combat training, \n we are " +
        "now ready to venture into the tunnel. \nIt will be challening, but " +
        "after seeing your skills, \nI trust you will bring peace to the woods."
        , p1.Name);
      int forward = getChoice(2, "Are you ready to journey into the tunnel?" +
                                 " \n 1) Yes \n 2) No)\n");
      if (forward == 1)
      {
        Console.WriteLine("Good to hear, we are now entering the cave.");
      }
      else
      {
        Console.WriteLine("You're coming anyways... were walking into the" +
                          " tunnel.");
      }
    }
    /// <summary>
    /// Shows the Intro Title Screen
    /// </summary>
    private static void ShowGameTitleScreen()
    {
      TextUtil.SetBufferSize();
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      TextUtil.PrintTextFile("gamelogo.txt");
      Console.ResetColor();
      TextUtil.PressAnyKeyBufferClear();
      Console.ForegroundColor = ConsoleColor.DarkRed;
      TextUtil.PrintTextFile("gamelogo2.txt");
      Console.ResetColor();
    }

    /// <summary>
    /// Checks to see if the selected Door is avalible to be opened
    /// </summary>
    /// <param name="response">Player door choice</param>
    /// <param name="player">Main character</param>
    /// <returns></returns>
    private static bool OpenedDoors(int response, Player player)
    {
      if (response < 1 || response > 4)
      {
        return false;
      }
      else
      {
        if (player.DoorsOpened.Count == 0)
        {
          player.DoorsOpened.Add(response);
          player.currentDoor = response;
          player.Stage = true;
          return true;
        }
        else
        {
          foreach (int r in player.DoorsOpened)
          {
            if (response == r)
            {
              return false;
            }
          }
          player.DoorsOpened.Add(response);
          player.currentDoor = response;
          player.Stage = true;
          return true;
        }
      }
    }

    #endregion Private Methods
  }
}
