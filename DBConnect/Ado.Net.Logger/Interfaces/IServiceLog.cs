using System;

namespace Ado.Net.Logger.Interfaces
{
    public interface IServiceLog
    {
        public void CreateLogTable();

        public void AddRowLog(Exception e);
    }
}