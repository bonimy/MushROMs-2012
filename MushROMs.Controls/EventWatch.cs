﻿using System;
using System.Timers;

namespace MushROMs.Controls
{
    /// <summary>
    /// Raises an event whenever a provided amount of time has elapsed.
    /// </summary>
    public class EventWatch
    {
        private const string ErrorInterval = "Interval is less than or equal to zero.";
        private const string ErrorOffset = "offset is less than zero.";

        /// <summary>
        /// Occurs when the interval elapses.
        /// </summary>
        public event ElapsedEventHandler Elapsed;

        /// <summary>
        /// The time, in milliseconds, to wait before making the first call to the <see cref="Elapsed"/> event.
        /// </summary>
        internal double offset;
        /// <summary>
        /// The time, in milliseconds, to wait between calling the <see cref="Elapsed"/> event.
        /// </summary>
        internal double interval;
        /// <summary>
        /// The total time, in milliseconds added from all calls to the <see cref="Elapsed"/> event.
        /// </summary>
        internal double total;
        /// <summary>
        /// The number of calls made to the <see cref="Elapsed"/> event.
        /// </summary>
        internal int calls;
        /// <summary>
        /// The maximum number of calls to the <see cref="Elapsed"/> event the <see cref="EventWatch"/> class will run.
        /// Value is zero if there is no limit on the allowable number of calls.
        /// </summary>
        internal int maxCalls;

        /// <summary>
        /// Gets the time, in milliseconds, to wait before making the first
        /// call to the <see cref="Elapsed"/> event.
        /// </summary>
        public double Offset
        {
            get { return this.offset; }
        }
        /// <summary>
        /// Gets or sets the time, in milliseconds, to wait between calling the <see cref="Elapsed"/> event.
        /// </summary>
        public double Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }
        /// <summary>
        /// Gets the number of calls made to the <see cref="Elapsed"/> event.
        /// </summary>
        public int Calls
        {
            get { return this.calls; }
        }
        /// <summary>
        /// Gets or sets the maximum number of calls to the <see cref="Elapsed"/> event the <see cref="EventWatch"/> class will run.
        /// Value is zero if there is no limit on the allowable number of calls.
        /// </summary>
        public int MaxCalls
        {
            get { return this.maxCalls; }
            set { this.maxCalls = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWatch"/> class from the specified interval.
        /// </summary>
        /// <param name="interval">
        /// The time, in milliseconds, to wait between calling the <see cref="Elapsed"/> event.
        /// </param>
        /// <exception cref="ArgumentException">
        /// interval is less than or equal to zero.
        /// </exception>
        public EventWatch(double interval)
        {
            if (interval <= 0)
                throw new ArgumentException(ErrorInterval);

            this.offset = 0;
            this.interval = interval;
            this.total = 0;
            this.calls = 0;
            this.maxCalls = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWatch"/> class from the specified interval
        /// and starting offset.
        /// </summary>
        /// <param name="interval">
        /// The time, in milliseconds, to wait between calling the <see cref="Elapsed"/> event.
        /// </param>
        /// <param name="offset">
        /// The time, in milliseconds, to wait before making the first
        /// call to the <see cref="Elapsed"/> event.
        /// </param>
        /// <exception cref="ArgumentException">
        /// interval is less than or equal to zero.
        /// </exception>
        public EventWatch(double interval, double offset)
        {
            if (interval <= 0)
                throw new ArgumentException(ErrorInterval);
            if (offset < 0)
                throw new ArgumentException(ErrorOffset);

            this.offset = offset;
            this.interval = interval;
            this.total = 0;
            this.calls = 0;
            this.maxCalls = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventWatch"/> class from the specified interval,
        /// starting offset, and maximum number of calls to make to the <see cref="Elapsed"/> event.
        /// </summary>
        /// <param name="interval">
        /// The time, in milliseconds, to wait between calling the <see cref="Elapsed"/> event.
        /// </param>
        /// <param name="offset">
        /// The time, in milliseconds, to wait before making the first
        /// call to the <see cref="Elapsed"/> event.
        /// </param>
        /// <param name="maxCalls">
        /// The maximum number of calls to the <see cref="Elapsed"/> event the <see cref="EventWatch"/> class will run.
        /// Value is zero if there is no limit on the allowable number of calls.
        /// </param>
        /// <exception cref="ArgumentException">
        /// interval is less than or equal to zero.
        /// </exception>
        public EventWatch(double interval, double offset, int maxCalls)
        {
            if (interval <= 0)
                throw new ArgumentException(ErrorInterval);
            if (offset < 0)
                throw new ArgumentException(ErrorOffset);

            this.offset = offset;
            this.interval = interval;
            this.total = 0;
            this.calls = 0;
            this.maxCalls = maxCalls;
        }

        /// <summary>
        /// Resets <see cref="Calls"/> to zero.
        /// </summary>
        public void Reset()
        {
            this.calls = 0;
            this.total = 0;
        }

        /// <summary>
        /// Resets the timer with the new specified starting offset.
        /// </summary>
        /// <param name="offset">
        /// The time, in milliseconds, to wait before making the first
        /// call to the <see cref="Elapsed"/> event.
        /// </param>
        public void Reset(double offset)
        {
            this.offset = offset;
            this.calls = 0;
            this.total = 0;
        }

        /// <summary>
        /// Raises the <see cref="Elapsed"/> event from its parent <see cref="EventWatch"/>.
        /// </summary>
        /// <param name="e">
        /// A <see cref="ElapsedEventArgs"/> that contains the data.
        /// </param>
        internal void CallElapsed(ElapsedEventArgs e)
        {
            OnElapsed(e);
        }

        /// <summary>
        /// Raises the <see cref="Elapsed"/> event.
        /// </summary>
        /// <param name="e">
        /// A <see cref="ElapsedEventArgs"/> that contains the data.
        /// </param>
        protected virtual void OnElapsed(ElapsedEventArgs e)
        {
            if (Elapsed != null)
                Elapsed(this, e);
        }
    }
}