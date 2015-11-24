namespace Rc.Core.Models
{
    public interface IRcModel<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IRcModel : IRcModel<int>
    {
    }

    public class RcModel<TPrimaryKey> : IRcModel<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }

    public class RcModel : RcModel<int>, IRcModel
    {
    }
}