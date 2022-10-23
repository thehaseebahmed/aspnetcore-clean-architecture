using Starter.Application.Constants;

namespace Starter.Application.ViewModels;

public class ValidationErrorsViewModel
{
    public int ErrorCode { get; set; } = ErrorConstants.VALIDATION_ERROR_CODE;
    public List<ValidationErrorViewModel> ErrorMessages { get; set; } = new List<ValidationErrorViewModel>();
}

public class ValidationErrorViewModel
{
    public string Field { get; set; }
    public string Message { get; set; }
}