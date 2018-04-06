using System;
using TriviaNoTests;

namespace TriviaRunner {
	internal class Class1 {
		private static Game game;

		[STAThread]
		private static void Main() {
			game = new Game();
			game.AddPlayer("Al");
			game.AddPlayer("Bertha");
			game.AddPlayer("Carl");
			game.AddPlayer("Pat");
			game.AddPlayer("Morgan");
			game.AddPlayer("Mark");
			game.AddPlayer("Luke");

			bool aWinner = false;
			int counter = 0;

			while (!aWinner) {
				game.TakeTurn();

				if (counter == 14) {
					game.Wrong();
					counter = 0;
				}
				else {
					aWinner = game.Correct();
					counter++;
				}

			}
			Console.ReadLine();
		}
	}
}