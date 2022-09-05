using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace WebMVC.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Import(IFormFile uploadFile, CancellationToken cancellationToken)
        {
            if (uploadFile == null || uploadFile.Length <= 0)
                return BadRequest("File is empty");

            if (!Path.GetExtension(uploadFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return BadRequest("File extension is not supported");

            using (var stream = new MemoryStream())
            {
                await uploadFile.CopyToAsync(stream, cancellationToken);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    var rowCount = worksheet.Dimension.Rows;
                }
            }
            TempData["success"] = "Successful Data Import!";
            //return Ok($"{file.FileName} Uploaded");
            return RedirectToAction("Index");
        }
    }
}