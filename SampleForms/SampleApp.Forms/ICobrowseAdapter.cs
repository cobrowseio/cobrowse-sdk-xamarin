using System;
namespace SampleApp.Forms
{
    public interface ICobrowseAdapter
    {
        string DeviceId { get; }

        void StartCobrowse();

        void CheckCobrowseFullDevice();

        void EndCurrentSession();
    }
}
