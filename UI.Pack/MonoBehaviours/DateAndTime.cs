using System;
using d4160.Core;
using Unity.Collections;
using Unity.Jobs;

namespace d4160.UI
{
    using Loops;
    using TMPro;
    using UnityEngine;

    public class DateAndTime : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI m_dateText;
        [SerializeField] protected TextMeshProUGUI m_timeText;
        public bool showDate = true;
        public bool showTime = true;
        public bool useJobSystem = false;

        private bool m_updateDateTime;

        protected void OnEnable()
        {
            UpdateLoop.OnUpdate += OnUpdate;
        }

        protected void OnDisable()
        {
            UpdateLoop.OnUpdate -= OnUpdate;
        }

        protected void OnUpdate(float dt)
        {
            if (m_updateDateTime)
            {
                if (useJobSystem)
                {
                    var job = new UpdateDateTimeJob()
                    {
                        timeCharArray = new NativeArray<char>(8, Allocator.TempJob),
                        dateCharArray = new NativeArray<char>(10, Allocator.TempJob)
                    };

                    var handle = job.Schedule();
                    handle.Complete();

                    m_dateText.text = showDate ? new string(job.dateCharArray.ToArray()) : string.Empty;
                    m_timeText.text = showTime ? new string(job.timeCharArray.ToArray()) : string.Empty;

                    job.timeCharArray.Dispose();
                    job.dateCharArray.Dispose();
                }
                else
                {
                    DateTime time = DateTime.Now;
                    var timeText = $"{time.Hour.ToZeroFormattedString()}:{time.Minute.ToZeroFormattedString()}:{time.Second.ToZeroFormattedString()}";
                    var dateText = DateTime.Now.ToString("yyyy/MM/dd");

                    m_dateText.text = dateText;
                    m_timeText.text = timeText;
                }
            }
        }

        public void SetUpdateDateTimeToTrue()
        {
            m_updateDateTime = true;
        }

        public void SetUpdateDateTimeToFalse()
        {
            m_updateDateTime = false;
        }
    }

    public struct UpdateDateTimeJob : IJob
    {
        public NativeArray<char> timeCharArray;
        public NativeArray<char> dateCharArray;

        public void Execute()
        {
            DateTime time = DateTime.Now;
            var timeText = $"{time.Hour.ToZeroFormattedString()}:{time.Minute.ToZeroFormattedString()}:{time.Second.ToZeroFormattedString()}";
            var dateText = DateTime.Now.ToString("yyyy/MM/dd");

            for (var i = 0; i < timeText.Length; i++)
            {
                timeCharArray[i] = timeText[i];
            }

            for (var i = 0; i < dateText.Length; i++)
            {
                dateCharArray[i] = dateText[i];
            }
        }
    }
}
