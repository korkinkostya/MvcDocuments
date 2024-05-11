using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDocuments.Domain.Interfaces;


namespace MvcDocuments.Controllers;

public class DocumentsController : Controller
{

    private readonly IDocumentRepository _documentRepository;

    private readonly IDocumentRowRepository _documentRowRepository;

    public DocumentsController( IDocumentRepository documentRepository,
        IDocumentRowRepository documentRowRepository)
    {

        _documentRepository = documentRepository;
        _documentRowRepository = documentRowRepository;
    }

    // GET: Documents
    public async Task<IActionResult> Index()
    {
        IEnumerable<Domain.Entities.Document> docList = await _documentRepository.GetAllAsync();

        return View(docList);
    }


    // GET: Documents/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Documents/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Number,Date")] MvcDocuments.Domain.Entities.Document document)
    {
        
        if (ModelState.IsValid)
        {
            _documentRepository.AddAsync(document);

            return RedirectToAction("Index");
        }

        return View(document);
    }

    // GET: Documents/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var document = await _documentRepository.GetByIdAsync(id);


        if (document == null)
        {
            return NotFound();
        }


        return View(document);
    }

    // POST: Documents/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Number,Date")] MvcDocuments.Domain.Entities.Document document)
    {
        ;
        if (id != document.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _documentRepository.UpdateAsync(document);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DocumentExists(document.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToAction("Index");
        }

        return View(document);
    }

    private async Task<bool> DocumentExists(int id)
    {
        return await _documentRepository.DocumentExistsAsync(id);
    }

    [HttpGet]
    public async Task<IActionResult> CreateRow(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var document = await _documentRepository.GetByIdAsync((int)id);


        if (document == null)
        {
            return NotFound();
        }

        return View(new MvcDocuments.Domain.Entities.DocumentRow() { DocumentId = document.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRow(string name, decimal price,
        [Bind("Id,Number,Date")] MvcDocuments.Domain.Entities.Document document)
    {

        var documentRow = new MvcDocuments.Domain.Entities.DocumentRow()
            { Name = name, Price = price, DocumentId = document.Id };


        await _documentRowRepository.AddAsync(documentRow);

        return RedirectToAction("Edit", new { id = document.Id });
    }

    public async Task<IActionResult> EditRow(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var documentRow = await _documentRowRepository.GetByIdAsync((int)id);


        if (documentRow == null)
        {
            return NotFound();
        }


        return View(documentRow);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRow(int id, [Bind("Id,Name,Price,DocumentId")] MvcDocuments.Domain.Entities.DocumentRow documentRow)
    {
        if (id != documentRow.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _documentRowRepository.UpdateAsync(documentRow);

                var foo = await DocumentRowExists(documentRow.Id);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DocumentRowExists(documentRow.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return RedirectToAction("Edit", new { id = documentRow.DocumentId });
        }

        return View(documentRow);
    }

    private async Task<bool> DocumentRowExists(int id)
    {
        return await _documentRowRepository.DocumentRowExistsAsync(id);
    }


    // GET: Documents/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }


        var document = await _documentRepository.GetByIdAsync((int)id);


        if (document == null)
        {
            return NotFound();
        }


        return View(document);
    }

    // POST: Documents/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        var document = await _documentRepository.GetByIdAsync(id);
        await _documentRepository.Remove(document);

        
        return RedirectToAction("Index");
    }


    // GET: Documents/Delete/5
    public async Task<IActionResult> DeleteRow(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var documentRow = await _documentRowRepository.GetByIdAsync((int)id);
        
        
        if (documentRow == null)
        {
            return NotFound();
        }

        return View(documentRow);
    }

    [HttpPost, ActionName("DeleteRow")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRowConfirmed(int id)
    {
        
        var documentRow = await _documentRowRepository.GetByIdAsync((int)id);

        await _documentRowRepository.Remove(documentRow);
        
        if (documentRow == null)
        {
            return NotFound();
        }

        return RedirectToAction("Edit", new { id = documentRow.DocumentId });
    }

    // GET: Documents/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var document = await _documentRepository.GetByIdAsync((int)id);
        
        if (document == null)
        {
            return NotFound();
        }


        return View(document);
    }
}