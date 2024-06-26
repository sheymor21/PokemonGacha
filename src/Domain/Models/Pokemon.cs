﻿using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models;

public class Pokemon
{
    [BsonElement("name")] public string Name { get; set; } = string.Empty;
    [BsonElement("type")] public List<string> Type { get; set; } = new();
}