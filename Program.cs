using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the SDG Blackjack Table! Try your luck 🃏! ");

            bool continuePlaying = true;

            // MAIN GAME LOOP
            while (continuePlaying)
            {
                // Create starting Deck
                List<Card> deck = generateListDeck();

                // Shuffle Deck and create a stack for playing 
                Stack<Card> playingDeck = shuffleDeck(deck);

                // Deal hands for player and dealer
                List<Card> playerHand = new List<Card>() { };
                List<Card> dealerHand = new List<Card>() { };
                int playerHandValue = 0;
                int dealerHandValue = 0;

                playerHand.Insert(0, playingDeck.Pop());
                dealerHand.Insert(0, playingDeck.Pop());
                playerHand.Insert(0, playingDeck.Pop());

                // Show player their cards
                Console.WriteLine("Your hand: ");
                foreach (Card card in playerHand)
                {
                    Console.WriteLine($"{card.Name} : {card.Value}");
                    playerHandValue += card.Value;
                }
                Console.WriteLine($"Total hand value: {playerHandValue}. ");

                // Show Dealer's first card
                Console.WriteLine($"Dealer card is: {dealerHand[0].Name} with value {dealerHand[0].Value}. ");
                dealerHandValue += dealerHand[0].Value;

                // BEGIN HIT/STAND LOOP
                // Ask the player for "Hit" or "Stand" until "Stand" or Bust (total hand value is > 21)
                string playerResponse = "";
                while (!(playerHandValue > 21 || playerResponse == "s"))
                {
                    Console.WriteLine("Would you like to HIT [h] or STAND [s]? ");
                    playerResponse = Console.ReadLine();

                    if (playerResponse == "h")
                    {
                        Card dealtCard = playingDeck.Pop();
                        playerHand.Insert(playerHand.Count, dealtCard);
                        playerHandValue += dealtCard.Value;
                        Console.WriteLine($"You were dealt a {dealtCard.Name} and your new total hand value is : {playerHandValue}. ");
                    }
                }

                // Calculate if the player busted
                if (playerHandValue > 21)
                {
                    Console.WriteLine("Ouch! Your hand value is over 21, you bust and house wins. ");
                    Console.WriteLine("Would you like to play again? YES [y] or NO [n] ");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }

                // Dealer reveals hand (added second card to value), and keeps hitting until handValue >= 17
                while (!(dealerHandValue >= 17))
                {
                    Card dealtCard = playingDeck.Pop();
                    dealerHand.Insert(dealerHand.Count, dealtCard);
                    dealerHandValue += dealtCard.Value;
                    Console.WriteLine($"The dealer dealt a {dealtCard.Name}, dealer total hand value is now : {dealerHandValue}. ");
                }

                // Calculate if dealer busts
                if (dealerHandValue > 21)
                {
                    Console.WriteLine("Huzzah! The dealer hand value is over 21, they bust and you've won! ");
                    Console.WriteLine(" 🍾   🍾   🍾 ");
                    Console.WriteLine("Would you like to play again? YES [y] or NO [n] ");
                    continuePlaying = (Console.ReadLine() == "y") ? true : false;
                    continue;
                }

                // Calculate winner - 
                // This also calculates ties:
                // The only way the player wins is if the playerHandValue > dealerHandValue if dealer hasn't busted
                if (playerHandValue > dealerHandValue)
                {
                    Console.WriteLine("Huzzah! Your hand wins! ");
                    Console.WriteLine(" 🍾   🍾   🍾 ");
                }
                else
                {
                    Console.WriteLine("Ouch! You've lost. Sucks for you! 😈 ");
                }

                // Ask user if they want to play again, and set the continuePlaying variable to the response
                Console.WriteLine("Would you like to play again? y/n");
                continuePlaying = (Console.ReadLine() == "y") ? true : false;
            }
        }

        public static Stack<Card> shuffleDeck(List<Card> deck)
        {
            var randomNumberGenerator = new Random();
            int leftIndex;
            Card leftCard;
            Card rightCard;

            for (int rightIndex = deck.Count - 1; rightIndex > 0; rightIndex--)
            {
                leftIndex = randomNumberGenerator.Next(rightIndex + 1);

                // Save cards in variables so we don't lose them!
                leftCard = deck[leftIndex];
                rightCard = deck[rightIndex];

                // Swap
                deck[leftIndex] = rightCard;
                deck[rightIndex] = leftCard;
            }

            return new Stack<Card>(deck);
        }

        public static List<Card> generateListDeck()
        {
            List<Card> deck = new List<Card>() { };
            var suits = new List<string>() { "Clubs", "Diamonds", "Hearts", "Spades" };
            var ranks = new List<string>() { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };
            var values = new List<int>() { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };

            for (int i = 0; i < ranks.Count; i++)
            {
                foreach (string suit in suits)
                {
                    deck.Insert(0, new Card(ranks[i] + " of " + suit, values[i]));
                }
            }

            return deck;
        }
    }

    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Card(string newName, int newValue)
        {
            Name = newName;
            Value = newValue;
        }
    }
}
