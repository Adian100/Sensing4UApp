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
    /// Represents the classification of a sensor reading after
    /// comparing it against the lower and upper threshold bounds.
    /// </summary>
    public enum Status : byte
    {
        /// <summary>
        /// Represents a sensor value in a known good range.
        /// </summary>
        OK = 0,
        /// <summary>
        /// Represents a sensor value that is above the known good range.
        /// </summary>
        High = 1,
        /// <summary>
        /// Represents a sensor value that is below the known good range.
        /// </summary>
        Low = 2,
    }
}