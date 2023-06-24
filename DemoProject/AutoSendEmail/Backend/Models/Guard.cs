using Serilog;
using System.Collections.Generic;
using System.IO;

namespace CommonBase.CustomerException
{
    public static class Guard
    {
        #region Check file
        public static bool Exist(string argument, string argumentName, bool handle = false)
        {
            if (!File.Exists(argument))
            {
                GuardException exception = new GuardException(argumentName, "File should be exist");
                if (handle)
                {
                    throw exception;
                }
                else
                {
                    Log.Error($"{exception.ArgumentName} {argument} : {exception.Message}");
                    return false;
                }
            }
            return true;
        }

        public static bool NotLocked(string argument, string argumentName, bool handle = false)
        {
            try
            {
                using (FileStream fileStream = File.Open(argument, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    fileStream.Close();
                }
                return true;
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                GuardException exception = new GuardException(argumentName, "File is being used by another process");
                if (handle)
                {
                    throw exception;
                }
                else
                {
                    Log.Error($"{exception.ArgumentName} {argument} : {exception.Message}");
                    return false;
                }
            }
        }
        #endregion

        #region Check null or empty
        public static bool NotNull<T>(T argument, string argumentName, bool handle = false)
        {
            if (argument == null)
            {
                GuardException exception = new GuardException(argumentName, "Argument should be not null");
                if (handle)
                {
                    throw exception;
                }
                else
                {
                    Log.Error($"{exception.ArgumentName} : {exception.Message}");
                    return false;
                }
            }
            return true;
        }

        public static bool StringNotNullOrEmpty(string argument, string argumentName, bool handle = false)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                GuardException exception = new GuardException(argumentName, "Argument should be not empty");
                if (handle)
                {
                    throw exception;
                }
                else
                {
                    Log.Error($"{exception.ArgumentName} : {exception.Message}");
                    return false;
                }
            }
            return true;
        }

        public static bool ListNotEmpty<T>(List<T> argument, string argumentName, bool handle = false)
        {
            if (argument.Count == 0)
            {
                GuardException exception = new GuardException(argumentName, "Argument should be not empty");
                if (handle)
                {
                    throw exception;
                }
                else
                {
                    Log.Error($"{exception.ArgumentName} : {exception.Message}");
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
