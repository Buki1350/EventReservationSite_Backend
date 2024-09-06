using DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Url;

namespace DotNetBoilerplate.Shared.Abstractions.Payments;

public record PaymentLinkDto(Url PaymentLink);