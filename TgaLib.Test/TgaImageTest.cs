using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace TgaLib.Test
{
    [TestClass]
    public class TgaImageTest
    {
        [TestMethod]
        public void TestGetBitmap()
        {
            var testcase = new[]
            {
                new { Input = "TestData/UBW8.tga", Expected = "TestData/grayscale.png" },
                new { Input = "TestData/UCM8.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/UTC16.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/UTC24.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/UTC32.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/CBW8.tga", Expected = "TestData/grayscale.png" },
                new { Input = "TestData/CCM8.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/CTC16.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/CTC24.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/CTC32.tga", Expected = "TestData/color.png" },
                new { Input = "TestData/rgb32rle.tga", Expected = "TestData/rgb32rle.png" },
            };

            testcase.ToList().ForEach((tc) =>
                {
                    using (var fs = new FileStream(tc.Input, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var r = new BinaryReader(fs))
                    {
                        var expectedImage = new BitmapImage(new Uri(tc.Expected, UriKind.Relative));
                        var tga = new TgaImage(r);
                        var actualImage = tga.GetBitmap();

                        var expectedConvertedImage = new FormatConvertedBitmap(expectedImage, PixelFormats.Bgra32, null, 0.0);
                        var bytesPerPixel = (expectedConvertedImage.Format.BitsPerPixel + 7) / 8;
                        var stride = expectedConvertedImage.PixelWidth * bytesPerPixel;
                        var expectedImageBytes = new byte[stride * expectedImage.PixelHeight];
                        expectedConvertedImage.CopyPixels(expectedImageBytes, stride, 0);

                        var actualConvertedImage = new FormatConvertedBitmap(actualImage, PixelFormats.Bgra32, null, 0.0);
                        var actualImageBytes = new byte[stride * tga.Header.Height];
                        actualConvertedImage.CopyPixels(actualImageBytes, stride, 0);

                        CollectionAssert.AreEqual(expectedImageBytes, actualImageBytes, string.Format("expected:{0}, actual:{1}", tc.Expected, tc.Input));
                    }
                });
        }
    }
}
