using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchService.Entities
{
    public class StoredResult
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
    }
}
