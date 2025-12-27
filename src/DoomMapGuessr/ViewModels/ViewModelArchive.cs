using System.Collections.Generic;


namespace DoomMapGuessr.ViewModels
{

    internal static class ViewModelArchive
    {

        private static readonly Dictionary<string, ViewModelBase> vms = [];

        public static T Get<T>()
            where T : ViewModelBase =>
            (vms[typeof(T).FullName!] as T)!;

        public static void Set<T>(T value)
            where T : ViewModelBase =>
            vms[typeof(T).FullName!] = value;

        public static bool TryGet<T>(out T? value)
            where T : ViewModelBase
        {

            try
            {

                value = Get<T>();

                return true;

            }
            catch
            {

                value = null;

                return false;

            }

        }

    }

}
