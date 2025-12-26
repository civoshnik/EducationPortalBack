using MediatR;

namespace Order.Application.Command.ExportOrdersToExcel
{
    public record ExportOrdersToExcelCommand : IRequest<byte[]>;
}
