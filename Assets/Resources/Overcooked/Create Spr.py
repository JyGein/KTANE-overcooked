import pathlib
import os

p = pathlib.Path('.')
pngs = list(p.glob('**/*.png'))
print(pngs)
f = open(p.absolute().parent.parent / 'Scripts' / 'Enums' / 'Spr.cs', "w")
f.write("public enum Spr\n")
f.write("{\n")
for item in pngs:
    f.write(f"	[SpritePath(\"{str(item).replace("\\", "/")}\")]\n")
    f.write(f"	{str(item).split(".")[0].replace("\\", "_")},\n")
f.write("}\n")
f.close()
os.system("pause")