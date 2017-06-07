using System;
using Xunit;

namespace MarketAnalyser.Test
{
    public class PredictionAlgorithmTest
    {
        [Fact]
        public void TestAlgorithm()
        {
            var currentTick = new MarketDataFixture { MarketValue = 20D }.Build();
            var previousTick = new MarketDataFixture{ MarketValue = 10D }
            .WithTimeOffset(currentTick, TimeSpan.FromSeconds(-2))
            .Build();


            PredictionAlgorithm predictionAlgorithm = new PredictionAlgorithm();

            var marketDecision = predictionAlgorithm.GetMarketDecision(previousTick, currentTick);

            Assert.Equal(MarketAction.Sell, marketDecision);
        }
    }

    public class MarketDataFixture
    {
        public MarketDataFixture()
        {
            MarketValue = 10D;
            Timestamp = DateTimeOffset.Now;
        }

        public MarketData Build()
        {
            return new MarketData
            {
                MarketValue = MarketValue,
                TimeStamp = Timestamp
            };
        }

        public MarketDataFixture WithTimeOffset(MarketData marketData, TimeSpan timeSpanToOffset)
        {
            Timestamp = marketData.TimeStamp + timeSpanToOffset;
            return this;
        }

        public DateTimeOffset Timestamp { get; set; }

        public double MarketValue { get; set; }
    }


}