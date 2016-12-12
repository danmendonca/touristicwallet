using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace TouristicWallet.Views
{
    public partial class ConvertPage : ContentPage
    {
        public ConvertPage(string convertToStr)
        {
            InitializeComponent();
            convertViewModel.ConvertTo = convertToStr;
            convertViewModel.Canvas = CView;
            convertViewModel.update();
        }
        private void OnPaintDrawing(object sender, SKPaintSurfaceEventArgs e)
        {
            convertViewModel.OnPaintDrawing(sender, e);
        }
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            convertViewModel.OnPanUpdated(sender, e);
        }
    }
}
