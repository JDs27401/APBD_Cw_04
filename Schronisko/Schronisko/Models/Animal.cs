namespace Schronisko.Models;

public class Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Breed { get; set; }
    public string Color { get; set; }
    public double Weight { get; set; }
    
    public List<Visit> Visits { get; set; }
}