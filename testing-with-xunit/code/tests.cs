using System;
using Xunit;

namespace MarketAnalyser.Test
{
    public class PredictionAlgorithmTest
    {
        [Fact]
        public void TestAlgorithm()
        {
            DateTimeOffset currentTime = DateTimeOffset.Now;
            DateTimeOffset previousTime = currentTime - TimeSpan.FromSeconds(2);

            MarketData previousTick = new MarketData
            {
                MarketValue = 10D,
                TimeStamp = previousTime
            };

            MarketData currentTick = new MarketData
            {
                MarketValue = 20D,
                TimeStamp = currentTime
            };

            PredictionAlgorithm predictionAlgorithm = new PredictionAlgorithm();

            var marketDecision = predictionAlgorithm.GetMarketDecision(previousTick, currentTick);

            Assert.Equal(MarketAction.Sell, marketDecision);
        }
    }

    
}