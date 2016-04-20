using System;

namespace LibraryInjected
{
    public interface IFileMon 
    {
        void OnCreateFile(string[] inFileNames);

        void OnDeleteFile(string[] inFileNames);
    }
}