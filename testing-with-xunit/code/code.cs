using System;

namespace MarketAnalyser
{
    public class PredictionAlgorithm
    {
        public MarketAction GetMarketDecision(MarketData previous, MarketData current)
        {
            double marketSlope = CalculateMarketSlope(previous, current);
            MarketAction decision = MakeMarketDecision(marketSlope);
            return decision;
        }

        private double CalculateMarketSlope(MarketData previous, MarketData current)
        {
            double yAxisDifference = current.MarketValue - previous.MarketValue;
            double xAxisDifference = (current.TimeStamp - previous.TimeStamp).TotalMilliseconds;

            return yAxisDifference / xAxisDifference;
        }

        private MarketAction MakeMarketDecision(double marketSlope)
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