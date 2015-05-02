using IntroCS;
using Sophmores_FinalProj.Utilities;
using System;
using System.Collections.Generic;

namespace Sophmores_FinalProj
{
  public static class Combat
  {
    #region Private Fields

    private static bool playerTurn = false;
    private static int poisonStart;
    private static Random random = new Random();
    private static int turn;

    #endregion Private Fields

    #region Public Fields

    public static bool run;

    #endregion Public Fields

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="enemy"></param>
    public static void StartCombat(Player player, Enemy enemy)
    {
      // Comment this line out to turn off Enemy Art
      player.RemoveBuff();
      run = false;
      if (!enemy.isAlive())
      {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("\nYou walk past the fallen {0}...", enemy.Name);
        Console.ResetColor();
        return;
      }
      else if (enemy.isAlive())
      {
        DisplayEnemyArt(enemy);
        turn = 0;
        poisonStart = -4;
        while ((player.isAlive() && enemy.isAlive()) || !run)
        {
          PlayerAction(player, enemy, turn);
          if (!(enemy.isAlive()))
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + enemy.Name + " has fallen!");
            Console.ResetColor();
            starLine();
            GainEnemyItems(player, enemy);
            EndFight(player);
            break;
          }
          EnemyAttack(player, enemy);
          if (!(player.isAlive()))
          {
            if (!player.TutorialComplete)
            {
              Console.ForegroundColor = ConsoleColor.DarkYellow;
              Console.WriteLine("\nLooks like you could use some help, here's some more health");
              Console.ForegroundColor = ConsoleColor.Green;
              Console.WriteLine("\n{0} has replenished 20 HP!", player.Name);
              Console.ForegroundColor = ConsoleColor.DarkYellow;
              Console.WriteLine("\nBe more careful, I won't be able to save you later!");
              Console.ResetColor();
            }
            else
            {
              Console.ForegroundColor = ConsoleColor.DarkBlue;
              TextUtil.PrintTextFile("Death.txt");
              Console.WriteLine("\n..." + player.Name + " has blacked out and is in critical condition...");
              Console.WriteLine("...\n...\n...\n...\nPress any key to continue");
              Console.ReadKey(true);
              Console.ForegroundColor = ConsoleColor.DarkYellow;
              Console.WriteLine("\n" + player.Name + " has woken back up in the door lobby somehow feeling a little better...");
              Console.ResetColor();
              EndFight(player);
              starLine();
              break;
            }
          }
          if (run)
          {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + player.Name + " has succesfully run from the " + enemy.Name + "!");
            Console.WriteLine(player.Name + " has returned to the door lobby.");
            Console.ResetColor();
            EndFight(player);
            starLine();
            break;
          }
          turn++;
          Depoison(player);
        }
        EndFight(player);
      }
      return;
    }

    #endregion Public Methods

    #region Private Methods
    /// <summary>
    /// Checks if it has been 4 turns since the player used poison, if so it disables
    /// </summary>
    /// <param name="player"></param>
    private static void Depoison(Player player)
    {
      if (turn == poisonStart + 4)
      {
        player.RemoveBuff();
        Console.WriteLine("The poison on the enemy has worn off!");
      }
    }

    /// <summary>
    /// Displays Enemy ASCII art files if they exist
    /// </summary>
    /// <param name="enemy">Current Enemy</param>
    private static void DisplayEnemyArt(Enemy enemy)
    {
      string[] artFiles = enemy.ArtFiles;
      if (artFiles != null)
      {
        TextUtil.SetBufferSize();
        for (int i = 0; i < 50; i++)
        {
          foreach (string file in artFiles)
          {
            Console.Clear();
            TextUtil.PrintTextFile(file);
          }
          Console.WriteLine();
        }
      }
      string message = string.Format("\nA Wild {0} has appeared!! It appears " +
                                    "to have {1}HP.", enemy.Name, enemy.TotalHP);
      TextUtil.PressAnyKeyNOBufferClear(message);
    }

    /// <summary>
    /// Returns values back to default after fight ends
    /// </summary>
    /// <param name="player">Main Character</param>
    private static void EndFight(Player player)
    {
      turn = 0;
      poisonStart = -4;
      player.RemoveBuff();
    }
    /// <summary>
    /// Enemy Attack Logic
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy">Attacking Enemy</param>
    private static void EnemyAttack(Player player, Enemy enemy)
    {
      string playerWeaponType = player.EquippedWeapon.type.ToLower().Trim();
      int attack = random.Next(enemy.MinDamage, enemy.MaxDamage);
      if ((player.Shield != 0) && (playerWeaponType == "sword"))
      {
        int block = 0;
        string type = "";
        switch (player.Shield)
        {
          case 1:
            block = random.Next(1, 15);
            type = "basic";
            attack -= block;
            break;
          case 2:
            block = random.Next(15, 25);
            type = "dragon scale";
            attack -= block;
            break;
        }
        Console.WriteLine("\n{0}'s {1} shield blocked {2} damage from the " + 
                          "enemy.", player.Name, type, block);
      }
      Console.WriteLine("\nEnemy attacks for {0} damage!", attack);
      player.ModifyCurrentHP(-1 * attack);
      Console.WriteLine("{0} Health: {1} \n", player.Name, player.CurrentHP);
    }
    /// <summary>
    /// When Enemy dies, Player gains inventory contents of said Enemy
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy">Enemy to Loot</param>
    private static void GainEnemyItems(Player player, Enemy enemy)
    {
      if (!(enemy.isAlive()))
      {
        foreach (KeyValuePair<Item, int> a in enemy.inventory.contents)
        {
          player.AddToInventory(a.Key, 1);
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("{0} has been added to your inventory!", a.Key.name);
          Console.ResetColor();
          if (a.Key is Weapon && a.Key.type.ToLower().Trim() != "shield")
          {
            string question = ("Would you like to equip " + a.Key.name +
                               "? \n 1)Yes \n 2)No\n");
            Console.WriteLine(question);
            int answer = GetChoice(2);
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
            Console.WriteLine(question);
            int answer = GetChoice(2);
            Console.ForegroundColor = ConsoleColor.Green;
            if (answer == 1)
            {
              player.ConsumeItem(a.Key as HealthPotion);
              Console.WriteLine("\nYou have consumed the potion, your " +
                                "current HP is {0}.", player.CurrentHP);
            }
            else if (answer == 2)
            {
              Console.WriteLine();
              Console.WriteLine("You have decided not to consume the potion.");
              Console.WriteLine();
            }
            Console.ResetColor();
          }
        }
      }
    }
    /// <summary>
    /// cleaner way to get choices.
    /// </summary>
    /// <param name="numChoices">
    /// number of choices the user should have.
    /// </param>
    /// <returns></returns>
    private static int GetChoice(int numChoices)
    {
      int choice = UI.PromptInt("\nPlease enter a choice number: ");
      while (choice < 1 || choice > numChoices)
      {
        Console.WriteLine("{0} is not a valid choice!", choice);
        choice = UI.PromptInt("Please enter a valid choice number: ");
      }
      return choice;
    }

    /// <summary>
    /// Displays user-side combat options and retrieves string input answer
    /// </summary>
    /// <returns></returns>
    private static string PlayerInputOptions()
    {
      starLine();
      Console.WriteLine("Make your choice...");
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("1) Attack  ");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("2) Swap  ");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("3) Use  ");
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("4) Run");
      Console.ResetColor();
      string playerInput = UI.PromptLine("\nchoice: ");
      return playerInput.Trim().ToLower();
    }
    /// <summary>
    /// Player side turn logic
    /// </summary>
    /// <param name="player">Main Character</param>
    /// <param name="enemy">Current Enemy</param>
    /// <param name="turn">Current turn #</param>
    private static void PlayerAction(Player player, Enemy enemy, int turn)
    {
      string[] msgs = new string[] 
      {                                                       // message Index
        "Player has equipped ",                                           // 0
        "You can't Equip that!",                                          // 1
        "You have chosen to keep your current weapon equipped.",          // 2
        "No item used.",                                                  // 3
        "You can't run, this is your first fight!",                       // 4
        "The enemy saw it coming this time, you were not able to escape!" // 5
      };
      playerTurn = true;
      while (playerTurn == true)
      {
        int curInput = preCombatCheck(player);
        if (curInput == 1)
        {
          PlayerAttack(player, enemy);
          playerTurn = false;
          break;
        }
        else if (curInput == 2)
        {
          List<Item> curWeapons = player.DisplayAllWeaponsReturnList();

          int choice = GetChoice(curWeapons.Count + 1);
          if (choice - 1 < curWeapons.Count)
          {
            if ((curWeapons[choice - 1]).playerCanEquip)
            {
              player.Equip(curWeapons[choice - 1] as Weapon);
              Console.WriteLine(msgs[0] + curWeapons[choice - 1].name + ".");
              playerTurn = false;
              break;
            }
            else
            {
              Console.WriteLine(msgs[1]);
            }
          }
          else
          {
            Console.WriteLine(msgs[2]);
          }
        }
        else if (curInput == 3)
        {
          List<Item> ConsubableItems = player.DisplayConsumablesReturnList();

          int choice = GetChoice(ConsubableItems.Count + 1);
          if (choice - 1 < ConsubableItems.Count)
          {
            if (ConsubableItems[choice - 1] is Poison) 
            { 
              poisonStart = turn;
            }
            player.ConsumeItem(ConsubableItems[choice - 1]);
            Console.WriteLine(player.Name +  
                              ConsumableMessage(ConsubableItems[choice - 1]));
            playerTurn = false;
            break;
          }
          else 
          {
            Console.WriteLine(msgs[3]);
          }
        }
        else if (curInput == 4)
        {
          if (player.TutorialComplete == false)
          {
            Console.WriteLine(msgs[4]);
            playerTurn = false;
            break;
          }
          int chance = random.Next(100);
          if (chance <= 40)
          {
            run = true;
            playerTurn = false;
            break;
          }
          else
          {
            Console.WriteLine(msgs[5]);
            playerTurn = false;
            break;
          }
        }
      }
    }

    /// <summary>
    /// Player attacks enemy for player.TotalDamage IF Enemy Affinity does
    /// cause Enemy to be immune
    /// </summary>
    private static void PlayerAttack(Player player, Enemy enemy)
    {
      string enemyAffinity = enemy.Affinity.ToLower().Trim();
      string playerWeaponType = (enemyAffinity == "n") ? "n" : player.EquippedWeapon.type.ToLower().Trim();
      if (playerWeaponType != enemyAffinity)
      {
        Console.WriteLine("Enemy Health: {0} \n", enemy.CurrentHP);
        Console.WriteLine("{0} attacks for {1} damage!",
                          player.Name, (player.TotalDamage / 2));
        enemy.ModifyCurrentHP(-1 * (player.TotalDamage / 2));
        Console.WriteLine("The enemy seems to be strong against this weapon " + 
                          "type!");
        Console.WriteLine("Enemy Health: {0} \n", enemy.CurrentHP);
        return;
      }
      Console.WriteLine("Enemy Health: {0}", enemy.CurrentHP);
      Console.WriteLine("{0} attacks for {1} damage!", player.Name,
                        player.TotalDamage);
      enemy.ModifyCurrentHP(-1 * player.TotalDamage);
      Console.WriteLine("Enemy Health: {0} \n", enemy.CurrentHP);
    }

    /// <summary>
    /// Turns string input into number input
    /// </summary>
    private static int ProcessPlayerInput(string input)
    {
      if (input == "attack" || input == "1") { return 1; }
      else if (input == "swap" || input == "2") { return 2; }
      else if (input == "use" || input == "3") { return 3; }
      else if (input == "run" || input == "4") { return 4; }
      else
        return 0;
    }

    /// <summary>
    /// Checks various parameters and locks options that player should not
    /// Have access to.
    /// </summary>
    private static int preCombatCheck(Player player)
    {
      int curInput = ProcessPlayerInput(PlayerInputOptions());
      bool validInp = false;
      while (validInp == false)
      {
        if (curInput == 1)
        {
          validInp = true;
          break;
        }
        if (curInput == 2 || curInput == 3)
        {
          if (player.inventory.contents.Count > 0)
          {
            validInp = true;
            break;
          }
          else
          {
            Console.WriteLine("You fool! You have no Items in your inventory!");
            curInput = ProcessPlayerInput(PlayerInputOptions());
          }
        }
        if (curInput == 4 && player.TutorialComplete == false)
        {
          Console.WriteLine("You can't run, this is your first fight!");
          curInput = ProcessPlayerInput(PlayerInputOptions());
        }
        if (curInput == 4 && player.TutorialComplete == true)
        {
          validInp = true;
          break;
        }
        if (curInput == 0)
        {
          Console.WriteLine("You've entered an invalid command");
          curInput = ProcessPlayerInput(PlayerInputOptions());
        }
      }
      return curInput;
    }

    /// <summary>
    /// Prints a line of stars
    /// </summary>
    private static void starLine()
    {
      Console.WriteLine();
      for (int i = 0; i < 65; i++) { Console.Write("*"); }
      Console.WriteLine("*");
    }
    /// <summary>
    /// Displays Message to player when Consumable Item is used
    /// </summary>
    /// <param name="currentItem">Item to use</param>
    /// <returns>Flavor text dependent on item used</returns>
    private static string ConsumableMessage(Item currentItem)
    {
      string message;
      if (currentItem is Poison)
      {
        message = (" has poisoned the enemy with " + currentItem.name +
                   " for three turns.");
        return message;
      }
      else if (currentItem is HealthPotion)
      {
        message = (" has used " + currentItem.name + " to recover HP.");
        return message;
      }
      else
      {
        return "";
      }
    }

    #endregion Private Methods
  }
}
