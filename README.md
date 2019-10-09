# TgaLib
--------
TgaLib is a library to decode TARGA image format.

Following features are support:
- RLE and raw TARGA images with 8/15/16/24/32 bits per pixel,
  monochrome, truecolor and colormapped images.
- Image origins, attribute type in extensions area.

## Example
----------
```C#
using (var fs = new System.IO.FileStream("test.tga", FileMode.Open, FileAccess.Read, FileShare.Read))
using (var reader = new System.IO.BinaryReader(fs))
{
    var tga = new TgaLib.TgaImage(reader);
    System.Windows.Media.Imaging.BitmapSource source = tga.GetBitmap();
}
```
