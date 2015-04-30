using IntroCS;
using Sophmores_FinalProj.Utilities;
using System;
using System.Collections.Generic;

namespace Sophmores_FinalProj
{
  internal class Program
  {
    #region Private Methods

    private static void DoorStage(Player player, Enemy enemy1,
                                  Enemy enemy2, Enemy boss)
    {
      Combat.StartCombat(player, enemy1);
      GainEnemyItems(player, enemy1);
      if (!player.isAlive() || Combat.run)
      {
        if (!player.isAlive()) { player.CurrentHP = 10; }
        return;
      }
      Combat.StartCombat(player, enemy2);
      GainEnemyItems(player, enemy2);
      if (!player.isAlive() || Combat.run)
      {
        if (!player.isAlive()) { player.CurrentHP = 10; }
        return;
      }
      Combat.StartCombat(player, boss);
      GainEnemyItems(player, boss);
      if (!player.isAlive() || Combat.run)
      {
        if (!player.isAlive()) { player.CurrentHP = 10; }
        return;
      }
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("\nAfter defeating the {0}, {1} leaves the room. " +
                        "You hear \n the door lock behind you as you step " +
                        "into the lobby.", boss.Name, player.Name);
      Console.ResetColor();
      player.Stage = false;
    }

    private static void DoorStage2(Player player, Enemy enemy1,
                                   Enemy enemy2, Enemy boss)
    {
      string noteDescription = TextUtil.ReturnTextFile("stage2note.txt");
      Item note = new Item("StarNote", "Paper", noteDescription);
      Console.WriteLine(note.description);
      player.AddToInventory(note, 1);

      string question = "Sword or Wand? \n 1)Sword \n 2)Wand";
      int answer = getChoice(2, question);
      bool pass = false;
      while (pass != true)
      {
        if (answer == 1)
        {
          Console.WriteLine("it does not effect");
          answer = getChoice(2, "maybe try a different weapon. " +
                                "\n 1)Sword \n 2)Wand");
        }
        else if (answer == 2)
        {
          Console.WriteLine("The webs have fallen \n");
          pass = true;
        }
      }

      Combat.StartCombat(player, enemy1);
      GainEnemyItems(player, enemy1);

      string question2 = "Jump or Climb? \n 1)Jump \n 2)Climb";
      int answer2 = getChoice(2, question2);
      bool pass2 = false;
      while (pass2 != true)
      {
        if (answer == 1)
        {
          Console.WriteLine("jump story \n");
          pass2 = true;
        }
        else if (answer == 2)
        {
          Console.WriteLine("climb story \n");
          pass2 = true;
        }
      }
    }

    private static void GainEnemyItems(Player player, Enemy enemy)
    {
      if (!(enemy.isAlive()))
      {
        foreach (KeyValuePair<Item, int> a in enemy.inventory.contents)
        {
          player.AddToInventory(a.Key, 1);
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("{0} has been added to your inventory!", a.Key.name);
          if (a.Key is Weapon)
          {
            string question = ("Would you like to equip " + a.Key.name +
                               "? \n 1)Yes \n 2)No\n");
            int answer = getChoice(2, question);
            Console.ForegroundColor = ConsoleColor.Blue;
            if (answer == 1)
            {
              player.Equip(a.Key as Weapon);
              Console.WriteLine("\nYour new weapon has been equipped!\n");
            }
            else if (answer == 2)
            {
              Console.WriteLine("\nYou have not equipped your newest weapon.\n");
            }
            Console.ResetColor();
          }
          else if (a.Key is HealthPotion)
          {
            string question = "Would you like to consume " + a.Key.name +
                              "?, Its desciption is : " + a.Key.description +
                              " \n 1)Yes \n 2)No";
            int answer = getChoice(2, question);
            Console.ForegroundColor = ConsoleColor.Green;
            if (answer == 1)
            {
              player.ConsumeItem(a.Key as HealthPotion);
              Console.WriteLine("\nYou have consumed the potion, your " +
                                "current HP is {0}.", player.CurrentHP);
            }
            else if (answer == 2)
            {
              Console.WriteLine("\nYou have decided not to consume the potion.\n");
            }
            Console.ResetColor();
          }
        }
      }
    }

