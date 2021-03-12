/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.SkiaSharp;
using SkiaSharp;
using SkiaSharp.Views.Tizen;
using Tizen.Theme.Common;
using Tizen.Theme.Common.Renderer;

using NLayoutEventArgs = Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs;

[assembly: ExportRenderer(typeof(ShadowFrame), typeof(ShadowFrameRenderer))]

namespace Tizen.Theme.Common.Renderer
{
    public class ShadowFrameRenderer : LayoutRenderer
    {
        static SKColor s_defaultColor = SKColors.Transparent;

        SKClipperView _clipper;
        SKCanvasView _shadowCanvasView;
        ShadowFrame ShadowElement => Element as ShadowFrame;

        public ShadowFrameRenderer()
        {
            if (!Forms.UseSkiaSharp)
                throw new InvalidOperationException("You must set Forms.UseSkiaSharp to true prior to using ShadowFrame.");

            RegisterPropertyHandler(Frame.BorderColorProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.BorderWidthProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.CornerRadiusProperty, UpdateCanvas);
            RegisterPropertyHandler(Frame.HasShadowProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.ShadowBlurRadiusProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.ShadowColorProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.ShadowOffsetXProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.ShadowOffsetYProperty, UpdateCanvas);
            RegisterPropertyHandler(ShadowFrame.ShadowOpacityProperty, UpdateCanvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);
            _clipper = new SKClipperView(Forms.NativeParent);
            _clipper.Show();
            _clipper.PassEvents = true;
            _clipper.PaintSurface += OnCliperPaint;
            Control.Children.Add(_clipper);
            BackgroundCanvas?.StackAbove(_clipper);

            _shadowCanvasView = new SKCanvasView(Forms.NativeParent);
            _shadowCanvasView.Show();
            _shadowCanvasView.PassEvents = true;
            _shadowCanvasView.PaintSurface += OnShadowPaint;
            Control.Children.Add(_shadowCanvasView);
            _shadowCanvasView.Lower();
            _shadowCanvasView.SetClip(null);
        }

        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (initialize && Element.BackgroundColor.IsDefault)
                return;
            else
                BackgroundCanvas?.Invalidate();
        }

        protected override void OnBackgroundLayoutUpdated(object sender, NLayoutEventArgs e)
        {
            base.OnBackgroundLayoutUpdated(sender, e);

            if (_clipper != null)
            {
                _clipper.Geometry = Control.Geometry;
                _clipper.Invalidate();
            }

            if (_shadowCanvasView != null && ShadowElement.HasShadow)
            {
                UpdateShadowGeometry();
                _shadowCanvasView.Invalidate();
            }
        }

        protected override void OnBackgroundPaint(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var bound = e.Info.Rect;
            canvas.Clear();
            var bgColor = Element.BackgroundColor == Color.Default ? s_defaultColor : SKColor.Parse(Element.BackgroundColor.ToHex());
            var borderColor = ShadowElement.BorderColor == Color.Default ? s_defaultColor : SKColor.Parse(ShadowElement.BorderColor.ToHex());

            using (var paint = new SKPaint
            {
                IsAntialias = true,
            })
            {
                using (var path = CreateRoundRectPath(bound.Width, bound.Height))
                {
                    // Draw background color
                    paint.ImageFilter = null;
                    paint.Style = SKPaintStyle.Fill;
                    paint.Color = bgColor;
                    canvas.DrawPath(path, paint);

                    // Draw Background (Brush)
                    using (var brushPaint = Element.GetBackgroundPaint(bound))
                    {
                        if (brushPaint != null)
                            canvas.DrawPath(path, brushPaint);
                    }

                    // Draw border
                    paint.IsAntialias = true;
                    paint.Style = SKPaintStyle.Stroke;
                    paint.StrokeWidth = Forms.ConvertToScaledPixel(ShadowElement.BorderWidth);
                    paint.Color = borderColor;
                    canvas.DrawPath(path, paint);
                }
            }
        }

        protected virtual void OnShadowPaint(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var bound = e.Info.Rect;
            canvas.Clear();

            // Draw shadow
            if (ShadowElement.HasShadow)
            {
                using (var paint = new SKPaint
                {
                    IsAntialias = true,
                    Style = SKPaintStyle.StrokeAndFill
                })
                {
                    using (var path = CreateShadowPath())
                    {
                        var scaledOffsetX = Forms.ConvertToScaledPixel(ShadowElement.ShadowOffsetX);
                        var scaledOffsetY = Forms.ConvertToScaledPixel(ShadowElement.ShadowOffsetY);
                        var scaledBlurRadius = Forms.ConvertToScaledPixel(ShadowElement.ShadowBlurRadius);

                        canvas.Save();
                        canvas.ClipPath(path, SKClipOperation.Difference, true);
                        paint.ImageFilter = SKImageFilter.CreateDropShadowOnly(
                            scaledOffsetX,
                            scaledOffsetY,
                            scaledBlurRadius,
                            scaledBlurRadius,
                            ShadowElement.ShadowColor.MultiplyAlpha(ShadowElement.ShadowOpacity).ToSK());
                        canvas.DrawPath(path, paint);
                        canvas.Restore();

                        canvas.Save();
                        canvas.ClipPath(path, SKClipOperation.Intersect, true);
                        canvas.DrawPath(path, paint);
                        canvas.Restore();
                    }
                }
            }
        }

