namespace lab2;

public partial class App : Application
{
    static Database? _database;

    public static Database Database
    {
        get
        {
            if (_database == null)
            {
                _database = new Database(Constants.DatabasePath);
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