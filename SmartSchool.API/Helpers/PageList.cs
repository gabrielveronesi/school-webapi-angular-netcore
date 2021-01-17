using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.API.Helpers
{
    public class PageList<T> : List<T> //colocando um T, todos os metodos que teria em uma list vai herdar para o PAGELIST (Sem tem um tipo especifico) 
    {
        public int CurrentPag { get; set; } //Pagina Atual
        public int TotalPages { get; set; } //Total de Paginas
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList(List<T> items, int count, int pageSize, int pageNumber)
        {
            
            TotalCount = count;
            PageSize = pageSize;
            CurrentPag = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);//para adicionar varios itens da list
        }

        public static async Task<PageList<T>> CreateAsync(
            IQueryable<T> source, int pageNumber, int pageSize)
        
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber-1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);    
        }
    }
}