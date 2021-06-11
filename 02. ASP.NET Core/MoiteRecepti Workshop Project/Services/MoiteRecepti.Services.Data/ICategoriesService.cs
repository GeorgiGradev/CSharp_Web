namespace MoiteRecepti.Services.Data
{
    using System.Collections.Generic;


    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllASKeyValuePair();
    }
}
