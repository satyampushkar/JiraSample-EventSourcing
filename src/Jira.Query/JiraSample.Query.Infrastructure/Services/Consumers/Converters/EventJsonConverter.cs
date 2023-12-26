using JiraSample.Common.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JiraSample.Query.Infrastructure.Services.Consumers.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
        }

        if (!doc.RootElement.TryGetProperty("Type", out var type))
        {
            throw new JsonException("Could not detect the Type discriminator property!");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDiscriminator switch
        {
            nameof(JiraItemCreatedEvent) => JsonSerializer.Deserialize<JiraItemCreatedEvent>(json, options),
            nameof(JiraItemUpdatedEvent) => JsonSerializer.Deserialize<JiraItemUpdatedEvent>(json, options),
            nameof(JiraItemAsigneeUpdatedEvent) => JsonSerializer.Deserialize<JiraItemAsigneeUpdatedEvent>(json, options),
            nameof(JiraItemNameUpdatedEvent) => JsonSerializer.Deserialize<JiraItemNameUpdatedEvent>(json, options),
            nameof(JiraItemDescriptionUpdatedEvent) => JsonSerializer.Deserialize<JiraItemDescriptionUpdatedEvent>(json, options),
            nameof(JiraItemStatusUpdatedEvent) => JsonSerializer.Deserialize<JiraItemStatusUpdatedEvent>(json, options),
            nameof(JiraItemTypeUpdatedEvent) => JsonSerializer.Deserialize<JiraItemTypeUpdatedEvent>(json, options),
            nameof(JiraItemParentUpdatedEvent) => JsonSerializer.Deserialize<JiraItemParentUpdatedEvent>(json, options),
            nameof(JiraItemChildrenUpdatedEvent) => JsonSerializer.Deserialize<JiraItemAsigneeUpdatedEvent>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
