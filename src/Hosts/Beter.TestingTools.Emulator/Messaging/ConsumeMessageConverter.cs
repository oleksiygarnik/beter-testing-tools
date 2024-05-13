using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Models;
using Beter.TestingTools.Models.GlobalEvents;
using Beter.TestingTools.Models.Incidents;
using Beter.TestingTools.Models.Scoreboards;
using Beter.TestingTools.Models.TimeTableItems;
using Beter.TestingTools.Models.TradingInfos;
using Confluent.Kafka;
using Beter.TestingTools.Emulator.Messaging.Extensions;
using Microsoft.Extensions.Logging.Abstractions;
using Beter.TestingTools.Common.Serialization;

namespace Beter.TestingTools.Emulator.Messaging;

public sealed class ConsumeMessageConverter : IConsumeMessageConverter
{
    private readonly ILogger<ConsumeMessageConverter> _logger;

    private readonly static Dictionary<string, Type> KnownTypes;

    public ConsumeMessageConverter(ILogger<ConsumeMessageConverter> logger)
    {
        _logger = logger ?? NullLogger<ConsumeMessageConverter>.Instance;
    }

    static ConsumeMessageConverter()
    {
        KnownTypes = new Dictionary<string, Type>
        {
            { MessageTypes.Scoreboard.ToUpperInvariant(), typeof(ScoreBoardModel[]) },
            { MessageTypes.Trading.ToUpperInvariant(), typeof(TradingInfoModel[]) },
            { MessageTypes.Timetable.ToUpperInvariant(), typeof(TimeTableItemModel[]) },
            { MessageTypes.Incident.ToUpperInvariant(), typeof(IncidentModel[]) },
            { MessageTypes.SubscriptionsRemoved.ToUpperInvariant(), typeof(SubscriptionsRemovedModel) },
            { MessageTypes.SystemEvent.ToUpperInvariant(), typeof(GlobalMessageModel[]) },
            { MessageTypes.Heartbeat.ToUpperInvariant(), typeof(HeartbeatModel)}
        };
    }

    public bool CanProcess(ConsumeResult<byte[], byte[]> consumeResult)
    {
        var typeHeader = consumeResult.Message.Headers.FirstOrDefault(x => x.Key == HeaderNames.MessageType);
        if (typeHeader == null)
        {
            return false;
        }

        var type = typeHeader.GetValueBytes().ToUtf8String()?.ToUpperInvariant();
        return type != null && KnownTypes.TryGetValue(type, out _);
    }

    public ConsumeMessageContext ConvertToMessageContextFromConsumeResult(ConsumeResult<byte[], byte[]> consumeResult)
    {
        var headers = new Dictionary<string, string>();
        foreach (var header in consumeResult.Message.Headers)
        {
            headers[header.Key] = header.GetValueBytes().ToUtf8String()!;
        }

        var messageTypeString = headers.GetValueOrDefault(HeaderNames.MessageType, MessageTypes.Timetable);
        var messageType = KnownTypes[messageTypeString.ToUpperInvariant()];
        var messageValue = consumeResult.Message.Value.ToUtf8String();

        _logger.LogInformation($"Body: {messageValue}");

        var messageTypedInstance = JsonHubSerializer.Deserialize(messageValue, messageType);
        var messageContext = new ConsumeMessageContext
        {
            MessageHeaders = headers,
            MessageType = messageType,
            MessageObject = messageTypedInstance,
        };

        return messageContext;
    }
}
