using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportesController : ControllerBase
    {
        private IReporteService reporteService;

        public ReportesController(IReporteService reporteService)
        {
            this.reporteService = reporteService;
        }

        [HttpGet()]
        [Route("[action]/{eventoId}")]
        public IActionResult GetLotesVendidos(int eventoId)
        {
            try
            {
                var reporte = reporteService.GetLotesVendidos(eventoId);
                var comlumHeadrs = new string[]
                {
                    "Nombre del vendedor",
                    "Nombre del lote",
                    "Precio inicial",
                    "Precio final"
                };

                byte[] result;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Current Employee");
                    using (var cells = worksheet.Cells[1, 1, 1, 4])
                    {
                        cells.Style.Font.Bold = true;
                    }

                    for (var i = 0; i < comlumHeadrs.Count(); i++)
                    {
                        worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                    }

                    var j = 2;
                    foreach (var lote in reporte)
                    {
                        worksheet.Cells["A" + j].Value = lote.NombreVendedor;
                        worksheet.Cells["B" + j].Value = lote.NombreLote;
                        worksheet.Cells["C" + j].Value = lote.PrecioInicial;
                        worksheet.Cells["D" + j].Value = lote.PrecioFinal;

                        j++;
                    }

                    for (int i = 1; i <= 10; i++) // this will aply it form col 1 to 10
                    {
                        worksheet.Column(i).Width = 18;
                    }
                    result = package.GetAsByteArray();
                }
                return File(result, "application/ms-excel", $"Employee.xlsx");

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet()]
        [Route("[action]/{loteId}")]
        public IActionResult GetCompradoresPorLote(int loteId)
        {
            try
            {
                var reporte = reporteService.GetCompradoresPorLote(loteId);
                var comlumHeadrs = new string[]
                {
                    "Nombre del comprador",
                    "Cantidad de pujas",
                    "Puja mayor"
                };

                byte[] result;

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Current Employee");
                    using (var cells = worksheet.Cells[1, 1, 1, 4])
                    {
                        cells.Style.Font.Bold = true;
                    }

                    for (var i = 0; i < comlumHeadrs.Count(); i++)
                    {
                        worksheet.Cells[1, i + 1].Value = comlumHeadrs[i];
                    }

                    var j = 2;
                    foreach (var lote in reporte)
                    {
                        worksheet.Cells["A" + j].Value = lote.NombreComprador;
                        worksheet.Cells["B" + j].Value = lote.CantidadPujas;
                        worksheet.Cells["C" + j].Value = lote.PujaMayor;
                        j++;
                    }

                    for (int i = 1; i <= 10; i++) // this will aply it form col 1 to 10
                    {
                        worksheet.Column(i).Width = 18;
                    }
                    result = package.GetAsByteArray();
                }
                return File(result, "application/ms-excel", $"Employee.xlsx");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet()]
        [Route("[action]")]
        public IActionResult GetTotal()
        {
            try
            {
                return Ok(reporteService.GetTotal());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
