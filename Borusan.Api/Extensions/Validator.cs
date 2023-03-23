using Borusan.Data;
using FluentValidation;

namespace Borusan.Api.Extensions
{
	//Todo:Validator not working
	public class MaterialValidator : AbstractValidator<MaterialDTO>
	{
		public MaterialValidator()
		{
			RuleFor(i => i.MaterialCode).NotNull().WithMessage("MaterialCode can not be null");
			RuleFor(i => i.MaterialName).NotNull().WithMessage("MaterialName can not be null");
		}
	}

	public class OrderValidator : AbstractValidator<OrderDTO>
	{
		public OrderValidator()
		{
			RuleFor(i => i.CustomerOrderNo).NotNull().WithMessage("CustomerOrderNo can not be null");
			RuleFor(i => i.SourceAddress).NotNull().WithMessage("SourceAddress can not be null");
			RuleFor(i => i.DestinationAddress).NotNull().WithMessage("DestinationAddress can not be null");
			RuleFor(i => i.Quantity).NotNull().WithMessage("Quantity can not be null");
			RuleFor(i => i.QuantityUnit).NotNull().WithMessage("QuantityUnit can not be null");
			RuleFor(i => i.Weight).NotNull().WithMessage("Weight can not be null");
			RuleFor(i => i.MaterialCode).NotNull().WithMessage("Material can not be null");
			RuleFor(i => i.MaterialName).NotNull().WithMessage("Material can not be null");
			RuleFor(i => i.Note).NotNull().WithMessage("Note can not be null");
			RuleFor(i => i.CustomerCode).NotNull().WithMessage("CustomerCode can not be null");
		}
	}
}
