using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;
using Beter.TestingTools.Models.Incidents;
using Beter.TestingTools.Models.Scoreboards;
using Beter.TestingTools.Models.TimeTableItems;
using Beter.TestingTools.Models.TradingInfos;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using System.Text.Json;

namespace Beter.TestingTools.Consumer.Services
{
    public class TestScenarioMessagesComparer
    {
        private static readonly CompareLogic Comparer = GetComparer();

        public static bool Compare<TValue>(TValue expected, TValue actual) where TValue : class
        {
            return Comparer.Compare(expected, actual).AreEqual;
        }

        private static CompareLogic GetComparer()
        {
            var compare = new CompareLogic();

            compare.Config.IgnoreProperty<SubscriptionsRemovedModel>(x => x.Ids);

            compare.Config.IgnoreProperty<IncidentModel>(x => x.Id);
            compare.Config.IgnoreProperty<IncidentModel>(x => x.Date);
            compare.Config.IgnoreProperty<IncidentModel>(x => x.Offset);

            compare.Config.IgnoreProperty<ScoreBoardModel>(x => x.Id);
            compare.Config.IgnoreProperty<ScoreBoardModel>(x => x.Timestamp);
            compare.Config.IgnoreProperty<ScoreBoardModel>(x => x.Offset);
            compare.Config.IgnoreProperty<TimerModel>(x => x.TimeStamp);

            compare.Config.IgnoreProperty<TradingInfoModel>(x => x.Id);
            compare.Config.IgnoreProperty<TradingInfoModel>(x => x.Timestamp);
            compare.Config.IgnoreProperty<TradingInfoModel>(x => x.Offset);
            compare.Config.CustomPropertyComparer<OutcomeModel>(
                x => x.Prices,
                new CustomComparer<Dictionary<string, object>, Dictionary<string, object>>(
                    (item1, item2) => JsonSerializer.Serialize(item1).Equals(JsonSerializer.Serialize(item2))));

            compare.Config.IgnoreProperty<TimeTableItemModel>(x => x.Id);
            compare.Config.IgnoreProperty<TimeTableItemModel>(x => x.Timestamp);
            compare.Config.IgnoreProperty<TimeTableItemModel>(x => x.StartDate);
            compare.Config.IgnoreProperty<TimeTableItemModel>(x => x.Offset);

            compare.Config.IgnoreProperty<GlobalMessageModel>(x => x.Id);

            return compare;
        }
    }
}
