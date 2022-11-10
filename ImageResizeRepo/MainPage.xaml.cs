using Dalvik.SystemInterop;
using Microsoft.Maui.Graphics.Platform;
using System.Reflection;
using IImage = Microsoft.Maui.IImage;

namespace ImageResizeRepo;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
    {
        var newFile = Path.Combine(FileSystem.CacheDirectory, "test.jpg");
        Microsoft.Maui.Graphics.IImage image;
        Assembly assembly = GetType().GetTypeInfo().Assembly;
        using (Stream stream = assembly.GetManifestResourceStream("ImageResizeRepo.Resources.Images.fbull.jpg"))
        {
            image = PlatformImage.FromStream(stream);
            var newImage = image.Resize(800, 800, ResizeMode.Bleed, false);
            using (var memStream = new MemoryStream())
            {
                newImage.Save(memStream);
                using var newStream = File.OpenWrite(newFile);
                memStream.Position = 0;
                memStream.CopyTo(newStream);
            }
        }
        


        count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

