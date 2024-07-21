using ModData;
using BuildInfo = Watches.Properties.BuildInfo;

namespace Watches.Managers;

internal static class DataManager
{
    private static readonly ModDataManager s_ModDataManager = new(BuildInfo.Name);

    internal static void SaveData<T>(T data, string suffix) where T : struct
    {
        var serializedData = Convert.ToString(data);
        s_ModDataManager.Save(serializedData, suffix);
    }

    internal static T? LoadData<T>(string suffix) where T : struct
    {
        var loadedData = s_ModDataManager.Load(suffix);
        if (loadedData != null && TryParse(loadedData, out T result))
        {
            return result;
        }
        return null;
    }

    private static bool TryParse<T>(string value, out T result) where T : struct
    {
        try
        {
            result = (T)Convert.ChangeType(value, typeof(T));
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}