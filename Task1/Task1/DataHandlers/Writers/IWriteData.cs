using Task1.Structures;

namespace Task1.DataHandlers.Writers;

public interface IWriteData
{
    public void WriteLines(List<DataLine> data, string fullPathToFolder);
}
