using DanaCopilot.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DanaCopilot.Domain.Entities
{
    [Table("Documents", Schema = "core")]
    public class Document
    {
        [Key]
        public long Id { get; set; }
        public int OrganizationId { get; set; }
        public string Title { get; set; }
        public string FileExtension { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileName { get; set; }
        public int? UploadedByUserId { get; set; }
        public DateTime UploadedAt { get; set; }
        public string FilePath { get; set; }
        public DocumentStatus Status { get; set; }
    }
}
