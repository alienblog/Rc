namespace Rc.Core.Dtos
{
	public interface IRcDto<TPrimaryKey>
	{
		TPrimaryKey Id{get;set;}
	}
	
	public interface IRcDto:IRcDto<int>
	{
		int Id{get;set;}
	}
		
	public class RcDto<TPrimaryKey>:IRcDto<TPrimaryKey>
	{
		public TPrimaryKey Id { get; set; }
	}
	
	public class RcDto:RcDto<int>,IRcDto
	{
	}
}