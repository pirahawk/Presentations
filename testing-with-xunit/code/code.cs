using System;

namespace MarketAnalyser
{
    public interface IMarketDecisionMaker
    {
        MarketAction MakeMarketDecision(double marketSlope);
    }

    public class MarketDecisionMaker : IMarketDecisionMaker
    {
        public MarketAction MakeMarketDecision(double marketSlope)
        {
            int trend = Math.Sign(marketSlope);

            switch (trend)
            {
                case -1:
                    return MarketAction.Buy;
                    
                case 1:
                    return MarketAction.Sell;

                case 0:
                default:
                    return MarketAction.Nothing;
            }
        }
    }

    public interface IMarketSlopeCalcualtor
    {
        double CalculateMarketSlope(MarketData previous, MarketData current);
    }

    public class MarketSlopeCalcualtor : IMarketSlopeCalcualtor
    {
        public double CalculateMarketSlope(MarketData previous, MarketData current)
        {
            double yAxisDifference = current.MarketValue - previous.MarketValue;
            double xAxisDifference = (current.TimeStamp - previous.TimeStamp).TotalMilliseconds;

            return yAxisDifference / xAxisDifference;
        }
    }

    public class PredictionAlgorithm
    {
        private readonly IMarketSlopeCalcualtor _marketSlopeCalcualtor;
        private readonly IMarketDecisionMaker _marketDecisionMaker;

        public PredictionAlgorithm(IMarketSlopeCalcualtor marketSlopeCalcualtor, IMarketDecisionMaker marketDecisionMaker)
        {
            _marketSlopeCalcualtor = marketSlopeCalcualtor;
            _marketDecisionMaker = marketDecisionMaker;
        }

        public MarketAction GetMarketDecision(MarketData previous, MarketData current)
        {
            double marketSlope = _marketSlopeCalcualtor.CalculateMarketSlope(previous, current);
            MarketAction decision = _marketDecisionMaker.MakeMarketDecision(marketSlope);
            return decision;
        }
    }

    public class MarketData
    {
        public double MarketValue { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }

    public enum MarketAction
    {
        Nothing,
        Buy,
        Sell,
    }
}



