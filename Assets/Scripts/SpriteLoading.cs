using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;

public static class SpriteLoading
{
    public static Dictionary<Spr, Texture> textures = new Dictionary<Spr, Texture>();
    public static HashSet<Spr> missingTextures = new HashSet<Spr>();
    public static Texture GetTexture(this Spr id, Overcooked module)
    {
        Texture value;
        if (textures.TryGetValue(id, out value)) return value;
        if (missingTextures.Contains(id)) return null;
        Texture texture2D = LoadTexture(GetFullPath(SpriteMapping.mapping[id].path));
        if (texture2D != null) return textures[id] = texture2D;
        if(missingTextures.Add(id))
        {
            module.log("Missing texture: " + id);
        }
        return null;
    }
    public static Texture LoadTexture(string path)
    {
        return Resources.Load(path) as Texture;
    }
    public static string GetFullPath(string path) { return "Overcooked/" + path; }
}