    /// <summary>
    /// Get's choice from user based on number of choices, user must enter a
    /// number between 1 and numChoices, otherwise they are asked to enter a
    /// valid input and the prompt is repeated.
    /// </summary>
    /// <param name="numChoices"> The total number of choices. </param>
    /// <param name="choices">
    /// The prompt of the choices, format of choices must be in "optional
    /// prompt \n 1)choice1 \n 2)choice2 \n 3)choice3... etc"
    /// </param>
    /// <returns></returns>
    private static int getChoice(int numChoices, string choices, bool door = false)
    {
      Console.ForegroundColor = ConsoleColor.Blue;
      if (door == false) { Console.WriteLine("\n \n" + choices); }
      else if (door == true)
      {
        Console.Write("\n \nWhich door would you like to enter? " +
                      "\n 1) Door 1 \n 2) Door 2 \n 3) Door 3");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n 4) Open Inventory \n");
      }
      Console.ResetColor();
      int choice = UI.PromptInt("\nPlease enter a choice number: ");
      while (choice < 1 || choice > numChoices)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n{0} is not a valid choice!", choice);
        Console.ForegroundColor = ConsoleColor.Blue;
        if (door == false) { Console.WriteLine(choices); }
        else if (door == true)
        {
          Console.Write("Which door would you like to enter? \n 1) Door 1 \n 2) Door 2 \n 3) Door 3");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("\n 4) Open Inventory \n");
        }
        Console.ResetColor();
        choice = UI.PromptInt("Please enter a valid choice number: ");
      }
      return choice;
    }

    private static void Main(string[] args)
    {
      TextUtil.SetBufferSize();
      Console.ForegroundColor = ConsoleColor.DarkGreen;
      TextUtil.PrintTextFile("gamelogo.txt");
      Console.ResetColor();
      TextUtil.PressAnyKeyBufferClear();
      Console.ForegroundColor = ConsoleColor.DarkRed;
      TextUtil.PrintTextFile("gamelogo2.txt");
      Console.ResetColor();
      Player p1 = GameIntro.Start(new Player("", 100, 10));
      p1.CurrentHP = 20;
      Console.Write(p1.Name + ", ");
      Enemy Spider = new Enemy();
      Spider.Name = "Small Spider";
      Combat.StartCombat(p1, Spider);
      p1.TutorialComplete = true;
      Console.WriteLine("Congrats on Defeating your first Enemy, {0}!", p1.Name);
      Poison venom = new Poison("Spider Venom", "athough it has a very attractive smell, " +
                            "\n this venom will weaken anyone who touches it.", false);
      Console.WriteLine("The {0} has dropped {1}, {2}. \n You can now add it to your inventory if you desire."
        , Spider.Name, venom.name, venom.description);
      int answer = getChoice(2, "Would you like to add to inventory? \n 1) Yes \n 2) No)\n");
      if (answer == 1)
      {
        p1.AddToInventory(venom, 1);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("{0} has been added to you inventory.", venom.name);
        Console.ResetColor();
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("you have dropped {0}", venom.name);
        Console.ResetColor();
      }

      //STAGE 1 ENEMIES
      Enemy skeleton = new Enemy("Skeleton", 10, 1, 5, 6);
      Weapon IronSword = new Weapon("Iron Sword", "sword", "sword made of iron", 10, 0);
      HealthPotion skeleFluid = new HealthPotion("Skeleton Fluid", "\n Fluid from a skeleton... it looks nutritious", false);
      skeleton.AddToInventory(IronSword, 1);
      skeleton.AddToInventory(skeleFluid, 2);

      Enemy goblin = new Enemy("Goblin", 20, 2, 7, 9);
      Weapon SteelSword = new Weapon("#blessed Steel Sword", "sword", "sword made of steel, with slight magic damage", 15, 10);
      HealthPotion GoblinBlood = new HealthPotion("Goblin Blood", "blood from the heart of the Goblin", false);
      goblin.AddToInventory(SteelSword, 1);
      goblin.AddToInventory(GoblinBlood, 2);

      Enemy giant = new Enemy("Giant", 35, 3, 10, 13);
      Weapon shield = new Weapon("Shield", "shield", "shield that cuts enemy damage in half when equipped in battle", 20, 0);
      HealthPotion GiantMagic = new HealthPotion("Giant Magic", "magic potion used by the Giant to cure his injuries", true);
      Item key1 = new Item("Key I", "key", "this is the key colloected from first stage");
      giant.AddToInventory(key1, 1);
      giant.AddToInventory(shield, 1);
      giant.AddToInventory(GiantMagic, 1);

      //STAGE 2 ENEMIES
      Enemy GiantSpider = new Enemy("Giant Spider", 20, 10, 5, 7);
      Weapon BasicBow = new Weapon("BasicBow", "bow", "basic wooden bow", 20, 0);
      Poison spidervenom = new Poison("Spider Venom", "poison", true);
      GiantSpider.AddToInventory(BasicBow, 1);
      GiantSpider.AddToInventory(spidervenom, 1);

      Enemy alligator = new Enemy("Alligator", 20, 10, 5, 7);
      Quiver firearrows = new Quiver("Fire Arrows", "this quiver contains 8 fire arrows", 8);
      HealthPotion aligatoreggs = new HealthPotion("Alligator Eggs", "Restores health", false);
      alligator.AddToInventory(firearrows, 1);
      alligator.AddToInventory(aligatoreggs, 1);

      Enemy kraken = new Enemy("Kraken", 20, 10, 5, 7);
      Weapon crossbow = new Weapon("CrossBow", "bow,", "higher damage than basic bow, magical powers when combined with fire arrows", 20, 15);
      Item key2 = new Item("Key II", "key", "this is the key collected from second stage");
      HealthPotion Krakenb = new HealthPotion("Kraken Blood", "magical blood that fully restores health", true);
      kraken.AddToInventory(crossbow, 1);
      kraken.AddToInventory(Krakenb, 1);
      kraken.AddToInventory(key2, 1);

      //STAGE 3 ENEMIES
      Enemy wolf = new Enemy("Wolf", 20, 10, 5, 7);
      Enemy zombie = new Enemy("Zombie", 20, 10, 5, 7);
      Enemy orc = new Enemy("Orc", 20, 10, 5, 7);
      Item key3 = new Item("Key III", "key", "this is the key collected from third stage");
      orc.AddToInventory(key3, 1);

      Console.WriteLine("{0}, good job on your combat training, \n we are now ready to venture" +
      " into the tunnel. \nIt will be challening, but after seeing your skills, \nI trust you will bring peace to the woods.", p1.Name);
      int forward = getChoice(2, "Are you ready to journey into the tunnel? \n 1) Yes \n 2) No)\n");
      if (forward == 1)
      {
        Console.WriteLine("Good to hear, we are now entering the cave.");
      }
      else
      {
        Console.WriteLine("You're coming anyways... were walking into the tunnel.");
      }
      Console.WriteLine("Its seems we have come to a fork. There are 4 doors ahead of us." +
      "\n Look! there's a note on the wall.");
      string noteDescription = TextUtil.ReturnTextFile("note.txt");
      Item note = new Item("Note", "Paper", noteDescription);
      int response = getChoice(2, " Would you like to: \n 1) Take a Look \n 2) Add to Inventory");
      if (response == 1)
      {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(note.description);
        Console.ResetColor();
        Console.WriteLine("\n\n**Hmmmm, kind of odd that the name at the bottom is ripped off**\n\n");
      }
      else
      {
        p1.AddToInventory(note, 1);
      }
      p1.AddToInventory(note, 1);
      Console.WriteLine("Well lets not take too long, lets start finding keys!\n");
      bool responseIsGood = false;
      while (!(responseIsGood))
      {
        response = getChoice(4, "", true);  // gets player input here
        if (1 <= response && response <= 3)     //checks if response is a door number
        {
          if (!p1.Stage)              // makes sure player is not currently in the middle of a stage.
          {
            if (p1.DoorsOpened.Count == 0)      // checks to see if player has not completed any stages
            {
              if (OpenedDoors(response, p1))      // code for stage 1
              {
                Console.WriteLine("\nOkay, we are going into door {0}", response);

                DoorStage(p1, skeleton, goblin, giant);

                continue;               ////Stage 1 ends here
              }
            }
            else if (p1.DoorsOpened.Count == 1) // checks to see if player has completed 1 stage (so that stage 2 can start)
            {
              if (OpenedDoors(response, p1))  //checks to see if the desired door has been opened before
              {
                Console.WriteLine("\nOkay, we are going into door {0}", response);

                DoorStage(p1, GiantSpider, alligator, kraken);
                continue;
              }
              else    // response if door has already been opened
              {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou have already completed this Door and it is now sealed, choose another.");
                Console.ResetColor();
                continue;
              }
            }
            else if (p1.DoorsOpened.Count == 2) // checks to see if stage 2 has been completed (so that stage 3 can start)
            {
              if (OpenedDoors(response, p1)) // checks to see if the desired door has been opened before
              {
                Console.WriteLine("\nOkay, we are going into door {0}", response);

                DoorStage(p1, wolf, zombie, orc);
                continue;
              }
              else               // response if door has been opened before
              {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou have already completed this Door and it is now sealed, choose another.");
                Console.ResetColor();
                continue;
              }
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
                  DoorStage(p1, skeleton, goblin, giant);
                  break;

                case 2:
                  DoorStage(p1, GiantSpider, alligator, kraken);
                  break;

                case 3:
                  DoorStage(p1, wolf, zombie, orc);
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
        else if (response == 4)
        {
          p1.DisplayInventoryContents();
          string input = UI.PromptLine("Spell out the name of Item you wish to see description of. (case doesn't matter)");
          foreach (KeyValuePair<Item, int> a in p1.inventory.contents)
          {
            if (input.Trim().ToLower() == a.Key.name.Trim().ToLower())
            {
              p1.Inspect(a.Key);
            }
            else
            {
              input = UI.PromptLine("There is no item in your inventory that matches that input. Try again.");
            }
          }
        }
        else if (p1.currentStage == 3 &&
        response != 4)
        {
          responseIsGood = true;
          continue;
        }
      }
    }

    private static bool OpenedDoors(int response, Player player)
    {
      if (response < 1 || response > 3) { return false; }
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
