using SQLite;

namespace lab2;

public enum SexType
{
    Male,
    Female
}

public class Person
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public SexType Sex { get; set; }
    public int BirthYear { get; set; }
    public int Age => System.DateTime.Now.Year - (Sex == SexType.Female ? (BirthYear - 5) : BirthYear);

    public Person() { } // for SQLite
    
    /// <param name="sex">0 - Male; 1 - Female</param>
    public Person(string name, string surname, int birthYear, bool sex)
    {
        Name = name;
        Surname = surname;
        BirthYear = birthYear;
        Sex = sex ? SexType.Male : SexType.Female;
    }

    public Person(string name, string surname, int birthYear, SexType sex)
        => (Name, Surname, BirthYear, Sex) = (name, surname, birthYear, sex);

    public override string ToString() => $"{Name} {Surname}, {Sex}, {Age} lat";
}