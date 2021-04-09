namespace AdventOfCode.Base
{
	public interface IProcessorFactory
	{
		IProcessor GetProcessor(int day, int part);
	}
}