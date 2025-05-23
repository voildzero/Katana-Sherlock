using System.Diagnostics;

namespace Katana_Sherlock;
public class DrawLogo
{
    public ConsoleColor color = Console.ForegroundColor = ConsoleColor.Green;
    public String Logo =
        " _   __  ___ _____ ___   _   _   ___         _____ _   _  ___________ _     _____ _____  _   __" + "\n" +
        "| | / / / _ \\_   _/ _ \\ | \\ | | / _ \\       /  ___| | | ||  ___| ___ \\ |   |  _  /  __ \\| | / /" + "\n" +
        "| |/ / / /_\\ \\| |/ /_\\ \\|  \\| |/ /_\\ \\______\\ `--.| |_| || |__ | |_/ / |   | | | | /  \\/| |/ /" + "\n" +
        "|    \\ |  _  || ||  _  || . ` ||  _  |______|`--. \\  _  ||  __||    /| |   | | | | |    |    \\" + "\n" +
        "| |\\  \\| | | || || | | || |\\  || | | |      /\\__/ / | | || |___| |\\ \\| |___\\ \\_/ / \\__/\\| |\\  \\" + "\n" +
        "\\_| \\_/\\_| |_/\\_/\\_| |_/\\_| \\_/\\_| |_/      \\____/\\_| |_/\\____/\\_| \\_\\_____/\\___/ \\____/\\_| \\_/";

    public void Clear() =>
        Console.Clear();
    public void Draw() =>
        Console.WriteLine(Logo);
}