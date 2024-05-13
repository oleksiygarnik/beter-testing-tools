﻿using Beter.TestingTools.Common.Constants;
using Beter.TestingTools.Models;
using System.Text.Json.Nodes;
using Beter.TestingTools.Common.Extensions;

namespace Beter.TestingTools.Generator.Application.Services.Playbacks.Transformations.Helpers;

public record FeedMessageWrapper : IFeedMessage, IIdentityModel
{
    private readonly JsonNode _message;

    public FeedMessageWrapper(JsonNode message)
    {
        _message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public string Id
    {
        get => _message.GetValue<string>(MessageProperties.Id);
        set => _message.SetValue(MessageProperties.Id, value);
    }

    public int MsgType
    {
        get => _message.GetValue<int>(MessageProperties.MsgType);
        set => _message.SetValue(MessageProperties.MsgType, value);
    }

    public long Offset
    {
        get => _message.GetValue<long>(MessageProperties.Offset);
        set => _message.SetValue(MessageProperties.Offset, value);
    }

    public int? SportId
    {
        get => _message.GetValue<int?>(MessageProperties.SportId);
        set => _message.SetValue(MessageProperties.SportId, value);
    }
}
