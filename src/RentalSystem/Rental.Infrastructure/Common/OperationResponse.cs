namespace Rental.Infrastructure.Common
{
    public class OperationResponse
    {
        protected bool ForcedFailedResponse;
        public bool CompletedWithSuccess => OperationError == null && !ForcedFailedResponse;
        public OperationError OperationError { get; set; }

        public OperationResponse SetAsFailureResponse(OperationError operationError)
        {
            OperationError = operationError;
            ForcedFailedResponse = true;
            return this;
        }
    }

    public class OperationResponse<T> : OperationResponse
    {
        public OperationResponse() { }
        public OperationResponse(T result)
        {
            Result = result;
        }

        public T Result { get; set; }

        public new OperationResponse<T> SetAsFailureResponse(OperationError operationError)
        {
            base.SetAsFailureResponse(operationError);
            return this;
        }
    }
}
