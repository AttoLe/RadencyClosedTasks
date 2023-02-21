using Task1.Structures;

namespace Task1.DataHandlers.Writers;

public abstract class WriteData
{
    protected string _PathToFolder;
    protected static int _counter;
    public WriteData(string pathToFolder)
    {
        _PathToFolder = pathToFolder;
    }
    public abstract void WriteLines(List<DataLine> data);
}
