using System;

namespace LibraryInjected
{
    public interface IFileFunctionInject : IFuncionInject
    {
        void OnCreateFile(string[] inFileNames);

        void OnDeleteFile(string[] inFileNames);
    }
}