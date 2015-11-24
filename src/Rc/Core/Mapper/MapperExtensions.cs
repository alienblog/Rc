namespace Rc
{
	public static class MapperExtensions
	{
		public static T Map<T>(this object obj)
		{
			return AutoMapper.Mapper.Map<T>(obj);
		}
	}
}