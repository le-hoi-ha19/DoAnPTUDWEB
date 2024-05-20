using DoAn_PTUDWEB.Models.ViewModels;

namespace DoAn_PTUDWEB.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
