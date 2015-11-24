using System;

namespace Rc.Core.Dtos
{
    public interface IAuditedDto<TPrimaryKey> : IRcDto<TPrimaryKey>
    {
        DateTime CreatedDate { get; set; }

        string CreatedDateString { get; }

        DateTime? UpdatedDate { get; set; }

        string UpdatedDateString { get; }

        DateTime? DeletedDate { get; set; }

        string DeletedDateString { get; }

        bool IsDeleted { get; set; }
    }

    public interface IAuditedDto : IAuditedDto<int>, IRcDto
    {

    }

    public class AuditedDto<TPrimaryKey> : RcDto<TPrimaryKey>, IAuditedDto<TPrimaryKey>
    {
        public DateTime CreatedDate { get; set; }

        public string CreatedDateString
        {
            get
            {
                return CreatedDate.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedDateString
        {
            get
            {
                return UpdatedDate.HasValue ? UpdatedDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : " - ";
            }
        }

        public DateTime? DeletedDate { get; set; }

        public string DeletedDateString
        {
            get
            {
                return DeletedDate.HasValue ? DeletedDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : " - ";
            }
        }

        public bool IsDeleted { get; set; }
    }

    public class AuditedDto : AuditedDto<int>, IAuditedDto, IRcDto
    {

    }
}