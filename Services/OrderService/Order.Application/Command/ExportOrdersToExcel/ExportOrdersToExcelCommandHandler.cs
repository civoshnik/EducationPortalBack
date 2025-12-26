using MediatR;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Interfaces;
using Order.Application.Command.ExportOrdersToExcel;

namespace Order.Application.Queries.ExportOrdersToExcel
{
    public class ExportOrdersToExcelCommandHandler : IRequestHandler<ExportOrdersToExcelCommand, byte[]>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExportOrdersToExcelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<byte[]> Handle(ExportOrdersToExcelCommand request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(cancellationToken);

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Orders");

            ws.Cell(1, 1).Value = "ID заказа";
            ws.Cell(1, 2).Value = "Пользователь";
            ws.Cell(1, 3).Value = "Сумма";
            ws.Cell(1, 4).Value = "Статус";
            ws.Cell(1, 5).Value = "Создан";

            for (int i = 0; i < orders.Count; i++)
            {
                var o = orders[i];
                ws.Cell(i + 2, 1).Value = o.OrderId.ToString();
                ws.Cell(i + 2, 2).Value = o.UserId.ToString();
                ws.Cell(i + 2, 3).Value = o.TotalPrice;
                ws.Cell(i + 2, 4).Value = o.Status.ToString();
                ws.Cell(i + 2, 5).Value = o.CreatedAt.ToString("dd.MM.yyyy HH:mm");
            }


            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
