using System.Collections.Generic;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts
{
    public class MarkdownPart : DashboardPart
    {
        private readonly string _content;
        private readonly string _title;
        private readonly string _subtitle;
        private readonly int _markdownSource;
        private readonly string _markdownUri;
        
        public MarkdownPart(string content = "", string title = "", string subtitle = "", int markdownSource = 1, string markdownUri = null)
        {
            _content = content;
            _title = title;
            _subtitle = subtitle;
            _markdownSource = markdownSource;
            _markdownUri = markdownUri;
        }
        
        public override Part GeneratePart()
        {
            var part = new Part();
            part.WithPosition(_x, _y, _rowSpan, _colSpan);
            part.Metadata = new PartMetadata();
            part.Metadata.WithType("Extension/HubsExtension/PartType/MarkdownPart");
            part.Metadata.Inputs = new List<Input>();
            part.Metadata.Settings.Content = new
            {
                Settings = new
                {
                    Content = _content,
                    Title = _title,
                    Subtitle = _subtitle,
                    MarkdownSource = _markdownSource,
                    MarkdownUri = _markdownUri
                }
            };
            return part;
        }

       
    }
}