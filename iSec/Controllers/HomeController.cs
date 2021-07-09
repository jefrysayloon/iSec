using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iSec.Models;
using iSec.Libraries.TextEncryption;


namespace iSec.Controllers
{
    public class HomeController : Controller
    {
        string key = "b14ca5898a4e4133bbce2ea2315a1916";
        private readonly iSecDBContext _context;
        private ITextEncryptionLibrary _text;

        public HomeController(iSecDBContext context, ITextEncryptionLibrary text)
        {
            _context = context;
            _text = text;
        }

        public async Task<IActionResult> Index()
        {
            var query = await _context.TextTables.ToListAsync();
            return View(query);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Encrypt()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Encrypt(TextTable text)
        {
            var res = new List<string>();
            var encrypted = await _text.Encrypt(key, text.EnteredText);
            var decrypted = await _text.Decrypt(key, encrypted);
            res.Add(encrypted);
            res.Add(decrypted);
            return Json(res);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = await _context.TextTables.FirstOrDefaultAsync(m => m.Id == id);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnteredText,EncryptedText,DecryptedText")] TextTable textTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Encrypt));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.TextTables.FindAsync(id);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEncryptedText(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = await _context.TextTables.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<TextTable>(
                query,
                "",
                s => s.EnteredText, s => s.EncryptedText, s => s.DecryptedText))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(query);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _context.TextTables.FirstOrDefaultAsync(m => m.Id == id);

            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var query = await _context.TextTables.FindAsync(id);

            _context.TextTables.Remove(query);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TextExists(long id)
        {
            return _context.TextTables.Any(e => e.Id == id);
        }
    }
}
