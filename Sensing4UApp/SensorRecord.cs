// Name & Student ID: Adian Dzananovic, 30067523
// Task: AT1 - MVP that manages sensors for Sensing4U
// Date: 02/11/2025

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sensing4UApp
{
    /// <summary>
    /// Represents a single sensor data point stored in the 2D dataset grid.
    /// </summary>
    public sealed class SensorRecord
    {
        /// <summary>
        /// Unique identifier for the sensor record.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Timestamp indicating when the sensor reading was taken.
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Label identifying this reading (used for binary search and display).
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// The numeric value recorded by the sensor.
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Indicates whether the reading is OK, High, or Low according to the thresholds.
        /// </summary>
        public Status CurrentStatus { get; set; }
    }
}