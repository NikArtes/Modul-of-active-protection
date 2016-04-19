using System;

namespace LibraryInjected
{
    public interface IFileMon : IMon
    {
        void OnCreateFile(int inClientPid, string[] inFileNames);

        void OnDeleteFile(int inClientPid, string[] inFileNames);
    }
}