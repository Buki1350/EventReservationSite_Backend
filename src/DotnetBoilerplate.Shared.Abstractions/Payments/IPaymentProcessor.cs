namespace DotNetBoilerplate.Shared.Abstractions.Payments;

public interface IPaymentProcessor
{
    Task<PaymentLinkDto> CreatePaymentLinkAsync(Guid orderId, Guid customerId, Guid paymentId, int price,
        string currency);
}