using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Falures
{
    public class ReportMaker
    {
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceIds,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            if (failureTypes.Length != deviceIds.Length || deviceIds.Length != times.Length)
            {
                throw new ArgumentException("Input arrays must have the same length.");
            }

            var date = new DateTime(year, month, day);
            var failures = new List<Failure>();

            for (var i = 0; i < failureTypes.Length; i++)
            {
                if (times[i].Length != 3 ||
                    !int.TryParse(times[i][0]?.ToString(), out int dayTime) ||
                    !int.TryParse(times[i][1]?.ToString(), out int monthTime) ||
                    !int.TryParse(times[i][2]?.ToString(), out int yearTime))
                {
                    throw new ArgumentException("Invalid time data.");
                }

                var failure = new Failure
                {
                    DeviceID = deviceIds[i],
                    Time = new DateTime(yearTime, monthTime, dayTime),
                    Type = (FailureType)failureTypes[i]
                };
                failures.Add(failure);
            }

            var devicesList = devices.Select(device => new Device
            {
                Name = device["Name"] as string,
                DeviceID = (int)device["DeviceId"]
            }).ToList();

            return FindDevicesFailedBeforeDate(date, failures, devicesList)
            .Select(device => device.Name)
            .ToList();
        }

        private static List<Device> FindDevicesFailedBeforeDate(
            DateTime date,
            List<Failure> failures,
            List<Device> devices)
        {
            var problematicDevices = new HashSet<int>();

            foreach (var failure in failures)
            {
                if (failure.IsSerious() && failure.Time < date)
                {
                    problematicDevices.Add(failure.DeviceID);
                }
            }

            return devices.Where(device => problematicDevices.Contains(device.DeviceID)).ToList();
        }
    }
}
