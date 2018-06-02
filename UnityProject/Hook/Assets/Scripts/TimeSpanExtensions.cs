using System;

public static class TimeSpanExtensions
{

    public static string ToChronometerString(this TimeSpan timeSpan)
    {
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;
        int milliseconds = timeSpan.Milliseconds;
        string result = string.Format(
                                      "{0:D2}:{1:D2}:{2:D2},{3}",
                                      hours,
                                      minutes,
                                      seconds,
                                      milliseconds.ToString("D2").Substring(0, 2));
        return result;
    }

}