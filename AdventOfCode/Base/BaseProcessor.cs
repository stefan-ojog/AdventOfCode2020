namespace AdventOfCode.Base
{
    public abstract class BaseProcessor<TInput, TResult> : IProcessor
    {
        private readonly IInputReader _inputReader;
        private readonly string _inputResourceName;
        private readonly IInputTransform<TInput> _inputTransform;

        public BaseProcessor(IInputReader inputReader, string inputResourceName, IInputTransform<TInput> inputTransform)
        {
            _inputReader = inputReader;
            _inputResourceName = inputResourceName;
            _inputTransform = inputTransform;
        }

        public TResult ComputeResult()
        {
            var input = _inputReader.GetInput(_inputResourceName);
            var model = _inputTransform.Create(input);

            return ComputeResultLogic(model);
        }

        public string GetResultAsString()
        {
            return ComputeResult().ToString();
        }

        protected abstract TResult ComputeResultLogic(TInput input);
    }

    public interface IProcessor
    {
        string GetResultAsString();
    }
}
