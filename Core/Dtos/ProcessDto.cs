using System;

namespace Core.Dtos
{
    [Serializable]
    public class ProcessDto
    {
        public int ProcId { get; set; }

        public string ProcName { get; set; }
    }
}