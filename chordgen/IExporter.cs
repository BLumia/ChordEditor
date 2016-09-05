using common;

namespace chordgen
{
    internal interface IExporter
    {
        void ExportToFile(SongInfo info, string fileName);
    }
}