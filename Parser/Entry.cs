using System;

namespace Parser
{
    public class Entry
    {
        public DateTime Date { set; get; }
        public string Message { set; get; }
        public string Type { set; get; }
        public string SenderName { set; get; }
        public string ElementType { set; get; }
        public uint SenderId { set; get; }
        public uint RecipientId { set; get; }
        public string Path { set; get; }
        public string Offset { set; get; }
        public string Length { set; get; }
    }
}