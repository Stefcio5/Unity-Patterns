using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public static class StorageServiceFactory
{
    public static IDataService Create(ISerializer serializer)
    {
#if UNITY_EDITOR
        return new WebGLDataService(serializer);

#else
        return new FileDataService(serializer);
#endif
    }
}