using System;

[AttributeUsage(AttributeTargets.Field)]
public class SpritePath : Attribute
{
    public string pathWithExt;

    public string path;

    public SpritePath(string pathWithExt)
    {
        this.pathWithExt = pathWithExt;
        path = pathWithExt.Split('.')[0];
    }
}
