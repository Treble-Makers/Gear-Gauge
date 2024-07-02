using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class MusicItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    public bool HaveOne { get; set; }
    public bool WantOne { get; set; }

    public int CategoryId { get; set; }

    public int CommentId { get; set; }

    public ICollection<MusicItem>? MusicItems { get; set; }

    public MusicItem() { }

    public MusicItem(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public override string? ToString()
    {
        return Title;
    }

    public override bool Equals(object? obj)
    {
        return obj is MusicItem @musicItem && Id == @musicItem.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
