using System.ComponentModel.DataAnnotations;

namespace StarWarsBestiaryApi;

public class Creature
{
    [Key]
    public Guid CId { get; set; }
    public required string Name { get; set; }
    public required string PlanetOfOrigin { get; set; }
    public required string Description { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public Diet Diet { get; set; }

    public static Diet IntToDiet(int i) 
    {
        return i switch
        {
            0 => Diet.Herbivore,
            1 => Diet.Omnivore,
            2 => Diet.Carnivore,
            _ => Diet.Omnivore,
        };
    }

}

public enum Diet 
{
    Herbivore,
    Omnivore,
    Carnivore
}


