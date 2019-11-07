﻿// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Windows.Controls.DataVisualization.Charting
{
    /// <summary>
    /// Represents a control that contains a data series to be rendered in X/Y scatter format.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [StyleTypedProperty(Property = "DataPointStyle", StyleTargetType = typeof(ScatterDataPoint))]
    [StyleTypedProperty(Property = "LegendItemStyle", StyleTargetType = typeof(LegendItem))]
    public sealed partial class ScatterSeries : DataPointSingleSeriesWithAxes
    {
        /// <summary>
        /// Gets or sets the height of the marker objects that follow the data 
        /// points.
        /// </summary>
        public double MarkerHeight
        {
            get { return (double)GetValue(MarkerHeightProperty); }
            set { SetValue(MarkerHeightProperty, value); }
        }

        /// <summary>
        /// Identifies the MarkerHeight dependency property.
        /// </summary>
        public static readonly DependencyProperty MarkerHeightProperty =
            DependencyProperty.Register(
                "MarkerHeight",
                typeof(double),
                typeof(ScatterSeries),
                new PropertyMetadata(double.NaN));

        /// <summary>
        /// Gets or sets the width of the marker objects that follow the data 
        /// points.
        /// </summary>
        public double MarkerWidth
        {
            get { return (double)GetValue(MarkerWidthProperty); }
            set { SetValue(MarkerWidthProperty, value); }
        }

        /// <summary>
        /// Identifies the MarkerWidth dependency property.
        /// </summary>
        public static readonly DependencyProperty MarkerWidthProperty =
            DependencyProperty.Register(
                "MarkerWidth",
                typeof(double),
                typeof(ScatterSeries),
                new PropertyMetadata(double.NaN));
        
        /// <summary>
        /// Initializes a new instance of the ScatterSeries class.
        /// </summary>
        public ScatterSeries()
        {
            this.DefaultStyleKey = typeof(ScatterSeries);
        }

        /// <summary>
        /// Gets the dependent axis as a range axis.
        /// </summary>
        public IRangeAxis ActualDependentRangeAxis { get { return this.InternalActualDependentAxis as IRangeAxis; } }

        #region public IRangeAxis DependentRangeAxis
        /// <summary>
        /// Gets or sets the dependent range axis.
        /// </summary>
        public IRangeAxis DependentRangeAxis
        {
            get { return GetValue(DependentRangeAxisProperty) as IRangeAxis; }
            set { SetValue(DependentRangeAxisProperty, value); }
        }

        /// <summary>
        /// Identifies the DependentRangeAxis dependency property.
        /// </summary>
        public static readonly DependencyProperty DependentRangeAxisProperty =
            DependencyProperty.Register(
                "DependentRangeAxis",
                typeof(IRangeAxis),
                typeof(ScatterSeries),
                new PropertyMetadata(null, OnDependentRangeAxisPropertyChanged));

        /// <summary>
        /// DependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="d">ScatterSeries that changed its DependentRangeAxis.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnDependentRangeAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScatterSeries source = (ScatterSeries)d;
            IRangeAxis newValue = (IRangeAxis)e.NewValue;
            source.OnDependentRangeAxisPropertyChanged(newValue);
        }

        /// <summary>
        /// DependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="newValue">New value.</param>        
        private void OnDependentRangeAxisPropertyChanged(IRangeAxis newValue)
        {
            this.InternalDependentAxis = (IAxis)newValue;
        }
        #endregion public IRangeAxis DependentRangeAxis

        /// <summary>
        /// Gets the independent axis as a range axis.
        /// </summary>
        public IRangeAxis ActualIndependentRangeAxis { get { return this.InternalActualIndependentAxis as IRangeAxis; } }

        #region public IRangeAxis IndependentRangeAxis
        /// <summary>
        /// Gets or sets the independent range axis.
        /// </summary>
        public IRangeAxis IndependentRangeAxis
        {
            get { return GetValue(IndependentRangeAxisProperty) as IRangeAxis; }
            set { SetValue(IndependentRangeAxisProperty, value); }
        }

        /// <summary>
        /// Identifies the IndependentRangeAxis dependency property.
        /// </summary>
        public static readonly DependencyProperty IndependentRangeAxisProperty =
            DependencyProperty.Register(
                "IndependentRangeAxis",
                typeof(IRangeAxis),
                typeof(ScatterSeries),
                new PropertyMetadata(null, OnIndependentRangeAxisPropertyChanged));

        /// <summary>
        /// IndependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="d">ScatterSeries that changed its IndependentRangeAxis.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnIndependentRangeAxisPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScatterSeries source = (ScatterSeries)d;
            IRangeAxis newValue = (IRangeAxis)e.NewValue;
            source.OnIndependentRangeAxisPropertyChanged(newValue);
        }

        /// <summary>
        /// IndependentRangeAxisProperty property changed handler.
        /// </summary>
        /// <param name="newValue">New value.</param>        
        private void OnIndependentRangeAxisPropertyChanged(IRangeAxis newValue)
        {
            this.InternalIndependentAxis = (IAxis)newValue;
        }
        #endregion public IRangeAxis IndependentRangeAxis

        /// <summary>
        /// Acquire a horizontal linear axis and a vertical linear axis.
        /// </summary>
        /// <param name="firstDataPoint">The first data point.</param>
        protected override void GetAxes(DataPoint firstDataPoint)
        {
            GetRangeAxis(
                InternalIndependentAxis,
                firstDataPoint,
                AxisOrientation.Horizontal,
                () => CreateRangeAxisFromData(firstDataPoint.IndependentValue),
                () => InternalActualIndependentAxis as IRangeAxis,
                (value) => { InternalActualIndependentAxis = (IAxis)value; },
                (dataPoint) => dataPoint.IndependentValue);

            GetRangeAxis(
                InternalDependentAxis,
                firstDataPoint,
                AxisOrientation.Vertical,
                () =>
                {
                    HybridAxis axis = (HybridAxis) CreateRangeAxisFromData(firstDataPoint.DependentValue);
                    axis.ShowGridLines = true;
                    return (IRangeAxis) axis;
                },
                () => InternalActualDependentAxis as IRangeAxis,
                (value) => { InternalActualDependentAxis = (IAxis)value; },
                (dataPoint) => dataPoint.DependentValue);
        }

        /// <summary>
        /// Creates a new scatter data point.
        /// </summary>
        /// <returns>A scatter data point.</returns>
        protected override DataPoint CreateDataPoint()
        {
            return new ScatterDataPoint();
        }

        /// <summary>
        /// Returns the style to use for all data points.
        /// </summary>
        /// <returns>The style to use for all data points.</returns>
        protected override Style GetDataPointStyleFromHost()
        {
            return SeriesHost.NextStyle(typeof(ScatterDataPoint), true);
        }

        /// <summary>
        /// This method updates a single data point.
        /// </summary>
        /// <param name="dataPoint">The data point to update.</param>
        protected override void UpdateDataPoint(DataPoint dataPoint)
        {
            double PlotAreaHeight = ActualDependentRangeAxis.GetPlotAreaCoordinate(ActualDependentRangeAxis.Range.Maximum);
            double dataPointX = ActualIndependentRangeAxis.GetPlotAreaCoordinate(ValueHelper.ToComparable(dataPoint.ActualIndependentValue));
            double dataPointY = ActualDependentRangeAxis.GetPlotAreaCoordinate(ValueHelper.ToDouble(dataPoint.ActualDependentValue));

            if (ValueHelper.CanGraph(dataPointX) && ValueHelper.CanGraph(dataPointY))
            {
                // Set dimensions
                if (!double.IsNaN(MarkerHeight))
                {
                    dataPoint.Height = MarkerHeight;
                }

                if (!double.IsNaN(MarkerWidth))
                {
                    dataPoint.Width = MarkerWidth;
                }

                // Call UpdateLayout to ensure ActualWidth/ActualHeight are correct
                if (dataPoint.ActualWidth == 0.0 || dataPoint.ActualHeight == 0.0)
                {
                    dataPoint.UpdateLayout();
                }

                // Set the Position
                Canvas.SetLeft(
                    dataPoint,
                    Math.Round(dataPointX - (dataPoint.ActualWidth / 2)));
                Canvas.SetTop(
                    dataPoint,
                    Math.Round(PlotAreaHeight - (dataPointY + (dataPoint.ActualHeight / 2))));
            }
        }
    }
}
