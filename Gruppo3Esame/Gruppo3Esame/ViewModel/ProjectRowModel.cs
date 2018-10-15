using Gruppo3Esame.Models;

namespace Gruppo3Esame.ViewModel
{
    public class ProjectRowModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Category Category { get; set; }
        public double TotalCost { get; set; }
    }   
}
