namespace lab2;

public partial class App : Application
{
    static Database _database;

    public static Database Database
    {
        get
        {
            if (_database == null)
            {
                string dbPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "lab2.sqlite");
                _database = new Database(dbPath);
            }

            return _database;
        }
    }

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}