        protected virtual void OnCliperPaint(object sender, SKPaintSurfaceEventArgs e)
        {
            if (ShadowElement.Content == null)
                return;

            var canvas = e.Surface.Canvas;
            var bound = e.Info.Rect;
            canvas.Clear();

            // clipping
            using (var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
            })
            {
                using (var path = CreateRoundRectPath(bound.Width, bound.Height))
                {
                    canvas.DrawPath(path, paint);
                }
            }
            ShadowElement.Content.SetClipperCanvas(_clipper);
        }

        void UpdateCanvas()
        {
            BackgroundCanvas?.Invalidate();
            _clipper?.Invalidate();
            if (ShadowElement.HasShadow)
            {
                UpdateShadowGeometry();
            }
            else
            {
                _shadowCanvasView?.Invalidate();
            }
        }

        SKPath CreateRoundRectPath(float width, float height)
        {
            var path = new SKPath();
            var topLeft = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.TopLeft);
            var topRight = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.TopRight);
            var bottomLeft = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.BottomLeft);
            var bottomRight = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.BottomRight);
            var padding = Convert.ToSingle(ShadowElement.BorderWidth);
            var diameter = padding * 2;
            width = width > diameter ? width - diameter : 0;
            height = height > diameter ? height - diameter : 0;
            var startX = topLeft + padding;
            var startY = padding;

            path.MoveTo(startX, startY);
            path.LineTo(width - topRight + padding, startY);
            path.ArcTo(topRight, new SKPoint(width + padding, topRight + padding));
            path.LineTo(width + padding, height - bottomRight + padding);
            path.ArcTo(bottomRight, new SKPoint(width - bottomRight + padding, height + padding));
            path.LineTo(bottomLeft + padding, height + padding);
            path.ArcTo(bottomLeft, new SKPoint(padding, height - bottomLeft + padding));
            path.LineTo(padding, topLeft + padding);
            path.ArcTo(topLeft, new SKPoint(startX, startY));
            path.Close();
            return path;
        }

        SKPath CreateShadowPath()
        {
            var geometry = NativeView.Geometry;
            if (ShadowElement.Content != null)
            {
                var contentNativeView = Platform.GetOrCreateRenderer(ShadowElement.Content)?.NativeView;
                if (contentNativeView != null)
                {
                    geometry = contentNativeView.Geometry;
                }
            }

            var path = new SKPath();
            var left = geometry.Left - _shadowCanvasView.Geometry.Left;
            var top = geometry.Top - _shadowCanvasView.Geometry.Top;
            var rect = new SKRect(left, top, left + geometry.Width, top + geometry.Height);
            var scaledTLRadius = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.TopLeft) * 2;
            var scaledTRRadius = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.TopRight) * 2;
            var scaledBLRadius = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.BottomLeft) * 2;
            var scaledBRRadius = Forms.ConvertToScaledPixel(ShadowElement.CornerRadius.BottomRight) * 2;
            var topLeft = new SKRect(rect.Left, rect.Top, rect.Left + scaledTLRadius, rect.Top + scaledTLRadius);
            var topRight = new SKRect(rect.Right - scaledTRRadius, rect.Top, rect.Right, rect.Top + scaledTRRadius);
            var bottomLeft = new SKRect(rect.Left, rect.Bottom - scaledBLRadius, rect.Left + scaledBLRadius, rect.Bottom);
            var bottomRight = new SKRect(rect.Right - scaledBRRadius, rect.Bottom - scaledBRRadius, rect.Right, rect.Bottom);
            path.ArcTo(topLeft, 180, 90, false);
            path.ArcTo(topRight, 270, 90, false);
            path.ArcTo(bottomRight, 0, 90, false);
            path.ArcTo(bottomLeft, 90, 90, false);
            path.Close();
            return path;
        }

        void UpdateShadowGeometry()
        {
            var geometry = NativeView.Geometry;
            if (ShadowElement.Content != null)
            {
                var contentNativeView = Platform.GetOrCreateRenderer(ShadowElement.Content)?.NativeView;
                if (contentNativeView != null)
                {
                    geometry = contentNativeView.Geometry;
                }
            }
            double left = 0;
            double top = 0;
            double right = 0;
            double bottom = 0;
            var scaledOffsetX = Forms.ConvertToScaledPixel(ShadowElement.ShadowOffsetX);
            var scaledOffsetY = Forms.ConvertToScaledPixel(ShadowElement.ShadowOffsetY);
            var scaledBlurRadius = Forms.ConvertToScaledPixel(ShadowElement.ShadowBlurRadius);
            var spreadSize = scaledBlurRadius * 3;
            var sl = scaledOffsetX - spreadSize;
            var sr = scaledOffsetX + spreadSize;
            var st = scaledOffsetY - spreadSize;
            var sb = scaledOffsetY + spreadSize;
            if (left > sl) left = sl;
            if (top > st) top = st;
            if (right < sr) right = sr;
            if (bottom < sb) bottom = sb;

            var canvasGeometry = new ElmSharp.Rect(
                geometry.X + (int)left,
                geometry.Y + (int)top,
                geometry.Width + (int)right - (int)left,
                geometry.Height + (int)bottom - (int)top);
            if (_shadowCanvasView != null)
            {
                _shadowCanvasView.Geometry = canvasGeometry;
                _shadowCanvasView.Invalidate();
            }
        }
    }

    internal static class SkExtensions
    {
        internal static SKPath ArcTo(this SKPath path, float radius, SKPoint finalPoint)
        {
            path.ArcTo(new SKPoint(radius, radius), 0, SKPathArcSize.Small, SKPathDirection.Clockwise, finalPoint);
            return path;
        }
        internal static SKColor ToSK(this Color color)
        {
            return new SKColor((byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255), (byte)(color.A * 255));
        }
    }
}
