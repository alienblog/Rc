namespace Rc.Areas.Api.Dtos
{
	public class SelectFileInput
	{
		//File or Folder
		public string SelectType{get;set;}
		
		public string Path {get;set;}="/";
	}
}