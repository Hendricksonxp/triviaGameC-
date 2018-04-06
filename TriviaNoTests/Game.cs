using System;
using System.Collections;


namespace TriviaNoTests {
  public class Game {
    private ArrayList players = new ArrayList();
    private int playerIndex = 0;
    private ArrayList places = new ArrayList();
    private ArrayList purse = new ArrayList();
    private ArrayList penaltyBox = new ArrayList();
    private bool isLeavingThePenaltyBox = true;

    private Queue popQuestions = new Queue();
    private Queue scienceQuestions = new Queue();
    private Queue sportsQuestions = new Queue();
    private Queue rockQuestions = new Queue();


    public Game() {
      for (int i=1; i < 51; i++) {
        popQuestions.Enqueue("Pop Question " + i.ToString());
        scienceQuestions.Enqueue("Science Question " + i.ToString());
        sportsQuestions.Enqueue("Sports Question " + i.ToString());
        rockQuestions.Enqueue("Rock Question " + i.ToString());
        
      }
    }

  
    public void AddPlayer(string playerName){
      if (players.Count == 6) {
        Console.WriteLine(playerName + " NOT added");
        return;
      }
      players.Add(playerName);
      places.Add(0);
      purse.Add(0);
      penaltyBox.Add(false);
      Console.WriteLine(playerName + " added");
    }

    public void TakeTurn() {
      
      string player = (string) players[playerIndex];

      Random roller = new Random((int) DateTime.Now.Ticks);
      int roll = roller.Next(6) + 1;
      Console.WriteLine("\n" + player + " rolls a " + roll);

      if ((bool) penaltyBox[playerIndex]) {

        if ((roll % 2) == 1) {
          isLeavingThePenaltyBox = true;
        }
        else {
          isLeavingThePenaltyBox = false;
        }
      }
      else {
      
        int newPlace = (int) places[playerIndex] + roll;
        if (newPlace > 11) newPlace = newPlace - 11;
        places[playerIndex] = newPlace;

        Console.WriteLine(player + " is now on place " + ((int) places[playerIndex] + 1));
      
        string category = CategoryForPlace(newPlace);

        Console.WriteLine("The category is " + category);

        string question = (string) QuestionForCategory(category);

        Console.WriteLine("Question: " + question);
      }
     
    }

    public bool Correct(){
      if ((bool) penaltyBox[playerIndex]){
        if (isLeavingThePenaltyBox) {
          Console.Write(players[playerIndex] + " is getting out of the penalty Box");
          penaltyBox[playerIndex] = false;
          playerIndex ++;
          if (playerIndex == players.Count) playerIndex = 0;
          return false;
        }
        playerIndex ++;
        if (playerIndex == players.Count) playerIndex = 0;
        return false;
      }

      int score = (int) purse[playerIndex];
      int newScore = score + 1;
      purse[playerIndex] = newScore;


      Console.WriteLine(players[playerIndex] 
        + " answered correctly, and now has " 
        + (purse[playerIndex]).ToString()
        + " coins.\n");

      if (newScore == 6) Console.WriteLine(players[playerIndex] + " has WON!!!!");
      playerIndex ++;
      if (playerIndex == players.Count) playerIndex = 0;
      return (newScore == 6);
    }

    public bool Wrong(){
      if (!(bool)penaltyBox[playerIndex]){
         
        penaltyBox[playerIndex] = true;

        Console.WriteLine(players[playerIndex] 
          + " answered incorrectly, and was sent to the penalty box. \nThey now have " 
          + (purse[playerIndex]).ToString()
          + " coins.\n");
      }
      playerIndex ++;
      if (playerIndex == players.Count) playerIndex = 0;
      return false;
    }

    private string CategoryForPlace(int placeId) {
      if (placeId == 0 ) return "Pop";
      if (placeId == 4 ) return "Pop";
      if (placeId == 8 ) return "Pop";
      if (placeId == 1) return "Science";
      if (placeId == 5) return "Science";
      if (placeId == 9) return "Science";
      if (placeId == 2) return "Sports";
      if (placeId == 6) return "Sports";
      if (placeId == 10) return "Sports";
      return "Rock";
    }

    private object QuestionForCategory(string category){
      if (category == "Pop") return popQuestions.Dequeue();
      if (category == "Science") return scienceQuestions.Dequeue();
      if (category == "Sports") return sportsQuestions.Dequeue();
      return rockQuestions.Dequeue();
    }

  }
}
