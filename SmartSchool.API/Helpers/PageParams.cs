namespace SmartSchool.API.Helpers
{
    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }   //vou usar isso no parametro de Repository
            //return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);

        public int? Matricula { get; set; } = null;
        public string Nome { get; set; } = string.Empty;
        public int? Ativo { get; set; } = null;

        
    }
}

