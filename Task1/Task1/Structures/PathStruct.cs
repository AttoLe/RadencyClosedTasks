namespace Task1.Structures;

public struct PathStruct
{
    public readonly string PathFrom, PathTo;

    public PathStruct(string[] paths) => 
        (PathFrom, PathTo) = (paths[0], paths[1]);
}