using System;

namespace LibraryInjected
{
    public interface IMon
    {
        void IsInstalled(int inClientPid);

        void ReportException(Exception inInfo);
    }
}