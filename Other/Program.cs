DateTime dateTime = new DateTime(2024, 5, 12, 19, 05, 0);


// 1. Kiểm tra chủ nhật 
DayOfWeek dayOfWeek = dateTime.DayOfWeek;
if (dayOfWeek == DayOfWeek.Sunday) { }
// Console.WriteLine(dayOfWeek);

// 2. Làm tròn giờ
DateTime RoundToNearestHalfHour(DateTime dt)
{
    int hour = dt.Hour;
    int minutes = dt.Minute;
    if (hour == 23 && minutes >= 45)
        return new DateTime(dt.Year, dt.Month, dt.Day + 1, 0, 0, 0);
    if (minutes < 15)
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
    else if (minutes >= 15 && minutes < 45)
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 30, 0);
    else
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour + 1, 0, 0);
}
// 3. Tính số giờ đi muộn
double CalculateHoursFromEight(DateTime dt)
{
    DateTime roundedTime = RoundToNearestHalfHour(dt); 
    if (roundedTime.Hour >= 8 && roundedTime.Hour < 12)
    {
        double hoursFromEight = roundedTime.Hour - 8;
        if (roundedTime.Minute == 30)
            return hoursFromEight + 0.5;
        else
            return hoursFromEight; 
    }
    else
        return 0; 
}
// 4. Tính số giờ về sớm
double CalculateHoursFromFivePM(DateTime dt)
{
    DateTime roundedTime = RoundToNearestHalfHour(dt); 

    if (roundedTime.Hour >= 13 && roundedTime.Hour < 17)
    {
        double hoursFromFivePM = 17 - roundedTime.Hour;
        if (roundedTime.Minute == 30)
            hoursFromFivePM -= 0.5;
        return hoursFromFivePM;
    }
    else
    {
        return 0; 
    }
}
double CalculateHoursFromNineteenToMidnightToday(DateTime dt)
{
    DateTime roundedTime = RoundToNearestHalfHour(dt); 

    if (roundedTime.Hour >= 17 && roundedTime.Hour < 24)
    {
        double hoursFromNineteen = roundedTime.Hour - 17;
        if (roundedTime.Minute == 30)
            hoursFromNineteen -= 0.5;

        return hoursFromNineteen;
    }
    else
    {
        return 0;
    }
}





Console.WriteLine(dateTime.Hour + ":" + dateTime.Minute);
Console.WriteLine(CalculateHoursFromEight(dateTime));
Console.WriteLine(CalculateHoursFromFivePM(dateTime));
Console.WriteLine(CalculateHoursFromNineteenToMidnightToday(dateTime));

