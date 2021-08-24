using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MicroShop.Data;
using MicroShop.Models;

namespace MicroShop.Controllers
{
    public class ManageShopController : Controller
    {
        private readonly MicroShopContext _context;

        public ManageShopController(MicroShopContext context)
        {
            _context = context;
        }

        // GET: MangeShop
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShopItem.ToListAsync());
        }

        // GET: MangeShop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopItem == null)
            {
                return NotFound();
            }

            return View(shopItem);
        }

        // GET: MangeShop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MangeShop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,AmountInStock")] ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shopItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        // GET: MangeShop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem.FindAsync(id);
            if (shopItem == null)
            {
                return NotFound();
            }
            return View(shopItem);
        }

        // POST: MangeShop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,AmountInStock")] ShopItem shopItem)
        {
            if (id != shopItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shopItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopItemExists(shopItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        // GET: MangeShop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shopItem = await _context.ShopItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shopItem == null)
            {
                return NotFound();
            }

            return View(shopItem);
        }

        // POST: MangeShop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shopItem = await _context.ShopItem.FindAsync(id);
            _context.ShopItem.Remove(shopItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopItemExists(int id)
        {
            return _context.ShopItem.Any(e => e.Id == id);
        }
    }
}
