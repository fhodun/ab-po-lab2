namespace lab2;

public enum SexType
{
    Male,
    Female
}

public class Person
{
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string Surname { get; set; }
    private SexType Sex { get; set; }
    public int BirthYear { get; private set; }
    private int Age => new DateTime().Year - (Sex == SexType.Female ? (BirthYear - 5) : BirthYear);

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