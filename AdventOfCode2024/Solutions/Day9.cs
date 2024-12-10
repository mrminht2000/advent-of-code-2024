using Newtonsoft.Json;

public class Day9
{
    private static long GetCheckSum(string? inputStr)
    {
        if (string.IsNullOrEmpty(inputStr)) return 0;

        var strLen = inputStr.Length;
        if (strLen % 2 == 0) strLen--;

        long res = 0;
        long lastId = (strLen - 1) / 2;
        int lastIdAmount = inputStr[strLen - 1] - '0';
        var idx = 0;
        var pos = 0;

        while (idx < strLen)
        {
            var idxAmout = inputStr[idx] - '0';
            var bakAmout = idxAmout;
            if (idxAmout == 0)
            {
                idx++;
                continue;
            }

            if (idx % 2 == 0)
            {
                res = CalculateCurId(res, idx, pos, idxAmout);
                pos += idxAmout;
            }
            else
            {
                while (idxAmout > 0 && !(lastIdAmount <= 0 && strLen - 2 < idx))
                {
                    UpdateNewLastId(inputStr, ref strLen, ref lastId, ref lastIdAmount);

                    var bakLastIdAmount = lastIdAmount;
                    if (lastIdAmount >= idxAmout)
                    {
                        CalculateLastId(ref res, lastId, ref lastIdAmount, pos, idxAmout);
                        pos += idxAmout;
                    }
                    else
                    {
                        CalculateLastId(ref res, lastId, ref lastIdAmount, pos, lastIdAmount);
                        pos += bakLastIdAmount;
                    }

                    idxAmout -= bakLastIdAmount;
                }
            }

            idx++;
        }

        return res;
    }

    private static void CalculateLastId(ref long res, long lastId, ref int lastIdAmount, int pos, int idxAmout)
    {
        var posSum = (2 * pos + idxAmout - 1) * idxAmout / 2;
        res += lastId * posSum;
        lastIdAmount -= idxAmout;
    }

    private static long CalculateCurId(long res, int idx, int pos, int idxAmout)
    {
        var curId = idx / 2;
        var posSum = (2 * pos + idxAmout - 1) * idxAmout / 2;
        res += curId * posSum;
        return res;
    }

    private static void UpdateNewLastId(string inputStr, ref int strLen, ref long lastId, ref int lastIdAmount)
    {
        if (lastIdAmount <= 0)
        {
            strLen -= 2;
            lastId = (strLen - 1) / 2;
            lastIdAmount = inputStr[strLen - 1] - '0';
        }
    }

    public static void GetResult()
    {
        using StreamReader r = new("day-9.txt");
        string json = r.ReadToEnd();
        var inputStr = JsonConvert.DeserializeObject<string>(json);

        Console.WriteLine(GetCheckSum(inputStr));
    }
}