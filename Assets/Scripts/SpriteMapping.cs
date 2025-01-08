using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class SpriteMapping
{
	public static Dictionary<Spr, SpritePath> mapping = (from f in typeof(Spr).GetFields()
		where f.FieldType == typeof(Spr)
		select f).ToDictionary((FieldInfo f) => (Spr)f.GetValue(null), (FieldInfo f) => (SpritePath)f.GetCustomAttributes(typeof(SpritePath), inherit: false)[0]);

	public static Dictionary<string, Spr> strToId = mapping.ToDictionary<KeyValuePair<Spr, SpritePath>, string, Spr>((KeyValuePair<Spr, SpritePath> kv) => kv.Value.path, (KeyValuePair<Spr, SpritePath> kv) => kv.Key);
}
