using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace TimeComponet
{
    public class TimeManager
    {

        private static TimeManager mInstance;

        public static TimeManager Instance
        {
            get { return mInstance; }
        }

        // 委託事件 - 成功 
        public delegate void DelegateCB(double time);
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_10ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_50ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_100ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_200ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_500ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_1000ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_3000ms;
        // 委託物件 - 成功
        public static DelegateCB onTimerCB_5000ms;

        private float _time_10_ms;
        private float _time_50_ms;
        private float _time_100_ms;
        private float _time_200_ms;
        private float _time_500_ms;
        private float _time_1000_ms;
        private float _time_3000_ms;
        private float _time_5000_ms;

        private const float _TIME_DELAY_10_MS = 0.01f;
        private const float _TIME_DELAY_50_MS = 0.05f;
        private const float _TIME_DELAY_100_MS = 0.1f;
        private const float _TIME_DELAY_200_MS = 0.2f;
        private const float _TIME_DELAY_500_MS = 0.5f;
        private const float _TIME_DELAY_1000_MS = 1.0f;
        private const float _TIME_DELAY_3000_MS = 3.0f;
        private const float _TIME_DELAY_5000_MS = 5.0f;

        public DateTime DateTimeNow;
        private DateTime baseTime;
        private double startTimestamp;
        private double currentTimestamp;
        private double lastUpdateTimestamp;
        private Timer tmr1000;
        private Timer tmr500;

        public double GetTimestamp
        {
            get
            {
                this.baseTime = new DateTime(1970, 1, 1);
                TimeSpan t = DateTime.UtcNow - this.baseTime;
                this.startTimestamp = t.TotalMilliseconds;
                this.currentTimestamp = t.TotalMilliseconds;
                this.lastUpdateTimestamp = t.TotalMilliseconds;
                return this.currentTimestamp;
            }
        }

        public void TimeStart()
        {
            this.tmr1000 = new Timer();
            this.tmr1000.Interval = 1000;
            this.tmr1000.AutoReset = true;
            this.tmr1000.Elapsed += new ElapsedEventHandler(TimeCB1000);
            this.tmr1000.Enabled = true;

            this.tmr500 = new Timer();
            this.tmr500.Interval = 500;
            this.tmr500.AutoReset = true;
            this.tmr500.Elapsed += new ElapsedEventHandler(TimeCB500);
            this.tmr500.Enabled = true;
        }
        public void TimeStop()
        {
            this.tmr1000.Elapsed -= new ElapsedEventHandler(TimeCB1000);
            this.tmr1000.Enabled = false;

            this.tmr500.Elapsed -= new ElapsedEventHandler(TimeCB500);
            this.tmr500.Enabled = false;
        }



        void TimeCB500(object data, ElapsedEventArgs e)
        {
            if (onTimerCB_500ms != null)
            {
                onTimerCB_500ms.Invoke(GetTimestamp);
            }
        }

        void TimeCB1000(object data, ElapsedEventArgs e)
        {
            if (onTimerCB_1000ms != null)
            {
                onTimerCB_1000ms.Invoke(GetTimestamp);
            }
        }

    }
}