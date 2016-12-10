using System;
using System.Collections.Generic;
using TouristicWallet.models;
using Xamarin.Forms;
using TouristicWallet.utils;
using TouristicWallet.Data;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Globalization;

namespace TouristicWallet.View
{
    public partial class ConversionPage : ContentPage
    {
        List<Balance> balances = new List<Balance>();
        CurrencyDataAccess dataAccess = new CurrencyDataAccess();
        string convertTo = "EUR";
        double sumBalance = 0;
        double maxBalance = -1; // the highest balance value per currency

        //graphic related 
        double deltaX = 0, lastPanX = 0;
        int padding = 20;
        float colWith = 40f;
        float colGap = 20f;
        float fullWith = 0;

        public ConversionPage()
        {
            InitializeComponent();
            balances.Add(new Balance("USD", 12.0));
            balances.Add(new Balance("GBP", 23.5));
            balances.Add(new Balance("EUR", 5.75));
            balances.Add(new Balance("AED", 12.0));
            balances.Add(new Balance("BSD", 23.5));
            balances.Add(new Balance("CNY", 5.75));
            balances.Add(new Balance("GTQ", 12.0));
            balances.Add(new Balance("KWD", 23.5));
            balances.Add(new Balance("NAD", 5.75));

            fullWith = padding + 2 * balances.Count * (colWith + colGap);
            convert();
        }

        //handle scroll
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            // Handle the pan
            if (e.TotalX == 0)
            {
                lastPanX = 0;
                return;
            }
            deltaX += e.TotalX - lastPanX;
            lastPanX = e.TotalX;
            deltaX = (deltaX > 0) ? 0 : deltaX;
            float maxDelta = -(fullWith - CView.CanvasSize.Width + padding);
            deltaX = (deltaX < maxDelta) ? maxDelta : deltaX;
            CView.InvalidateSurface();
        }

        private void convert()
        {
            //get rate to destination
            string url =
                    string.Format("http://download.finance.yahoo.com/d/quotes?f=sl1d1t1&s={0}{1}=X",
                    convertTo, "EUR");
            WebService.CallWebAsync(url, updateCurrencyDestination);
            //get rates foreach balance
            foreach (var balance in balances)
            {
                url = string.Format("http://download.finance.yahoo.com/d/quotes?f=sl1d1t1&s={0}{1}=X",
                    balance.initials, "EUR");
                WebService.CallWebAsync(url, updateCurrencyBalance);
            }
        }
        private void updateCurrencyDestination(string response)
        {
            updateCurrency(response, false);
        }
        private void updateCurrencyBalance(string response)
        {
            updateCurrency(response, true);
        }

        private void updateCurrency(String response, bool updateGraph)
        {
            string initials = response.Split(',')[0].Replace("EUR=X", "").Replace("\"", "");
            string rateStr = response.Split(',')[1];
            double rate = double.Parse(rateStr, CultureInfo.InvariantCulture);
            Currency currency = dataAccess.GetCurrency(initials);
            currency.RateToEUR = rate;
            dataAccess.SaveCurrency(currency);
            if (updateGraph)
                updateGraphAndTotal(initials);
        }

        private void updateGraphAndTotal(string initials)
        {
            Currency dest = dataAccess.GetCurrency(convertTo);

            foreach (var saldo in balances)
            {
                if (saldo.initials.Equals(initials))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        saldo.amountConverted = saldo.amount * dataAccess.GetCurrency(saldo.initials).RateToEUR / dest.RateToEUR;
                        sumBalance += saldo.amountConverted;
                        maxBalance = Math.Max(maxBalance, saldo.amount);
                        maxBalance = Math.Max(maxBalance, saldo.amountConverted);
                        CView.InvalidateSurface();
                    });
                }

            }
        }


        private void OnPaintDrawing(object sender, SKPaintSurfaceEventArgs e)
        {

            float with = e.Surface.Canvas.ClipBounds.Width;
            float height = e.Surface.Canvas.ClipBounds.Height;

            SKCanvas canvas = e.Surface.Canvas;

            using (SKPaint paint = new SKPaint())
            {
                canvas.Clear(Color.White.ToSKColor());

                paint.TextAlign = SKTextAlign.Center;
                paint.TextSize = 24;
                paint.Color = Color.Blue.ToSKColor();
                string str = "Total: " + sumBalance.ToString("0.00") + " " + convertTo;
                canvas.DrawText(str, with / 2, (height / 5) / 2, paint);

                canvas.Translate((float)deltaX, 0);

                float x0, x1, y0, y1;

                x0 = padding;
                x1 = with - padding;

                y0 = height - padding;
                y1 = (height / 5) - padding;

                canvas.DrawLine(x0, y0, fullWith, y0, paint);// horizontal line
                canvas.DrawLine(x0, y0, x0, y1, paint);// vertical line

                int colNum = 0;
                float maxGraphHeight = (height - padding - y1);
                paint.TextAlign = SKTextAlign.Left;
                paint.TextSize = 16;
                foreach (var balance in balances)
                {
                    if (balance.amountConverted != -1)//already converted
                    {
                        //original amount
                        paint.Color = Color.Red.ToSKColor();
                        float x0Rect = x0 + colWith * (colNum + 1) + colNum * colGap;
                        float x1Rect = x0 + colWith * (colNum + 2) + colNum * colGap;
                        float yRectoriginal = height - padding - (float)balance.amount * maxGraphHeight / (float)maxBalance;
                        SKRect rectOriginal = new SKRect(x0Rect, yRectoriginal, x1Rect, y0);
                        canvas.DrawRect(rectOriginal, paint);
                        canvas.DrawText(balance.initials, x0Rect, height - 5, paint); // currency initials
                        canvas.DrawText(balance.amount.ToString("0.00"), x0Rect, yRectoriginal - 5, paint);
                        colNum++;

                        //converted amount
                        paint.Color = Color.Blue.ToSKColor();
                        float yRectConv = height - padding - (float)balance.amountConverted * maxGraphHeight / (float)maxBalance;
                        SKRect rectConverted = new SKRect(x1Rect, yRectConv, x1Rect + colWith, y0);
                        canvas.DrawRect(rectConverted, paint);
                        canvas.DrawText(balance.amountConverted.ToString("0.00"), x1Rect, yRectConv - 5, paint);
                        colNum++;
                    }
                }
            }
        }
    }

    public class Balance
    {
        public string initials { get; set; }
        public double amount { get; set; }
        public double amountConverted { get; set; }
        public Balance(string initials, double amount)
        {
            this.amount = amount;
            this.initials = initials;
            this.amountConverted = -1;
        }
    }
}
