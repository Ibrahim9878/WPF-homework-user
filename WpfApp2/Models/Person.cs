namespace WpfApp2.Models;

public class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public bool? HasStep { get; set; }
    public string City { get; set; }
    public override string ToString()
    {
        return $"{Name} -- {Surname}";
    }
}
