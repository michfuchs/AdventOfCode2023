namespace AdventOfCode2023.Dec07
{
    public class Solution07 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 07);

        /// <summary>
        /// Get the winnings based on hands and their bids.
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 0;
            var hands = Data07.Hands;
            foreach (var hand in hands)
            {
                var cards = hand.Cards;
                hand.Type = GetHandType(cards, partTwo: false);
                hand.Value = GetHandValue(cards, partTwo: false);
            }
            hands = hands.OrderBy(h => h.Type).ThenBy(h => h.Value).ToList();
            for (int i = 0; i < hands.Count; i++)
            {
                total += hands[i].Bid * (i + 1);
            }
            return total;
        }

        /// <summary>
        /// Get the winnings based on hands and their bids.
        /// J counts as joker now
        /// </summary>
        public long GetSolutionPartTwo()
        {
            var total = 0;
            var hands = Data07.Hands;
            foreach (var hand in hands)
            {
                var cards = hand.Cards;
                hand.Type = GetHandType(cards, partTwo: true);
                hand.Value = GetHandValue(cards, partTwo: true);
            }
            hands = hands.OrderBy(h => h.Type).ThenBy(h => h.Value).ToList();
            for (int i = 0; i < hands.Count; i++)
            {
                total += hands[i].Bid * (i + 1);
            }
            return total;
        }

        private int GetHandValue(string cards, bool partTwo)
        {
            var totalValue = 0;
            for (var i = cards.Length - 1; i >= 0; i--)
            {
                var c = cards[i];
                int cardValue;
                switch (c)
                {
                    case 'A': cardValue = 14; break;
                    case 'K': cardValue = 13; break;
                    case 'Q': cardValue = 12; break;
                    case 'J': cardValue = partTwo ? 1 : 11; break;
                    case 'T': cardValue = 10; break;
                    default: cardValue = int.Parse(c.ToString()); break;
                }
                // arbitrary value, it's all about ranking, not actual value.
                // the important thing is to make sure the first card will always contribute more than second card,
                // second more than third, and so on.
                totalValue += (int)Math.Pow(15, cards.Length - i) * cardValue;  
            }
            return totalValue;
        }

        private int GetHandType(string cards, bool partTwo)
        {
            var dictCardCount = new Dictionary<char, int>();
            foreach (var c in cards)
            {
                if (dictCardCount.ContainsKey(c))
                    dictCardCount[c]++;
                else dictCardCount.Add(c, 1);
            }

            var orderedByCount = dictCardCount.OrderByDescending(d => d.Value);
            var maxCard = orderedByCount.First();
            var nextMaxCard = dictCardCount.OrderByDescending(d => d.Value).Skip(1).FirstOrDefault();

            // Joker functionality
            if (partTwo && dictCardCount.ContainsKey('J'))
            {
                var jAdd = dictCardCount['J'];
                if (maxCard.Key != 'J') dictCardCount[maxCard.Key] = maxCard.Value + jAdd;
                else dictCardCount[nextMaxCard.Key] = nextMaxCard.Value + jAdd;

                dictCardCount.Remove('J');

                // needs to be reevaluated
                orderedByCount = dictCardCount.OrderByDescending(d => d.Value);
                maxCard = orderedByCount.First();
                nextMaxCard = dictCardCount.OrderByDescending(d => d.Value).Skip(1).FirstOrDefault();
            }

            var handType = 0;
            if (maxCard.Value == 5) handType = 7;
            else if (maxCard.Value == 4) handType = 6;
            else if (maxCard.Value == 3 && nextMaxCard.Value == 2) handType = 5;
            else if (maxCard.Value == 3) handType = 4;
            else if (maxCard.Value == 2 && nextMaxCard.Value == 2) handType = 3;
            else if (maxCard.Value == 2) handType = 2;
            else handType = 1;

            return handType;
        }
    }
}
