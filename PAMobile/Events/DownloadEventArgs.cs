namespace PAMobile.Events;

internal class DownloadEventArgs : EventArgs
{
    internal bool FileSaved = false;
    public DownloadEventArgs(bool fileSaved)
    {
        FileSaved = fileSaved;
    }
}
