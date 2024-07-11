public class GearInventory
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    public string ImagePath { get; set; } // Add this property
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    // Navigation property for tags
    public ICollection<Tag> Tags { get; set; }

    public GearInventory()
    {
        Tags = new List<Tag>();
    }

    public override string ToString()
    {
        return Title;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        GearInventory other = (GearInventory)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}

