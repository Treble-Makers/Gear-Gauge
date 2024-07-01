using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;


namespace GearGauge.Models;

public class Music_Item
{
    public int Id {get; }
    static private int nextId = 1;
    public string? Title {get; set; }
    public CommentId commentId{get; set; }

    public CategoryId categoryId {get; set; }


}

public MusicItem()
{
    Id = nextId;
    nextId++;
}

public MusicItem()
{
    Title = title;
    CommentId = commentId;
    CategoryId = categoryId;
}

public override string ToString()
{
    string output = "";
    if(Title.Equals(""))
    {
        PageTitle = "Data not available";
    }

    output = string.Format("\nID: %d\n" + "Title: %s\n", Id, Title);
    return output;
}

public override bool Equals(object obj)
{
    return obj is MusicItem musicItem &&
    Id == musicItem.Id;
}

public override int GetHashCode()
{
    return HashCode.Combine(Id);
}