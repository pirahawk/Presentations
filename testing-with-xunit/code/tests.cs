using System;
using System.Collections.Generic;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace MarketAnalyser.Test
{
    public class PredictionAlgorithmTest
    {
        [Theory]
        [MemberData(nameof(SplitCountData))]
        public void TestAlgorithm(MarketData previousTick, MarketData currentTick, MarketAction expectedAction)
        {
            PredictionAlgorithm predictionAlgorithm = new PredictionAlgorithmFixture().Build();

            var marketDecision = predictionAlgorithm.GetMarketDecision(previousTick, currentTick);

            Assert.Equal(expectedAction, marketDecision);
        }

        [Fact]
        public void EnsurePredictionAlgorithmUsesCalculatorAsExpected()
        {            
            var mockCalculator = new Mock<IMarketSlopeCalcualtor>();
            mockCalculator.Setup(m => m.CalculateMarketSlope(It.IsAny<MarketData>(), It.IsAny<MarketData>())).Returns(1);

            var previous = new MarketDataFixture().Build();
            var current = new MarketDataFixture().Build();

            var predictionAlgorithm = new PredictionAlgorithmFixture
            {
                MarketSlopeCalcualtor = mockCalculator.Object
            }.Build();

            predictionAlgorithm.GetMarketDecision(previous, current);

            mockCalculator.Verify(m => m.CalculateMarketSlope(previous, current), Times.Once);
        }

        public static IEnumerable<object[]> SplitCountData
        {
            get
            {
                var currentTickSell = new MarketDataFixture { MarketValue = 20D }.Build();
                var previousTickSell = new MarketDataFixture { MarketValue = 10D }
                    .WithTimeOffset(currentTickSell, TimeSpan.FromSeconds(-2))
                    .Build();

                yield return new object[] { previousTickSell , currentTickSell , MarketAction.Sell};

                var currentTickBuy = new MarketDataFixture { MarketValue = 10D }.Build();
                var previousTickBuy = new MarketDataFixture { MarketValue = 20D }
                    .WithTimeOffset(currentTickBuy, TimeSpan.FromSeconds(-2))
                    .Build();

                yield return new object[] { previousTickBuy, currentTickBuy, MarketAction.Buy };

                var currentTickNoMovement = new MarketDataFixture { MarketValue = 10D }.Build();
                var previousTickNoMovement = new MarketDataFixture { MarketValue = 10D }
                    .WithTimeOffset(currentTickBuy, TimeSpan.FromSeconds(-2))
                    .Build();

                yield return new object[] { previousTickNoMovement, currentTickNoMovement, MarketAction.Nothing };
            }
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


    public class PredictionAlgorithmFixture
    {
        public PredictionAlgorithmFixture()
        {
            MarketSlopeCalcualtor = new MarketSlopeCalcualtor();
            MarketDecisionMaker = new MarketDecisionMaker();
        }

        public PredictionAlgorithm Build()
        {
            return new PredictionAlgorithm(MarketSlopeCalcualtor,MarketDecisionMaker);
        }

        public IMarketDecisionMaker MarketDecisionMaker { get; set; }

        public IMarketSlopeCalcualtor MarketSlopeCalcualtor { get; set; }
    }

}