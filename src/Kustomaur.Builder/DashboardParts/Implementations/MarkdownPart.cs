using System.Collections.Generic;
using Kustomaur.Models;

namespace Kustomaur.Dashboard.DashboardParts.Implementations
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
            
            // Default Row and Column
            WithRowSpan(3);
            WithColSpan(3);
        }
        
        public override Part GeneratePart()
        {
            _part.WithPosition(_x, _y, _rowSpan, _colSpan);
            _part.Metadata = new PartMetadata();
            _part.Metadata.WithType("Extension/HubsExtension/PartType/MarkdownPart");
            _part.Metadata.Inputs = new List<Input>();
            _part.Metadata.Settings.Content = new
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
            return _part;
        }

       
    }
}